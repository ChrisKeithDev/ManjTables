using ManjTables.DataModels;
using ManjTables.Reports.ReportModels;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace ManjTables.Reports.ReportServices
{
    public class SchoolEnrollmentService : IReportService<SchoolEnrollmentDto>
    {
        private readonly ManjTablesContext _dbContext;

        public SchoolEnrollmentService(ManjTablesContext dbContext)
        {
            _dbContext = dbContext;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }
        public void GenerateReportExcel(List<SchoolEnrollmentDto> reportDtos, string filePath, string classroomId)
        {
            using ExcelPackage package = new();
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("School Enrollment Report");

            // Add two empty rows above the table
            worksheet.InsertRow(1, 4);

            // Add two empty columns to the left of the table
            worksheet.InsertColumn(1, 2);

            // Set title row values
            worksheet.Cells["C5"].Value = "First Name";
            worksheet.Cells["D5"].Value = "Last Name";
            worksheet.Cells["E5"].Value = "Program";
            worksheet.Cells["F5"].Value = "Teacher";

            // Set title row font properties
            var titleRowRange = worksheet.Cells[5, 3, 5, 6];
            titleRowRange.Style.Font.Bold = true;
            titleRowRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            titleRowRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            int currentRow = 6;
            int primaryCount = 0;
            int toddlerCount = 0;
            int elementaryCount = 0;

            var sortedDtos = reportDtos.OrderBy(dto => dto.StudentFirstName).ToList();

            foreach (var dto in sortedDtos)
            {
                worksheet.Cells["C" + currentRow].Value = dto.StudentFirstName;
                worksheet.Cells["D" + currentRow].Value = dto.StudentLastName;
                worksheet.Cells["E" + currentRow].Value = dto.Program;
                worksheet.Cells["F" + currentRow].Value = dto.Instructor;
                //worksheet.Cells[$"C{currentRow}:F{currentRow}"].Style.WrapText = true;
                worksheet.Row(currentRow).Height = 30;

                // Apply dotted lines for the current row
                var rowRange = worksheet.Cells[currentRow, 3, currentRow, 6];
                rowRange.Style.Border.Top.Style = ExcelBorderStyle.Dotted;
                rowRange.Style.Border.Left.Style = ExcelBorderStyle.Dotted;
                rowRange.Style.Border.Right.Style = ExcelBorderStyle.Dotted;
                rowRange.Style.Border.Bottom.Style = ExcelBorderStyle.Dotted;

                if (dto.Program != null && dto.Program.Contains("Primary"))
                {
                    primaryCount++;
                }
                else if (dto.Program != null && dto.Program.Contains("Toddler"))
                {
                    toddlerCount++;
                }
                else if (dto.Program != null && dto.Program.Contains("Elementary"))
                {
                    elementaryCount++;
                }

                currentRow++;
            }
            // Set worksheet cell format
            worksheet.Cells.Style.Font.Size = 16;

            // Auto-fit column widths for columns C, D, E, and F
            worksheet.Cells[5, 3, worksheet.Dimension.End.Row, 6].AutoFitColumns();

            // Add thick border between the title row and the rest of the rows
            titleRowRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

            // Set title row background color
            titleRowRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
            titleRowRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow);

            worksheet.Column(3).AutoFit();
            worksheet.Column(4).AutoFit();
            worksheet.Column(5).AutoFit();
            worksheet.Column(6).AutoFit();

            // Merge cells C2, D2, E2, and F2
            worksheet.Cells["C3:F3"].Merge = true;

            // Add thick borders around the table
            var tableRange = worksheet.Cells[5, 3, currentRow, 6];
            tableRange.Style.Border.BorderAround(ExcelBorderStyle.Thick);

            // Set the text and formatting for the merged cell
            worksheet.Cells["C3"].Value = "School Enrollment by Program";
            worksheet.Cells["C3"].Style.Font.Size = 24;
            worksheet.Cells["C3"].Style.Font.Bold = true;
            worksheet.Cells["C3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells["C3"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            // Tally total students
            int totalStudents = primaryCount + toddlerCount + elementaryCount;

            // Add tally below the table
            worksheet.Cells[currentRow + 2, 3].Value = $"{primaryCount} Primary";
            worksheet.Cells[currentRow + 3, 3].Value = $"{toddlerCount} Toddlers";
            worksheet.Cells[currentRow + 4, 3].Value = $"{elementaryCount} Elementary";
            worksheet.Cells[currentRow + 5, 3].Value = $"{totalStudents} Total";

            // Set font properties for tally cells
            var tallyRange = worksheet.Cells[currentRow + 2, 3, currentRow + 5, 3];
            tallyRange.Style.Font.Bold = true;
            tallyRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            tallyRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            tallyRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
            tallyRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);

            package.SaveAs(new FileInfo(filePath));
        }

        public List<SchoolEnrollmentDto> GetReportDtos(string stub)
        {
            // Fetch all children with their programs
            var childrenWithPrograms = _dbContext.Childs
                .Include(child => child.Programme)
                .ToList();

            // Fetch all staff classrooms with staff members
            var staffClassrooms = _dbContext.StaffClassrooms
                .Include(sc => sc.StaffMember)
                .ToList();

            var enrollmentDtos = new List<SchoolEnrollmentDto>();

            foreach (var child in childrenWithPrograms)
            {
                if (string.IsNullOrEmpty(child.ChildFirstName))
                {
                    continue;
                }
                // Get the first ClassroomId from the Programme
                var firstClassroomId = child.Programme?.ClassroomIds?
                    .Split(',')
                    .Select(id => id.Trim())
                    .FirstOrDefault();

                // Find the staff member for the first classroom
                var staffMember = firstClassroomId != null
                    ? staffClassrooms.FirstOrDefault(sc => sc.ClassroomId == firstClassroomId)?.StaffMember
                    : null;

                // Create the DTO
                var dto = new SchoolEnrollmentDto
                {
                    StudentFirstName = child.ChildFirstName,
                    StudentLastName = child.ChildLastName,
                    Program = child.Programme?.ProgrammeName ?? "N/A",
                    Instructor = staffMember != null ? staffMember.FirstName + " " + staffMember.LastName : "N/A"
                };

                enrollmentDtos.Add(dto);
            }

            return enrollmentDtos;
        }

        public void RunReport(string stub, string filePath)
        {            
            List<SchoolEnrollmentDto> enrollmentDtos = GetReportDtos(stub);
            GenerateReportExcel(enrollmentDtos, filePath, stub);
        }

        // Unused Overload
        public void RunReport(string classroomId, string filePath, DateTime selectedDate)
        {
            throw new NotImplementedException();
        }
    }
}