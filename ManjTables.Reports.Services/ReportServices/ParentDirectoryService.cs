using ManjTables.DataModels;
using ManjTables.Reports.ReportModels;
using ManjTables.SQLiteReaderLib.Connection;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace ManjTables.Reports.ReportServices
{
    public class ParentDirectoryService : IReportService<ParentDirectoryDto>
    {
        private readonly ManjTablesContext _dbContext;

        public ParentDirectoryService(ManjTablesContext dbContext)
        {
            _dbContext = dbContext;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }
        public void GenerateReportExcel(List<ParentDirectoryDto> reportDtos, string filePath, string classroomId)
        {
            using ExcelPackage package = new();
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Parent Directory");

            // Add two empty rows above the table
            worksheet.InsertRow(1, 4);

            // Add two empty columns to the left of the table
            worksheet.InsertColumn(1, 2);

            // Set title row values
            worksheet.Cells["C5"].Value = "Child Name";
            worksheet.Cells["D5"].Value = "Child Address";
            worksheet.Cells["E5"].Value = "Parent 1";
            worksheet.Cells["F5"].Value = "Parent 2";

            // Set title row font properties
            var titleRowRange = worksheet.Cells[5, 3, 5, 6];
            titleRowRange.Style.Font.Bold = true;
            titleRowRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            titleRowRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            int currentRow = 6;

            foreach (var dto in reportDtos)
            {
                worksheet.Cells["C" + currentRow].Value = dto.StudentName;
                worksheet.Cells["D" + currentRow].Value = dto.StudentAddress;
                worksheet.Cells["E" + currentRow].Value = dto.Parent1;
                worksheet.Cells["F" + currentRow].Value = dto.Parent2;
                worksheet.Cells[$"C{currentRow}:F{currentRow}"].Style.WrapText = true;

                currentRow++;
            }

            // Set worksheet cell format
            worksheet.Cells.Style.Font.Size = 16;
            double desiredHeight = 80; // Specify the desired height in points (1 point = 1/72 inch)
            titleRowRange.Style.Font.Size = 16;            

            foreach (var row in worksheet.Cells)
            {
                worksheet.Row(row.Start.Row).Height = desiredHeight;
            }
            worksheet.Row(5).Height = 30;

            // Set column widths for columns C to F
            worksheet.Column(3).Width = 30;
            worksheet.Column(4).Width = 45;
            worksheet.Column(5).Width = 45;
            worksheet.Column(6).Width = 45;

            // Add thick border between the title row and the rest of the rows
            titleRowRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

            // Set title row background color
            titleRowRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
            titleRowRange.Style.Fill.BackgroundColor.SetColor(Color.Yellow);

            // Merge cells C2 to F2
            worksheet.Cells["C3:F3"].Merge = true;

            // Add dotted borders between the inside rows and columns
            var insideTableRange = worksheet.Cells[6, 3, currentRow - 1, 6];
            insideTableRange.Style.Border.Top.Style = ExcelBorderStyle.Dotted;
            insideTableRange.Style.Border.Left.Style = ExcelBorderStyle.Dotted;
            insideTableRange.Style.Border.Right.Style = ExcelBorderStyle.Dotted;
            insideTableRange.Style.Border.Bottom.Style = ExcelBorderStyle.Dotted;

            // Format the inside rows and columns
            insideTableRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            insideTableRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            // Add thick borders around the table
            var tableRange = worksheet.Cells[5, 3, currentRow - 1, 6];
            tableRange.Style.Border.BorderAround(ExcelBorderStyle.Thick);

            ClassroomService classroomNameService = new(_dbContext);
            string classroomName = classroomNameService.GetClassroomName(classroomId);

            // Set the text and formatting for the merged cell
            worksheet.Cells["C3"].Value = $"{classroomName} - Parent Directory";
            worksheet.Cells["C3"].Style.Font.Size = 24;
            worksheet.Cells["C3"].Style.Font.Bold = true;
            worksheet.Cells["C3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells["C3"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            package.SaveAs(new FileInfo(filePath));
        }
        public List<ParentDirectoryDto> GetReportDtos(string classroomId)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            List<ParentDirectoryDto> parentDirectoryDtos = _dbContext.Childs
                                                            .Where(child => child.ClassroomIds.Contains(classroomId))
                                                            .Select(child => new ParentDirectoryDto
                                                            {
                                                                StudentName = child.ChildFirstName + " " + child.ChildLastName,
                                                                StudentAddress = child.Address.StreetAddress + Environment.NewLine +
                                                                                 child.Address.City + ", " + child.Address.State + " " + child.Address.ZipCode,
                                                                Parent1 = child.ChildParents.OrderBy(cp => cp.Parent.ParentSelector).Take(1)
                                                                          .Select(cp => cp.Parent.FirstName + " " + cp.Parent.LastName + Environment.NewLine +
                                                                                        cp.Parent.PhoneNumber + Environment.NewLine +
                                                                                        cp.Parent.Email).FirstOrDefault(),
                                                                Parent2 = child.ChildParents.OrderBy(cp => cp.Parent.ParentSelector).Skip(1).Take(1)
                                                                          .Select(cp => cp.Parent.FirstName + " " + cp.Parent.LastName + Environment.NewLine +
                                                                                        cp.Parent.PhoneNumber + Environment.NewLine +
                                                                                        cp.Parent.Email).FirstOrDefault()
                                                            }).ToList();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            return parentDirectoryDtos;
        }

        public void RunReport(string classroomId, string filePath)
        {
            List<ParentDirectoryDto> parentDirectoryDtos = GetReportDtos(classroomId);
            GenerateReportExcel(parentDirectoryDtos, filePath, classroomId);
        }

        // Unused overload
        public void RunReport(string classroomId, string filePath, DateTime selectedDate)
        {
            throw new NotImplementedException();
        }
    }
}
