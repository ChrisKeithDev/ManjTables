using ManjTables.DataModels;
using ManjTables.Reports.ReportModels;
using ManjTables.SQLiteReaderLib.Connection;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace ManjTables.Reports.ReportServices
{
    public class AllergiesPermissionsService : IReportService<AllergiesPermissionsDto>
    {
        private readonly ManjTablesContext _dbContext;

        public AllergiesPermissionsService(ManjTablesContext dbContext)
        {
            _dbContext = dbContext;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public List<AllergiesPermissionsDto> GetReportDtos(string classroomId)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8601 // Possible null reference assignment.
            List<AllergiesPermissionsDto> reportData = (from child in _dbContext.Childs
                              join animalForm in _dbContext.AnimalApprovalForms on child.ChildId equals animalForm.ChildId into animalForms
                              from animalForm in animalForms.DefaultIfEmpty()
                              join photoForm in _dbContext.PhotoReleaseForms on child.ChildId equals photoForm.ChildId into photoForms
                              from photoForm in photoForms.DefaultIfEmpty()
                              where child.ClassroomIds.Contains(classroomId)
                              select new AllergiesPermissionsDto
                              {
                                  StudentName = child.ChildFirstName + " " + child.ChildLastName,
                                  DismissalTime = child.Programme.DismissalTime, // Assuming Programme has a DismissalTime field
                                  Allergies = child.Allergies,
                                  AnimalPermission = animalForm != null && animalForm.Permission == "Y",
                                  PhotoPermission = (photoForm != null ? photoForm.Internal : "N")
                                        + (photoForm != null ? photoForm.Website : "N")
                                        + (photoForm != null ? photoForm.Print : "N")
                              }).ToList();
#pragma warning restore CS8601 // Possible null reference assignment.
#pragma warning restore CS8602 // Dereference of a possibly null reference.            
            return reportData;
        }

        public void GenerateReportExcel(List<AllergiesPermissionsDto> allergiesPermissionsDtos, string filePath, string classroomId)
        {
            using ExcelPackage package = new();
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Allergies and Permissions Report");

            // Add two empty rows above the table
            worksheet.InsertRow(1, 4);

            // Add two empty columns to the left of the table
            worksheet.InsertColumn(1, 2);

            // Set title row values
            worksheet.Cells["C5"].Value = "Student Name";
            worksheet.Cells["D5"].Value = "Dismissal Time";
            worksheet.Cells["E5"].Value = "Allergies";
            worksheet.Cells["F5"].Value = "Animal Permission";
            worksheet.Cells["G5"].Value = "Photo Permission";

            // Set title row font properties
            var titleRowRange = worksheet.Cells[5, 3, 5, 7];
            titleRowRange.Style.Font.Bold = true;
            titleRowRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            titleRowRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            // Sort data by StudentName before adding to Excel
            var sortedDtos = allergiesPermissionsDtos.OrderBy(dto => dto.StudentName).ToList();

            // Add data to the worksheet
            int rowStart = 6;
            foreach (var dto in sortedDtos)
            {
                worksheet.Cells[$"C{rowStart}"].Value = dto.StudentName;
                worksheet.Cells[$"D{rowStart}"].Value = dto.DismissalTime;
                worksheet.Cells[$"E{rowStart}"].Value = dto.Allergies;
                worksheet.Cells[$"F{rowStart}"].Value = dto.AnimalPermission ? "Yes" : "No";
                worksheet.Cells[$"G{rowStart}"].Value = PhotoPermissionString(dto.PhotoPermission);
                rowStart++;
            }

            // Set worksheet cell format
            worksheet.Cells.Style.Font.Size = 16;
            double desiredHeight = 30; // Specify the desired height in points (1 point = 1/72 inch)

            foreach (var row in worksheet.Cells)
            {
                worksheet.Row(row.Start.Row).Height = desiredHeight;
            }

            // Auto-fit column widths for columns C, D, E, F, and G
            worksheet.Cells[5, 3, worksheet.Dimension.End.Row, 7].AutoFitColumns();

            // Add thick border between the title row and the rest of the rows
            titleRowRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

            // Set title row background color
            titleRowRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
            titleRowRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow);

            // Merge cells C2, D2, E2, F2, and G2
            worksheet.Cells["C3:G3"].Merge = true;

            // Add dotted borders between the inside rows and columns
            var insideTableRange = worksheet.Cells[6, 3, allergiesPermissionsDtos.Count + 5, 7];
            insideTableRange.Style.Border.Top.Style = ExcelBorderStyle.Dotted;
            insideTableRange.Style.Border.Left.Style = ExcelBorderStyle.Dotted;
            insideTableRange.Style.Border.Right.Style = ExcelBorderStyle.Dotted;
            insideTableRange.Style.Border.Bottom.Style = ExcelBorderStyle.Dotted;

            // Add thick borders around the table
            var tableRange = worksheet.Cells[5, 3, allergiesPermissionsDtos.Count + 5, 7];
            tableRange.Style.Border.BorderAround(ExcelBorderStyle.Thick);

            ClassroomService classroomNameService = new(_dbContext);
            string classroomName = classroomNameService.GetClassroomName(classroomId);

            // Set the text and formatting for the merged cell
            worksheet.Cells["C3"].Value = $"{classroomName} - Dismissals, Allergies, and Permissions";
            worksheet.Cells["C3"].Style.Font.Size = 24;
            worksheet.Cells["C3"].Style.Font.Bold = true;
            worksheet.Cells["C3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells["C3"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            package.SaveAs(new FileInfo(filePath));
        }

        public void RunReport(string classroomId, string filePath)
        {
            List<AllergiesPermissionsDto> allergiesPermissionsDtos = GetReportDtos(classroomId);
            GenerateReportExcel(allergiesPermissionsDtos, filePath, classroomId);
        }

        private static string PhotoPermissionString(string photoPermission)
        {
            photoPermission ??= "NNN";

            return photoPermission switch
            {
                "YYY" => "Yes to all",
                "YYN" => "No Print",
                "YNY" => "No Website",
                "YNN" => "Internal Only",
                "NYY" => "No Internal",
                "NYN" => "Website Only",
                "NNY" => "Print Only",
                _ => "Missing Form",
            };
        }

        // unused overloads
        public void RunReport(string classroomId, string filePath, DateTime selectedDate)
        {
            throw new NotImplementedException();
        }
    }
}
