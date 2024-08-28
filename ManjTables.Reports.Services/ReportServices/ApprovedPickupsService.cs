using ManjTables.DataModels;
using ManjTables.Reports.ReportModels;
using ManjTables.SQLiteReaderLib.Connection;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace ManjTables.Reports.ReportServices
{
    public class ApprovedPickupsService : IReportService<ApprovedPickupsDto>
    {
        private readonly ManjTablesContext _dbContext;

        public ApprovedPickupsService(ManjTablesContext dbContext)
        {
            _dbContext = dbContext;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }
        public void GenerateReportExcel(List<ApprovedPickupsDto> reportDtos, string filePath, string classroomId)
        {
            using ExcelPackage package = new();
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Approved Pickups Report");

            // Add two empty rows above the table
            worksheet.InsertRow(1, 4);

            // Add two empty columns to the left of the table
            worksheet.InsertColumn(1, 2);

            // Set title row values
            worksheet.Cells["C5"].Value = "Student Name";
            worksheet.Cells["D5"].Value = "Pickup With Notice";
            worksheet.Cells["E5"].Value = "Pickup Without Notice";

            // Set title row font properties
            var titleRowRange = worksheet.Cells[5, 3, 5, 5];
            titleRowRange.Style.Font.Bold = true;
            titleRowRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            titleRowRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;            

            int currentRow = 6;
            bool isAlternateRow = false;

            Color accentColor = Color.FromArgb(174, 199, 149);

            var sortedDtos = reportDtos.OrderBy(dto => dto.StudentName).ToList();            

            // Add data to the worksheet
            foreach (var dto in sortedDtos)
            {
                worksheet.Cells["C" + currentRow].Value = dto.StudentName;
                if (string.IsNullOrEmpty(dto.PickupWithNotice) && string.IsNullOrEmpty(dto.PickupWithoutNotice))
                {
                    worksheet.Cells["D" + currentRow].Value = "PARENTS ONLY";
                    worksheet.Cells[$"D{currentRow}:E{currentRow}"].Merge = true;
                    worksheet.Cells[$"D{currentRow}:E{currentRow}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[$"D{currentRow}:E{currentRow}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                }
                else
                {
                    worksheet.Cells["D" + currentRow].Value = dto.PickupWithNotice;
                    worksheet.Cells["E" + currentRow].Value = dto.PickupWithoutNotice;
                }

                // Set row background color
                var rowRange = worksheet.Cells[currentRow, 3, currentRow, 5];
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

            // Enable text wrapping for pickup columns
            worksheet.Column(4).Style.WrapText = true;
            worksheet.Column(5).Style.WrapText = true;

            // Set a minimum width for the columns that have wrapped text
            worksheet.Column(3).AutoFit();
            worksheet.Column(4).Width = 50; // Adjust as needed
            worksheet.Column(5).Width = 50; // Adjust as needed

            // Add thick border between the title row and the rest of the rows
            titleRowRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

            // Set title row background color
            titleRowRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
            titleRowRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow);

            // Merge cells C2, D2, and E2
            worksheet.Cells["C3:E3"].Merge = true;

            // Add thick borders around the table
            var tableRange = worksheet.Cells[5, 3, currentRow, 5];
            tableRange.Style.Border.BorderAround(ExcelBorderStyle.Thick);

            ClassroomService classroomNameService = new(_dbContext);
            string classroomName = classroomNameService.GetClassroomName(classroomId);

            // Set the text and formatting for the merged cell
            worksheet.Cells["C3"].Value = $"{classroomName} - Approved Pickup";
            worksheet.Cells["C3"].Style.Font.Size = 24;
            worksheet.Cells["C3"].Style.Font.Bold = true;
            worksheet.Cells["C3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells["C3"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            package.SaveAs(new FileInfo(filePath));
        }

        public List<ApprovedPickupsDto> GetReportDtos(string classroomId)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            List<ApprovedPickupsDto> reportData = (from child in _dbContext.Childs
                                                   where child.ClassroomIds.Contains(classroomId)
                                                   let approvedAdults = child.ChildApprovedAdults.Select(caa => caa.ApprovedAdult)
                                                   select new ApprovedPickupsDto
                                                   {
                                                       StudentName = child.ChildFirstName + " " + child.ChildLastName,
                                                       PickupWithNotice = string.Join("\n", approvedAdults
                                                       .Where(aa => aa != null && aa.WithoutNotice == "N")
                                                       .Select(aa => aa.FirstName + " " + aa.LastName)),
                                                       PickupWithoutNotice = string.Join("\n", approvedAdults
                                                       .Where(aa => aa != null && aa.WithoutNotice == "Y")
                                                       .Select(aa => aa.FirstName + " " + aa.LastName))
                                                   }).ToList();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            return reportData;
        }

        public void RunReport(string classroomId, string filePath)
        {
            List<ApprovedPickupsDto> approvedPickupsDtos = GetReportDtos(classroomId);
            GenerateReportExcel(approvedPickupsDtos, filePath, classroomId);
        }

        // unused overload
        public void RunReport(string classroomId, string filePath, DateTime selectedDate)
        {
            throw new NotImplementedException();
        }
    }
}
