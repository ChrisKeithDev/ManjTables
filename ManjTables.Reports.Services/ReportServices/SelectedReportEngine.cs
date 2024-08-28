using ManjTables.DataModels;
using ManjTables.Reports.ReportModels;
using ManjTables.SQLiteReaderLib.Connection;
using OfficeOpenXml;

namespace ManjTables.Reports.ReportServices
{
    public class SelectedReportEngine
    {
        public bool CbAgesChecked;
        public bool CbAllergiesChecked;
        public bool CbApprovedPickupsChecked;
        public bool CbEcpDismissalChecked;
        public bool CbEmergencyChecked;
        public bool CbEnrollmentChecked;
        public bool CbGoingOutPermissionChecked;
        public bool CbMailingListChecked;
        public bool CbParentDirChecked;
        public bool CbStudentZipCodesChecked;
        public bool CbAllClassroomsChecked;
        public bool CbAllFilesChecked;
        private string? folderPath;
        private string? folderName;
        private readonly string lockFilePath = @"O:\Apps From Chris\Data\ManjTables Data\db.lock";
        private readonly string dbPath = @"O:\Apps From Chris\Data\ManjTables Data\manjtables.db";
        private string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private static readonly string currentDate = DateTime.Now.ToString("MMMM d, yyyy - h.mmtt");
        private readonly AgeAtDateService? _ageAtDateService;
        private readonly AllergiesPermissionsService? _allergiesPermissionsService;
        private readonly ApprovedPickupsService? _approvedPickupsReportService;
        private readonly EcpDismissalService? _ecpDismissalService;
        private readonly EmergencyContactsService? _emergencyContactsService;
        private readonly GoingOutPermissionService? _goingOutPermissionService;
        private readonly MailingListService? _mailingListService;
        private readonly ParentDirectoryService? _parentDirectoryService;
        private readonly SchoolEnrollmentService? _schoolEnrollmentService;
        public event EventHandler<string>? StatusUpdated;
        private IDatabaseConnection? _databaseConnection;

        const int maxAttempts = 5;
        const int delay = 5000;

        

        public SelectedReportEngine(
            AgeAtDateService?           ageAtDateService,               AllergiesPermissionsService? allergiesPermissionsService,
            ApprovedPickupsService?     approvedPickupsReportService,   EcpDismissalService?        ecpDismissalService,
            EmergencyContactsService?   emergencyContactsService, GoingOutPermissionService? goingOutPermissionService,       MailingListService?         mailingListService,
            ParentDirectoryService?     parentDirectoryService,         SchoolEnrollmentService?    schoolEnrollmentService)
        {
            ExcelPackage.LicenseContext     = LicenseContext.NonCommercial;
            _ageAtDateService               = ageAtDateService;
            _allergiesPermissionsService    = allergiesPermissionsService;
            _approvedPickupsReportService   = approvedPickupsReportService;
            _ecpDismissalService            = ecpDismissalService;
            _emergencyContactsService       = emergencyContactsService;
            _goingOutPermissionService      = goingOutPermissionService;
            _mailingListService             = mailingListService;
            _parentDirectoryService         = parentDirectoryService;
            _schoolEnrollmentService        = schoolEnrollmentService;
        }

