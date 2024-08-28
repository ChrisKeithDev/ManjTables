using ManjTables.DataModels;
using ManjTables.DataModels.Models;
using ManjTables.DataModels.Models.Forms;
using ManjTables.ObjectMapping.MappingServices;
using Microsoft.Extensions.Logging;

namespace ManjTables.ObjectMapping.TablesMapping
{
    public class TablesMapper : ITablesMapper
    {
        private readonly ILogger<TablesMapper> _logger;

        public TablesMapper(ILogger<TablesMapper> logger)
        {
            _logger = logger;
        }
        public Address ChildAddressTableMapper(AggregatedChildAndFormData aggregatedData)
        {
            var formFields = aggregatedData.EmergencyContactForm?.Fields;
            Address address = new()
            {
                StreetAddress = formFields?["Child's Address.street"] as string,
                City = formFields?["Child's Address.city"] as string,
                State = formFields?["Child's Address.state_province"] as string,
                ZipCode = formFields?["Child's Address.postal_code"] as string,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            return address;
        }

        public List<Address> ParentAddressesTableMapper(AggregatedChildAndFormData aggregatedData)
        {
            var formFields = aggregatedData.EmergencyContactForm?.Fields;
            List<Address> addresses = new();

            for (int i = 1; i <= 2; i++)
            {
                string? street = formFields?[$"Parent {i} Address (if different from child).street"] as string;
                string? city = formFields?[$"Parent {i} Address (if different from child).city"] as string;
                string? state = formFields?[$"Parent {i} Address (if different from child).state_province"] as string;
                string? zipCode = formFields?[$"Parent {i} Address (if different from child).postal_code"] as string;

                if (!string.IsNullOrEmpty(street))
                {
                    Address address = new()
                    {
                        StreetAddress = street,
                        City = city,
                        State = state,
                        ZipCode = zipCode,
                        DateCreated = DateTime.Now,
                        DateModified = DateTime.Now
                    };

                    addresses.Add(address);
                }
            }

            return addresses;
        }

        public AnimalApprovalForm AnimalApprovalFormTableMapper(AggregatedChildAndFormData aggregatedData)
        {
            var formFields = aggregatedData.AnimalApprovalForm?.Fields;
            AnimalApprovalForm animalApprovalForm = new()
            {
                FormTemplateId = aggregatedData.AnimalApprovalForm?.FormTemplateId,
                Permission = formFields?["Permission"] as string,
                SignatureOne = formFields?["Signature of parent or guardian 1"] as string,
                SignatureTwo = formFields?["Signature of parent or guardian 2"] as string,
                ChildId = aggregatedData.ChildInfoData.ChildId,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            return animalApprovalForm;
        }

        public List<ApprovedAdult> ApprovedAdultTableMapper(AggregatedChildAndFormData aggregatedData)
        {
            var formFields = aggregatedData.ApprovedPickupForm?.Fields;
            List<ApprovedAdult> approvedAdults = new();

            // Loop through 1 to 4 for "with notice" and "without notice"
            for (int i = 1; i <= 4; i++)
            {
                // With Notice
                string? nameWithNotice = formFields?.GetValueOrDefault($"Authorized Pick up with notice_{i}_Name") as string;
                string? phoneWithNotice = formFields?.GetValueOrDefault($"Authorized Pick up with notice_{i}_Phone Number") as string;
                string? relationshipWithNotice = formFields?.GetValueOrDefault($"Authorized Pick up with notice_{i}_Relationship to Child") as string;

                if (!string.IsNullOrEmpty(nameWithNotice))
                {
                    ApprovedAdult approvedAdultWithNotice = new()
                    {
                        FirstName = nameWithNotice.Split(" ").FirstOrDefault(),
                        LastName = nameWithNotice.Split(" ").LastOrDefault(),
                        PhoneNumber = phoneWithNotice,
                        Relationship = relationshipWithNotice,
                        WithoutNotice = "N",
                        DateCreated = DateTime.Now,
                        DateModified = DateTime.Now
                    };
                    approvedAdults.Add(approvedAdultWithNotice);
                }

                // Without Notice
                string? nameWithoutNotice = formFields?.GetValueOrDefault($"Authorized Pick up without notice_{i}_Name") as string;
                string? phoneWithoutNotice = formFields?.GetValueOrDefault($"Authorized Pick up without notice_{i}_Phone Number") as string;
                string? relationshipWithoutNotice = formFields?.GetValueOrDefault($"Authorized Pick up without notice_{i}_Relationship to Child") as string;

                if (!string.IsNullOrEmpty(nameWithoutNotice))
                {
                    ApprovedAdult approvedAdultWithoutNotice = new()
                    {
                        FirstName = nameWithoutNotice.Split(" ").FirstOrDefault(),
                        LastName = nameWithoutNotice.Split(" ").LastOrDefault(),
                        PhoneNumber = phoneWithoutNotice,
                        Relationship = relationshipWithoutNotice,
                        WithoutNotice = "Y",
                        DateCreated = DateTime.Now,
                        DateModified = DateTime.Now
                    };
                    approvedAdults.Add(approvedAdultWithoutNotice);
                }
            }

            return approvedAdults;
        }


        public ApprovedPickupForm ApprovedPickupFormTableMapper(AggregatedChildAndFormData aggregatedData)
        {
            var formFields = aggregatedData.ApprovedPickupForm?.Fields;
            ApprovedPickupForm approvedPickupForm = new()
            {
                FormTemplateId = aggregatedData.ApprovedPickupForm?.FormTemplateId,
                ParentGuardian = null,
                ChildId = aggregatedData.ChildInfoData.ChildId,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            if (formFields != null)
            {
                formFields.TryGetValue("Parent / Guardian", out object? parentGuardian);

                approvedPickupForm.ParentGuardian = parentGuardian as string;
            }
            else
            {
                _logger.LogWarning("formFields in form: {FormId} is null for ChildId: {ChildId}",
                    aggregatedData.ApprovedPickupForm?.FormTemplateId, aggregatedData.ChildInfoData.ChildId);
            }

            return approvedPickupForm;
        }

        public Child ChildTableMapper(AggregatedChildAndFormData aggregatedData)
        {
            var formFields = aggregatedData.EmergencyContactForm?.Fields;
            Child child = new()
            {
                ChildId = aggregatedData.ChildInfoData.ChildId,
                ChildFirstName = aggregatedData.ChildInfoData.FirstName,
                ChildLastName = aggregatedData.ChildInfoData.LastName,
                ChildMiddleName = aggregatedData.ChildInfoData.MiddleName,
                Photo = aggregatedData.ChildInfoData.ProfilePhoto,
                BirthDate = aggregatedData.ChildInfoData.BirthDate,
                Gender = aggregatedData.ChildInfoData.Gender,
                PrimaryLanguage = aggregatedData.ChildInfoData.DominantLanguage,
                Hours = aggregatedData.ChildInfoData.HoursString,
                Allergies = formFields?["Medical Conditions / Allergies "] as string,
                ClassroomIds = aggregatedData.ChildInfoData.ClassroomIdsString,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            return child;
        }

        public List<EmergencyContact> EmergencyContactTableMapper(AggregatedChildAndFormData aggregatedData)
        {
            var formFields = aggregatedData.EmergencyContactForm?.Fields;
            List<EmergencyContact> emergencyContacts = new();

            // Loop through 1 to 3 for emergency contacts
            for (int i = 1; i <= 3; i++)
            {
                string? name = formFields?.GetValueOrDefault($"Emergency Contacts_{i}_Name") as string;
                string? phone = formFields?.GetValueOrDefault($"Emergency Contacts_{i}_Phone") as string;

                if (!string.IsNullOrEmpty(name))
                {
                    EmergencyContact emergencyContact = new()
                    {
                        FullName = name,
                        PhoneNumber = phone,
                        DateCreated = DateTime.Now,
                        DateModified = DateTime.Now
                    };

                    emergencyContacts.Add(emergencyContact);
                }
            }

            return emergencyContacts;
        }


        public EmergencyInformationForm EmergencyInformationFormTableMapper(AggregatedChildAndFormData aggregatedData)
        {
            EmergencyInformationForm emergencyInformationForm = new()
            {
                FormTemplateId = aggregatedData.EmergencyContactForm?.FormTemplateId,
                ChildId = aggregatedData.ChildInfoData.ChildId,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            return emergencyInformationForm;
        }

        public List<Parent> ParentTableMapper(AggregatedChildAndFormData aggregatedData)
        {
            var formFields = aggregatedData.EmergencyContactForm?.Fields;
            List<Parent> parents = new();

            for (int i = 1; i <= 2; i++)
            {
                string? lastName = formFields?.GetValueOrDefault($"Parent {i} Name.last") as string;
                string? firstName = formFields?.GetValueOrDefault($"Parent {i} Name.first") as string;
                string? phone = formFields?.GetValueOrDefault($"Parent {i} Mobile #") as string;
                string? email = formFields?.GetValueOrDefault($"Parent {i} Email Address") as string;

                if (!string.IsNullOrEmpty(lastName))
                {
                    Parent parent = new()
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        PhoneNumber = phone,
                        Email = email,
                        ParentSelector = i,
                        DateCreated = DateTime.Now,
                        DateModified = DateTime.Now
                    };

                    parents.Add(parent);
                }
            }

            return parents;
        }

        public PhotoReleaseForm PhotoReleaseFormTableMapper(AggregatedChildAndFormData aggregatedData)
        {
            var formFields = aggregatedData.PhotoReleaseForm?.Fields;
            PhotoReleaseForm photoReleaseForm = new()
            {
                FormTemplateId = aggregatedData.PhotoReleaseForm?.FormTemplateId,
                // Initialize other fields to default values
                Print = null,
                Website = null,
                Internal = null,
                Signature = null,
                ChildId = aggregatedData.ChildInfoData.ChildId,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            if (formFields != null)
            {
                formFields.TryGetValue("Print", out object? print);
                formFields.TryGetValue("Website", out object? website);
                formFields.TryGetValue("Internal", out object? internalObj);
                formFields.TryGetValue("Signature", out object? signature);

                photoReleaseForm.Print = print as string;
                photoReleaseForm.Website = website as string;
                photoReleaseForm.Internal = internalObj as string;
                photoReleaseForm.Signature = signature as string;
            }
            else
            {
                _logger.LogWarning("formFields in form: {FormId} is null for ChildId: {ChildId}", 
                    aggregatedData.PhotoReleaseForm?.FormTemplateId, aggregatedData.ChildInfoData.ChildId);
            }

            return photoReleaseForm;
        }


        public Programme ProgrammeTableMapper(AggregatedChildAndFormData aggregatedData)
        {
            string programmeName;
            // Process the information to create a Programme object
            if (aggregatedData.ChildInfoData.Program != null)
            {
                programmeName = MapperServices.GetProgramText(aggregatedData.ChildInfoData.Program);
            }
            else
            {
                programmeName = "Not listed";
            }
            string dismissalTime;
            if (aggregatedData.ChildInfoData.HoursString != null)
            {
                dismissalTime = MapperServices.GetDismissalTime(aggregatedData.ChildInfoData.HoursString);
            }
            else
            {
                dismissalTime = "Not listed";
            }

            Programme programme = new()
            {
                ProgrammeName = programmeName,
                DismissalTime = dismissalTime,
                ClassroomIds = aggregatedData.ChildInfoData.ClassroomIdsString,
                Ecp = MapperServices.IsEcp(programmeName),
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };
            return programme;
        }

        public GoingOutPermissionForm GoingOutPermissionFormTableMapper(AggregatedChildAndFormData aggregatedData)
        {
            var formFields = aggregatedData.GoingOutPermissionForm?.Fields;

            if (formFields == null)
            {
                _logger.LogWarning("No fields found for GoingOutPermissionForm for ChildId: {ChildId}", aggregatedData.ChildInfoData.ChildId);
                return new GoingOutPermissionForm // Return a non-null, empty object to avoid the warning
                {
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now
                };
            }

            GoingOutPermissionForm goingOutPermissionForm = new()
            {
                FormTemplateId = aggregatedData.GoingOutPermissionForm?.FormTemplateId,
                ChildId = aggregatedData.ChildInfoData.ChildId,
                Contact = formFields.GetValueOrDefault("Contact") as string,
                DriverAvailability = string.Join(", ", formFields.GetValueOrDefault("Driver Availability") as IEnumerable<object> ?? Enumerable.Empty<object>()),
                ParentGuardianSignature = formFields.GetValueOrDefault("Parent/Guardian Signature") as string,
                PermissionToParticipate = formFields.GetValueOrDefault("Permission to participate in Going Out") as string == "Y",
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            return goingOutPermissionForm;
        }


        public void MapAllTables(
                                AggregatedChildAndFormData aggregatedData,
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
                                out GoingOutPermissionForm goingOutPermissionForm)
        {
            try
            {
                child = ChildTableMapper(aggregatedData);
                programme = ProgrammeTableMapper(aggregatedData);
                childAddress = ChildAddressTableMapper(aggregatedData);
                parentAddresses = ParentAddressesTableMapper(aggregatedData);
                approvedAdults = ApprovedAdultTableMapper(aggregatedData);
                emergencyContacts = EmergencyContactTableMapper(aggregatedData);
                parents = ParentTableMapper(aggregatedData);
                animalApprovalForm = AnimalApprovalFormTableMapper(aggregatedData);
                approvedPickupForm = ApprovedPickupFormTableMapper(aggregatedData);
                emergencyInformationForm = EmergencyInformationFormTableMapper(aggregatedData);
                photoReleaseForm = PhotoReleaseFormTableMapper(aggregatedData);
                goingOutPermissionForm = GoingOutPermissionFormTableMapper(aggregatedData);

            }
            catch (KeyNotFoundException ex)
            {
                int childId = aggregatedData.ChildInfoData.ChildId;
                _logger.LogError("KeyNotFoundException for ChildId: {childId}. Exception: {ex}", childId, ex);

                child = new Child();
                programme = new Programme();
                childAddress = new Address();
                parentAddresses = new List<Address>();
                approvedAdults = new List<ApprovedAdult>();
                emergencyContacts = new List<EmergencyContact>();
                parents = new List<Parent>();
                animalApprovalForm = new AnimalApprovalForm();
                approvedPickupForm = new ApprovedPickupForm();
                emergencyInformationForm = new EmergencyInformationForm();
                photoReleaseForm = new PhotoReleaseForm();
                goingOutPermissionForm = new GoingOutPermissionForm();
            }
            catch (Exception ex)
            {
                int childId = aggregatedData.ChildInfoData.ChildId;
                _logger.LogError("General exception for ChildId: {childId}. Exception: {ex}", childId, ex);

                child = new Child();
                programme = new Programme();
                childAddress = new Address();
                parentAddresses = new List<Address>();
                approvedAdults = new List<ApprovedAdult>();
                emergencyContacts = new List<EmergencyContact>();
                parents = new List<Parent>();
                animalApprovalForm = new AnimalApprovalForm();
                approvedPickupForm = new ApprovedPickupForm();
                emergencyInformationForm = new EmergencyInformationForm();
                photoReleaseForm = new PhotoReleaseForm();
                goingOutPermissionForm = new GoingOutPermissionForm();
            }
        }
    }
}
