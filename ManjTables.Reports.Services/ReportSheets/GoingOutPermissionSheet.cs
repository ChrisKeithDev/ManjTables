using ManjTables.Reports.ReportModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManjTables.Reports.ReportSheets
{
    public class GoingOutPermissionSheet : IReportSheet
    {
        public string Filename { get; set; }
        public string SheetTitle { get; set; }
        public DateTime Date { get; set; }

        public List<string> ColumnHeaders { get; } = new List<string>
    {
        "Child Name",
        "Contact",
        "Driver Availability",
        "Parent Signature",
        "Permission to Participate"
    };

        public int ColumnCount => ColumnHeaders.Count;

        public List<GoingOutPermissionDto> ReportData { get; set; }

        public GoingOutPermissionSheet()
        {
            Filename = "GoingOutPermissionReport.xlsx";
            SheetTitle = "Going Out Permission Report";
            Date = DateTime.Today;
            ReportData = new List<GoingOutPermissionDto>();
        }
    }

}
