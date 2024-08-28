using ManjTables.DataModels;
using ManjTables.Reports.ReportModels;
using ManjTables.SQLiteReaderLib.Connection;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace ManjTables.Reports.ReportServices
{
    public class AgeAtDateService : IReportService<AgeAtDateDto>
    {
        private readonly ManjTablesContext _dbContext;
        private DateTime _selectedDate;

        public AgeAtDateService(ManjTablesContext dbContext)
        {
            _dbContext = dbContext;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public List<AgeAtDateDto> GetReportDtos(string classroomId)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.
            List<AgeAtDateDto> reportData = (from child in _dbContext.Childs
                                             where child.ClassroomIds.Contains(classroomId)
                                             select new AgeAtDateDto
                                             {
                                                 StudentName = child.ChildFirstName + " " + child.ChildLastName,
                                                 Age = GetAge(child.BirthDate, _selectedDate),
                                                 Birthdate = child.BirthDate
                                             }).ToList();
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            return reportData;
        }

        public void GenerateReportExcel(List<AgeAtDateDto> reportDtos, string filePath, string classroomId)
        {
            using ExcelPackage package = new();
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Children Age Data");

            // Add two empty rows above the table
            worksheet.InsertRow(1, 4);

            // Add two empty columns to the left of the table
            worksheet.InsertColumn(1, 2);

            // Set title row values
            worksheet.Cells["C5"].Value = "Child Name";
            worksheet.Cells["D5"].Value = $"Age at {_selectedDate:MM-dd-yyyy}";
            worksheet.Cells["E5"].Value = "Birthdate";

            // Set title row font properties
            var titleRowRange = worksheet.Cells[5, 3, 5, 5];
            titleRowRange.Style.Font.Bold = true;
            titleRowRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            titleRowRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            int currentRow = 6;
            // Add child data to the worksheet
            foreach (var dto in reportDtos)
            {
                worksheet.Cells["C" + currentRow].Value = dto.StudentName;
                worksheet.Cells["D" + currentRow].Value = dto.Age;
                worksheet.Cells["D" + currentRow].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["E" + currentRow].Value = dto.Birthdate;
                currentRow++;
            }

            // Set worksheet cell format
            worksheet.Cells.Style.Font.Size = 16;
            double desiredHeight = 30; // Specify the desired height in points (1 point = 1/72 inch)

            foreach (var row in worksheet.Cells)
            {
                worksheet.Row(row.Start.Row).Height = desiredHeight;
            }

            // Auto-fit column widths for columns C, D, and E
            worksheet.Cells[5, 3, worksheet.Dimension.End.Row, 5].AutoFitColumns();

            // Add thick border between the title row and the rest of the rows
            titleRowRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

            // Set title row background color
            titleRowRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
            titleRowRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow);

            // Merge cells C2, D2, and E2
            worksheet.Cells["C3:E3"].Merge = true;

            // Add dotted borders between the inside rows and columns
            var insideTableRange = worksheet.Cells[6, 3, reportDtos.Count + 5, 5];
            insideTableRange.Style.Border.Top.Style = ExcelBorderStyle.Dotted;
            insideTableRange.Style.Border.Left.Style = ExcelBorderStyle.Dotted;
            insideTableRange.Style.Border.Right.Style = ExcelBorderStyle.Dotted;
            insideTableRange.Style.Border.Bottom.Style = ExcelBorderStyle.Dotted;

            // Add thick borders around the table
            var tableRange = worksheet.Cells[5, 3, reportDtos.Count + 5, 5];
            tableRange.Style.Border.BorderAround(ExcelBorderStyle.Thick);

            ClassroomService classroomNameService = new(_dbContext);
            string classroomName = classroomNameService.GetClassroomName(classroomId);

            // Set the text and formatting for the merged cell
            worksheet.Cells["C3"].Value = $"{classroomName} - Age at Date";
            worksheet.Cells["C3"].Style.Font.Size = 24;
            worksheet.Cells["C3"].Style.Font.Bold = true;
            worksheet.Cells["C3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells["C3"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            package.SaveAs(new FileInfo(filePath));
        }

        public void RunReport(string classroomId, string filePath, DateTime selectedDate)
        {
            _selectedDate = selectedDate;
            List<AgeAtDateDto> ageAtDateDtos = GetReportDtos(classroomId);
            GenerateReportExcel(ageAtDateDtos, filePath, classroomId);
        }

        public static string GetAge(string birthDate, DateTime selectedDate)
        {
            if (string.IsNullOrEmpty(birthDate))
            {
                return "Unknown";
            }
            DateTime? birthDateAsDateTime = null;
            if (DateTime.TryParse(birthDate, out DateTime birthDateAsDateTimeTemp))
            {
                birthDateAsDateTime = birthDateAsDateTimeTemp;
            }

            if (birthDateAsDateTime.HasValue)
            {
                int years = selectedDate.Year - birthDateAsDateTime.Value.Year;
                int months = selectedDate.Month - birthDateAsDateTime.Value.Month;

                // Check if the birthdate has not reached this month yet
                if (selectedDate.Day < birthDateAsDateTime.Value.Day)
                {
                    months--;
                }

                // Adjust for negative months (i.e., the birthdate is in the future of the asOfDate)
                if (months < 0)
                {
                    years--;
                    months += 12;
                }

                return $"{years}yrs {months}mo";
            }

            return "Unknown";
        }

        

        // unused overloads
        public void RunReport(string classroomId, string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
