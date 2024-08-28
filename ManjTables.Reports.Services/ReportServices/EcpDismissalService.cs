using ManjTables.DataModels;
using ManjTables.Reports.ReportModels;
using ManjTables.SQLiteReaderLib.Connection;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace ManjTables.Reports.ReportServices
{
    public class EcpDismissalService : IReportService<EcpDismissalDto>
    {
        private readonly ManjTablesContext _dbContext;

        public EcpDismissalService(ManjTablesContext dbContext)
        {
            _dbContext = dbContext;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public List<EcpDismissalDto> GetReportDtos(string stub)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8601 // Possible null reference assignment.
            List<EcpDismissalDto> reportData = (from child in _dbContext.Childs
                                                where child.Programme != null && child.Programme.ProgrammeName == "Primary ECP"
                                                select new EcpDismissalDto
                                                {
                                                    StudentName = child.ChildFirstName + " " + child.ChildLastName,
                                                    Program = child.Programme.ProgrammeName, // Accessing ProgrammeName
                                                    DismissalTime = child.Programme.DismissalTime // Accessing DismissalTime from Programme
                                                }).ToList();
#pragma warning restore CS8601 // Possible null reference assignment.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            return reportData;
        }

        public void GenerateReportExcel(List<EcpDismissalDto> reportDtos, string filePath, string stub)
        {
            using ExcelPackage package = new();
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("ECP Dismissal Report");

            // Add two empty rows above the table
            worksheet.InsertRow(1, 4);

            // Add two empty columns to the left of the table
            worksheet.InsertColumn(1, 2);

            // Set title row values
            worksheet.Cells["C5"].Value = "Child Name";
            worksheet.Cells["D5"].Value = "Program";
            worksheet.Cells["E5"].Value = "Dismissal Time";

            // Set title row font properties
            var titleRowRange = worksheet.Cells[5, 3, 5, 5];
            titleRowRange.Style.Font.Bold = true;
            titleRowRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            titleRowRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            int currentRow = 6;

            // Add data to the worksheet
            foreach (var dto in reportDtos)
            {
                worksheet.Cells["C" + currentRow].Value = dto.StudentName;
                worksheet.Cells["D" + currentRow].Value = dto.Program;
                worksheet.Cells["E" + currentRow].Value = dto.DismissalTime;
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
            titleRowRange.Style.Fill.BackgroundColor.SetColor(Color.Yellow);

            // Merge cells C2, D2, and E2
            worksheet.Cells["C3:E3"].Merge = true;

            // Add dotted borders between the inside rows and columns
            var insideTableRange = worksheet.Cells[6, 3, currentRow - 1, 5];
            insideTableRange.Style.Border.Top.Style = ExcelBorderStyle.Dotted;
            insideTableRange.Style.Border.Left.Style = ExcelBorderStyle.Dotted;
            insideTableRange.Style.Border.Right.Style = ExcelBorderStyle.Dotted;
            insideTableRange.Style.Border.Bottom.Style = ExcelBorderStyle.Dotted;

            // Add thick borders around the table
            var tableRange = worksheet.Cells[5, 3, currentRow, 5];
            tableRange.Style.Border.BorderAround(ExcelBorderStyle.Thick);

            // Set the text and formatting for the merged cell
            worksheet.Cells["C3"].Value = "Primary ECP Dismissal Times";
            worksheet.Cells["C3"].Style.Font.Size = 24;
            worksheet.Cells["C3"].Style.Font.Bold = true;
            worksheet.Cells["C3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells["C3"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            worksheet.Cells[$"E6:E{currentRow - 1}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            package.SaveAs(new FileInfo(filePath));
        }

        public void RunReport(string stub, string filePath)
        {
            List<EcpDismissalDto> ecpDismissalDtos = GetReportDtos(stub);
            GenerateReportExcel(ecpDismissalDtos, filePath, stub);
        }

        // unused overload
        public void RunReport(string classroomId, string filePath, DateTime selectedDate)
        {
            throw new NotImplementedException();
        }
    }
}
