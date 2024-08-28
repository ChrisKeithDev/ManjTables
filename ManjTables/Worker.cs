using ManjTables.DataModels;
using ManjTables.DataModels.Models;
using ManjTables.DataModels.Models.Forms;
using ManjTables.JsonParsing;
using ManjTables.ObjectMapping.TablesMapping;
using ManjTables.SQLiteReaderLib;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static System.Formats.Asn1.AsnWriter;

namespace ManjTables
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;
        private IFormsJsonParser? _formsJsonParser;
        private Timer? _debounceTimer;
        private FormTemplateIds? _formTemplateIds;
        private List<ChildInfo>? _childInfos;
        private List<FormModel>? _formModels;
        private List<Classroom>? _classrooms;
        private readonly TablesMapper _tablesMapper;

        public Worker(ILogger<Worker> logger, ILogger<TablesMapper> tablesMapperLogger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _childInfos = new List<ChildInfo>();
            _formModels = new List<FormModel>();
            _classrooms = new List<Classroom>();
            _tablesMapper = new TablesMapper(tablesMapperLogger);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            string dbControlLogPath = WorkerUtility.GetControlLogPath();

            using FileSystemWatcher fileSystemWatcher = WorkerUtility.SetupFileSystemWatcher(dbControlLogPath);

            _logger.LogInformation("{Now} Watching for changes to {dbControlLogPath}", DateTime.Now, dbControlLogPath);

            fileSystemWatcher.Changed += async (sender, e) =>
            {
                // Dispose of the existing timer if it exists
                _debounceTimer?.Dispose();
                _logger.LogInformation("File system watcher event received");

                // Delete existing records or database and create a new one
                using (var initScope = _serviceProvider.CreateScope())
                {
                    try
                    {
                        var initContext = initScope.ServiceProvider.GetRequiredService<ManjTablesContext>();
                        _logger.LogInformation("Attempting to delete existing database");
                        bool isDeleted = await initContext.Database.EnsureDeletedAsync(stoppingToken);
                        _logger.LogInformation("Database deletion status: {Status}", isDeleted ? "Deleted" : "Not Deleted");

                        _logger.LogInformation("Attempting to create a new database");
                        bool isCreated = await initContext.Database.EnsureCreatedAsync(stoppingToken);
                        _logger.LogInformation("Database creation status: {Status}", isCreated ? "Created" : "Not Created");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("An exception occurred while trying to delete or create the database: {Message}", ex.Message);
                    }
                }

                // Create a new timer that waits for 500 milliseconds before invoking the method
#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
                _debounceTimer = new Timer(DebounceTimerCallback, e.FullPath, 500, Timeout.Infinite);
#pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
            };

            fileSystemWatcher.EnableRaisingEvents = true;

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }

        private async void DebounceTimerCallback(object state)
        {

            using var scope = InitializeServicesAndVariables(out var context, out var sqliteReaderService,
                out var formsJsonParser, out var workerUtility, out ILogger<Worker> logger,
                out var dataTransformationService, out StaffProcessingService staffMemberService);

            if (state is not string filePath)
            {
                throw new ArgumentException("Invalid state object. Expected a string.");
            }

            _logger.LogInformation("Reading database path from {filePath}", filePath);
            string dbPath = workerUtility.ReadDatabasePath(filePath);

            _logger.LogInformation("Logging database change at path = {dbPath}", dbPath);
            workerUtility.LogDatabaseChange(dbPath);

            (List<ChildInfo> childInfos, FormTemplateIds? formTemplateIds, List<Classroom> classrooms) 
                = sqliteReaderService.ReadChildInfoAndTemplateIdsFromSQLite(dbPath);

            if (childInfos.Count == 0 || formTemplateIds == null)
            {
                workerUtility.HandleEmptyChildInfoTable();
                throw new InvalidOperationException("No ChildInfo or FormTemplateIds records found.");
            }

            logger.LogInformation("{Now} ManjTables DB reading operation completed.", DateTime.Now);

            _childInfos = childInfos;
            _formTemplateIds = formTemplateIds;
            _formsJsonParser = formsJsonParser;
            _classrooms = classrooms;
            
            context.Classrooms.AddRange(_classrooms);
            await context.SaveChangesAsync();

            // At this point, everything I need is in ChildInfo and FormTemplateIds
            // So here is where I pass _childInfos into a method to deserialize the JSON into FormModel objects
            if (_childInfos != null)
            {
                try
                {
                    _formModels = _formsJsonParser.ParseRawJsonIntoFormModels(_childInfos);
                }
                catch (JsonReaderException ex)
                {
                    _logger.LogError("An error occurred while parsing JSON: {Message}", ex.Message);
                    // Additional logging
                }

            }
            
            _logger.LogInformation("FormModels count before filtering: {Count}", _formModels?.Count ?? 0);
            _logger.LogInformation("FormTemplateIds before filtering: EmergencyContact: {E}, " +
                "ApprovedPickup: {A}, PhotoRelease: {P}, AnimalApproval: {An}, GoingOutPermission: {G}",
                   _formTemplateIds?.EmergencyInformationTemplateId,
                   _formTemplateIds?.ApprovedPickupTemplateId,
                   _formTemplateIds?.PhotoReleaseTemplateId,
                   _formTemplateIds?.AnimalApprovalTemplateId,
                   _formTemplateIds?.GoingOutPermissionTemplateId);


            // Then I filter the FormModel objects using FormTemplateIds
            if (_formModels != null && _formTemplateIds != null && _childInfos != null)
            {
                _formModels = _formsJsonParser.FilterFormsUsingFormTemplateIds(_formModels, _formTemplateIds);
                SupportAndDebugMethods.CheckForMissingForms(filteredForms: _formModels, 
                    formTemplateIds: _formTemplateIds, logger: _logger);

                // Now I have a list of FormModel objects and ChildInfo objects that
                // I can use to map to the appropriate classes.      
                
                _logger.LogInformation("{Now} ManjTables DB reading operation completed.", DateTime.Now);

                // Load the JSON file for StaffMember assignments
                string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Models/HardcodedJson/StaffMembers.json");

                var staffProcessingService = scope.ServiceProvider.GetRequiredService<StaffProcessingService>();
                await StaffProcessingService.ProcessStaffMembersFromJson(context, jsonFilePath);

                // I need to create a list of AggregatedChildAndFormData objects.
                _logger.LogInformation("ChildInfos count: {Count}", _childInfos.Count);
                _logger.LogInformation("FormModels count: {Count}", _formModels.Count);
                _logger.LogInformation("Entering the Aggregation foreach Logic");

                // Each instance of AggregatedChildAndFormData needs to represent
                // a single Child and the four main FormModels.
                List<AggregatedChildAndFormData> aggregatedDataList = dataTransformationService
                    .AggregateChildAndFormData(_childInfos, _formModels, _formTemplateIds);

                _logger.LogInformation("AggregatedDataList count: {Count}", aggregatedDataList.Count);
                _logger.LogInformation("Entering the Mapping foreach Logic");

                bool itsOkay = true;
                foreach (var aggregatedData in aggregatedDataList)
                {
                    _logger.LogInformation("Mapping for ChildId: {ChildId}", aggregatedData.ChildInfoData.ChildId);
                    _logger.LogInformation("Child Name: {FirstName} {LastName}", aggregatedData.ChildInfoData.FirstName, aggregatedData.ChildInfoData.LastName);

                    _tablesMapper.MapAllTables(
                        aggregatedData,
                        out Child child,
                        out Programme programme,
                        out Address childAddress,
                        out List<Address> parentAddresses,
                        out List<ApprovedAdult> approvedAdults,
                        out List<EmergencyContact> emergencyContacts,
                        out List<Parent> parents,
                        out AnimalApprovalForm animalApprovalForm,
                        out ApprovedPickupForm approvedPickupForm,
                        out EmergencyInformationForm emergencyInformationForm,
                        out PhotoReleaseForm photoReleaseForm,
                        out GoingOutPermissionForm goingOutPermissionForm);
                 
                    try
                    {
                        Programme? existingProgramme = context.Programmes
                            .FirstOrDefault(p => p.ProgrammeName == programme.ProgrammeName
                            && p.DismissalTime == programme.DismissalTime);

                        if (existingProgramme != null)
                        {
                            programme = existingProgramme;
                        }
                        else
                        {
                            context.Programmes.Add(programme);
                            await context.SaveChangesAsync(); 
                        }

                        child.ProgrammeId = programme.ProgrammeId;

                        context.Childs.AddRange(child);
                        context.Entry(child).State = EntityState.Added;

                        child.Address = await AddressService.HandleAddressAsync(context, childAddress);

                        for (int i = 0; i < parentAddresses.Count; i++)
                        {
                            var parentAddress = parentAddresses[i];
                            var parent = parents[i];
                            parent.Address = await AddressService.HandleAddressAsync(context, parentAddress);
                        }

                        context.ApprovedAdults.AddRange(approvedAdults);

                        context.EmergencyContacts.AddRange(emergencyContacts);

                        // Add or update parents without removing any.
                        ParentService.AddOrUpdateParents(context, parents);

                        // Now you need to associate the parents with the child
                        foreach (var parent in parents)
                        {
                            var childParent = new ChildParent
                            {
                                ParentId = parent.ParentId,
                                ChildId = child.ChildId
                            };
                            // This check ensures you don't add duplicates
                            if (!context.ChildParents.Any(cp => cp.ParentId == parent.ParentId && cp.ChildId == child.ChildId))
                            {
                                context.ChildParents.Add(childParent);
                            }
                        }

                        context.AnimalApprovalForms.Add(animalApprovalForm);
                        context.Entry(animalApprovalForm).State = EntityState.Added;

                        context.ApprovedPickupForms.Add(approvedPickupForm);
                        context.Entry(approvedPickupForm).State = EntityState.Added;

                        context.EmergencyInformationForms.Add(emergencyInformationForm);
                        context.Entry(emergencyInformationForm).State = EntityState.Added;

                        context.PhotoReleaseForms.Add(photoReleaseForm);
                        context.Entry(photoReleaseForm).State = EntityState.Added;

                        context.GoingOutPermissionForms.Add(goingOutPermissionForm);
                        context.Entry(goingOutPermissionForm).State = EntityState.Added;

                        await context.SaveChangesAsync();

                        // Link Child with ApprovedAdults
                        child.ChildApprovedAdults = approvedAdults.Select(adult => new ChildApprovedAdult
                        {
                            ApprovedAdultId = adult.ApprovedAdultId,
                            ChildId = child.ChildId
                        }).ToList();

                        // Link Child with EmergencyContacts
                        child.ChildEmergencyContacts = emergencyContacts.Select(ec => new ChildEmergencyContact
                        {
                            EmergencyContactId = ec.EmergencyContactId,
                            ChildId = child.ChildId
                        }).ToList();

                        // Link Child with Parents
                        child.ChildParents = parents.Select(p => new ChildParent
                        {
                            ParentId = p.ParentId,
                            ChildId = child.ChildId
                        }).ToList();

                        // Link ApprovedPickupForm with ApprovedAdults
                        approvedPickupForm.ApprovedAdultAndPickupForms = approvedAdults.Select(aa => new ApprovedAdultAndPickupForm
                        {
                            ApprovedAdultId = aa.ApprovedAdultId,
                            PickupFormId = approvedPickupForm.ApprovedPickupFormId
                        }).ToList();

                        // Link EmergencyInformationForm with EmergencyContacts
                        emergencyInformationForm.EmergencyContactEmergencyInformationForms = emergencyContacts.Select(ec => new EmergencyContactEmergencyInformationForm
                        {
                            EmergencyContactId = ec.EmergencyContactId,
                            EmergencyInformationFormId = emergencyInformationForm.EmergencyInformationFormId
                        }).ToList();

                        // Inspect what's in the change tracker
                        // Adds a lot of logs which drown out the rest, only turn on when needed

                        //foreach (var entry in context.ChangeTracker.Entries())
                        //{
                        //    _logger.LogInformation("Entity: {EntityName}, State: {EntityState}", entry.Entity.GetType().Name, entry.State);

                        //}

                        await context.SaveChangesAsync();
                        _logger.LogInformation("=========== Data saved to database. ============");
                        itsOkay = true;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("An error occurred while saving to the database: {Message}", ex.Message);
                        itsOkay = false;
                    }
                }
                if (itsOkay)
                {
                    Thread.Sleep(3000);
                    WorkerUtility.CopyDbFilesToNewFolder();
                }
            }
        }
        private IServiceScope InitializeServicesAndVariables(out ManjTablesContext context,
            out SQLiteReaderService sqliteReaderService, out IFormsJsonParser formsJsonParser,
            out WorkerUtility workerUtility, out ILogger<Worker> logger,
            out DataTransformationService dataTransformationService, out StaffProcessingService staffMemberService)
        {
            // Create a scope for resolving scoped services
            var scope = _serviceProvider.CreateScope();

            // Resolve the services within the scope
            context = scope.ServiceProvider.GetRequiredService<ManjTablesContext>();
            sqliteReaderService = scope.ServiceProvider.GetRequiredService<SQLiteReaderService>();
            formsJsonParser = scope.ServiceProvider.GetRequiredService<IFormsJsonParser>();
            workerUtility = scope.ServiceProvider.GetRequiredService<WorkerUtility>();
            logger = scope.ServiceProvider.GetRequiredService<ILogger<Worker>>();
            dataTransformationService = scope.ServiceProvider.GetRequiredService<DataTransformationService>();
            staffMemberService = scope.ServiceProvider.GetRequiredService<StaffProcessingService>();

            // Return the scope for disposal in the calling method
            return scope;
        }


    }
}
