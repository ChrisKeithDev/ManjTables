using ManjTables.DataModels;
using ManjTables.Reports.ReportModels;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace ManjTables.Reports.ReportServices
{
    public class GoingOutPermissionService : IReportService<GoingOutPermissionDto>
    {
        private readonly ManjTablesContext _dbContext;

        public GoingOutPermissionService(ManjTablesContext dbContext)
        {
            _dbContext = dbContext;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public List<GoingOutPermissionDto> GetReportDtos(string classroomId)
        {
            // Only generate the report if the classroom ID is 2936 (Elementary classroom)
            if (classroomId != "2936")
            {
                return new List<GoingOutPermissionDto>();
            }

            var reportData = (from child in _dbContext.Childs
                              join permissionForm in _dbContext.GoingOutPermissionForms
                                  on child.ChildId equals permissionForm.ChildId into permissionForms
                              from permissionForm in permissionForms.DefaultIfEmpty()
                              where child.ClassroomIds != null
                                  && child.ClassroomIds.Contains(classroomId)
                                  && permissionForm.ParentGuardianSignature != null
                              select new GoingOutPermissionDto
                              {
                                  ChildName = $"{child.ChildFirstName} {child.ChildLastName}",
                                  Contact = permissionForm.Contact ?? "Unknown",
                                  DriverAvailability = permissionForm.DriverAvailability ?? "Unknown",
                                  PermissionToParticipate = permissionForm.PermissionToParticipate == true ? "Y" : "N"
                              }).ToList();

            return reportData;
        }

        public void GenerateReportExcel(List<GoingOutPermissionDto> reportDtos, string filePath, string classroomId)
        {
            using ExcelPackage package = new();
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Going Out Permission Report");

            // Add two empty rows above the table
            worksheet.InsertRow(1, 4);

            // Add two empty columns to the left of the table
            worksheet.InsertColumn(1, 2);

            // Set title row values
            worksheet.Cells["C5"].Value = "Child Name";
            worksheet.Cells["D5"].Value = "Contact";
            worksheet.Cells["E5"].Value = "Driver Availability";
            worksheet.Cells["F5"].Value = "Permission to Participate";

            // Set title row font properties
            var titleRowRange = worksheet.Cells[5, 3, 5, 6];
            titleRowRange.Style.Font.Bold = true;
            titleRowRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            titleRowRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            int currentRow = 6;
            bool isAlternateRow = false;

            Color accentColor = Color.FromArgb(174, 199, 149);

            var sortedDtos = reportDtos
                .OrderBy(dto => dto.ChildName)
                .ToList();

            // Add data to the worksheet
            foreach (var dto in sortedDtos)
            {
                worksheet.Cells["C" + currentRow].Value = dto.ChildName;
                worksheet.Cells["D" + currentRow].Value = dto.Contact;
                worksheet.Cells["E" + currentRow].Value = dto.DriverAvailability.Replace("I am usually available ", "").Replace(", ", "\n"); // Strip prefix and newline each availability
                worksheet.Cells["F" + currentRow].Value = dto.PermissionToParticipate == "Y" ? "Yes" : "No";

                // Set row background color
                var rowRange = worksheet.Cells[currentRow, 3, currentRow, 6];
                rowRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rowRange.Style.Fill.BackgroundColor.SetColor(isAlternateRow ? accentColor : Color.White);

                // Set row font properties
                rowRange.Style.Font.Bold = false;
                rowRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                rowRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                currentRow++;
                isAlternateRow = !isAlternateRow;
            }

            // Set worksheet cell format
            worksheet.Cells.Style.Font.Size = 16;

            // Enable text wrapping for availability column
            worksheet.Column(5).Style.WrapText = true;

            // Enable text wrapping for contact column
            worksheet.Column(4).Style.WrapText = true;

            // Set a minimum width for the columns
            worksheet.Column(3).AutoFit();
            worksheet.Column(4).Width = 35; // Adjust as needed
            worksheet.Column(5).Width = 50; // Adjust as needed
            worksheet.Column(6).Width = 32;

            // Add thick border between the title row and the rest of the rows
            titleRowRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

            // Set title row background color
            titleRowRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
            titleRowRange.Style.Fill.BackgroundColor.SetColor(Color.Yellow);

            // Merge cells C2, D2, E2, and F2
            worksheet.Cells["C3:F3"].Merge = true;

            // Add thick borders around the table
            var tableRange = worksheet.Cells[5, 3, currentRow, 6];
            tableRange.Style.Border.BorderAround(ExcelBorderStyle.Thick);

            ClassroomService classroomNameService = new(_dbContext);
            string classroomName = classroomNameService.GetClassroomName(classroomId);

            // Set the text and formatting for the merged cell
            worksheet.Cells["C3"].Value = $"{classroomName} - Going Out Permissions";
            worksheet.Cells["C3"].Style.Font.Size = 24;
            worksheet.Cells["C3"].Style.Font.Bold = true;
            worksheet.Cells["C3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells["C3"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            worksheet.Cells["C5:F5"].Style.Font.Size = 16;
            worksheet.Row(5).Height = 30;

            package.SaveAs(new FileInfo(filePath));
        }


        public void RunReport(string classroomId, string filePath)
        {
            // Only run the report for the elementary classroom with ID 2936
            if (classroomId == "2936")
            {
                var reportData = GetReportDtos(classroomId);
                GenerateReportExcel(reportData, filePath, classroomId);
            }
        }

        // Unused overloads
        public void RunReport(string classroomId, string filePath, DateTime selectedDate)
        {
            throw new NotImplementedException();
        }
    }
}
