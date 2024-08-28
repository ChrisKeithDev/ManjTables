using ManjTables.Reports.ReportSheets;

namespace ManjTables.Reports.ReportModels
{
    public class AllergiesPermissionsSheet : IReportSheet
    {
        public string Filename { get; set; }
        public string SheetTitle { get; set; }
        public DateTime Date { get; set; }

        public List<string> ColumnHeaders { get; } = new List<string>
        {
            "Student Name",
            "Dismissal Time",
            "Allergies",
            "Animal Permission",
            "Photo Permission"
        };

        public int ColumnCount => ColumnHeaders.Count;

        // This is the list of DTOs that will populate the rows of the report
        public List<AllergiesPermissionsDto> ReportData { get; set; }

        public AllergiesPermissionsSheet()
        {
            // You can set default values here if needed
            Filename = "AllergiesPermissionsReport.xlsx";
            SheetTitle = "Allergies and Permissions Report";
            Date = DateTime.Today;
            ReportData = new List<AllergiesPermissionsDto>();
        }

        // Additional methods to add data, generate the report, etc.
    }
}