        public void GenerateReports(DateTime selectedDate, List<ClassroomDto> selectedClassrooms)
        {
            if (!WaitForLockRelease())
            {
                UpdateStatus("Unable to access the database, please wait then try again");
                return;
            }

            CreateLockFile();
            try
            {
                folderPath = GenerateFolder();

                _databaseConnection = new DatabaseConnection();

                _databaseConnection.Open(dbPath);

                if (CbAllFilesChecked || CbEnrollmentChecked)
                {
                    string filePath = Path.Combine(folderPath, "School Enrollment.xlsx");
                    _schoolEnrollmentService?.RunReport("All", filePath);
                }
                if (CbAllFilesChecked || CbMailingListChecked)
                {
                    string filePath = Path.Combine(folderPath, "Mailing List.csv");
                    _mailingListService?.RunReport("All", filePath);
                }
                if (CbAllFilesChecked || CbEcpDismissalChecked)
                {
                    string filePath = Path.Combine(folderPath, "ECP Dismissal.xlsx");
                    _ecpDismissalService?.RunReport("All", filePath);
                }


                foreach (var classroom in selectedClassrooms)
                {
                    // Create a subfolder for each classroom
                    string classroomFolderPath = BuildFolders(classroom.ClassroomName);
                    string filePath;
                    if (CbAgesChecked || CbAllFilesChecked)
                    {
                        filePath = Path.Combine(classroomFolderPath, GenerateFileName(classroom.ClassroomName, "Age Report"));
                        _ageAtDateService?.RunReport(classroom.ClassroomId, filePath, selectedDate);
                    }
                    if (CbAllergiesChecked || CbAllFilesChecked)
                    {
                        filePath = Path.Combine(classroomFolderPath, GenerateFileName(classroom.ClassroomName, "Allergies Report"));
                        _allergiesPermissionsService?.RunReport(classroom.ClassroomId, filePath);
                    }
                    if (CbApprovedPickupsChecked || CbAllFilesChecked)
                    {
                        filePath = Path.Combine(classroomFolderPath, GenerateFileName(classroom.ClassroomName, "Approved Pickups Report"));
                        _approvedPickupsReportService?.RunReport(classroom.ClassroomId, filePath);
                    }
                    if (CbEmergencyChecked || CbAllFilesChecked)
                    {
                        filePath = Path.Combine(classroomFolderPath, GenerateFileName(classroom.ClassroomName, "Emergency Contacts Report"));
                        _emergencyContactsService?.RunReport(classroom.ClassroomId, filePath);
                    }
                    if (CbGoingOutPermissionChecked || CbAllFilesChecked)
                    {
                        filePath = Path.Combine(classroomFolderPath, GenerateFileName(classroom.ClassroomName, "Going Out Permission Report"));
                        _goingOutPermissionService?.RunReport(classroom.ClassroomId, filePath);
                    }
                    if (CbMailingListChecked || CbAllFilesChecked)
                    {
                        filePath = Path.Combine(classroomFolderPath, GenerateCsvFilename(classroom.ClassroomName, "Mailing List"));
                        _mailingListService?.RunReport(classroom.ClassroomId, filePath);
                    }
                    if (CbParentDirChecked || CbAllFilesChecked)
                    {
                        filePath = Path.Combine(classroomFolderPath, GenerateFileName(classroom.ClassroomName, "Parent Directory"));
                        _parentDirectoryService?.RunReport(classroom.ClassroomId, filePath);
                    }
                }
                _databaseConnection.Close();
            }
            finally
            {
                DeleteLockFile();
            }
        }


        private string GenerateFolder()
        {
            folderName = "ManjReports - " + DateTime.Now.ToString("MMMM d, yyyy - h.mmtt");
            desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            folderPath = Path.Combine(desktopPath, folderName);
            Directory.CreateDirectory(folderPath);
            return folderPath;
        }

        private string BuildFolders(string classroomName)
        {
            if (folderPath != null)
            {
                string classroomFolderPath = Path.Combine(folderPath, classroomName);
                Directory.CreateDirectory(classroomFolderPath);
                return classroomFolderPath;
            }
            throw new NullReferenceException("folderPath is null");
        }

        private static string GenerateFileName(string classroomName, string reportName)
        {
            return $"{reportName} - {classroomName} - {currentDate}.xlsx";
        }

        private static string GenerateCsvFilename(string classroomName, string reportName)
        {
            return $"{reportName} - {classroomName} - {currentDate}.csv";
        }

        private void UpdateStatus(string status)
        {
            StatusUpdated?.Invoke(this, status);
        }

        private void CreateLockFile()
        {
            if (!File.Exists(lockFilePath))
            {
                File.Create(lockFilePath).Dispose();
            }
        }

        private void DeleteLockFile()
        {
            if (File.Exists(lockFilePath))
            {
                File.Delete(lockFilePath);
            }
        }

        private bool WaitForLockRelease()
        {
            int attempts = 0;
            while (File.Exists(lockFilePath) && attempts < maxAttempts)
            {
                UpdateStatus("The database is being updated, just a moment please");
                Thread.Sleep(delay);
                attempts++;
            }
            return attempts < maxAttempts;
        }

    }
}
