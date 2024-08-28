using ManjTables.DataModels;
using ManjTables.DataModels.Models;
using ManjTables.Reports.ReportModels;
using ManjTables.SQLiteReaderLib.Connection;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace ManjTables.Reports.ReportServices
{
    public class EmergencyContactsService : IReportService<EmergencyContactsDto>
    {
        private readonly ManjTablesContext _dbContext;

        public EmergencyContactsService(ManjTablesContext dbContext)
        {
            _dbContext = dbContext;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }
        public void GenerateReportExcel(List<EmergencyContactsDto> reportDtos, string filePath, string classroomId)
        {
            using ExcelPackage package = new();
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Emergency Contacts");

            // Add two empty rows above the table
            worksheet.InsertRow(1, 4);

            // Add two empty columns to the left of the table
            worksheet.InsertColumn(1, 2);

            // Set title row values
            worksheet.Cells["C5"].Value = "Student Name";
            worksheet.Cells["D5"].Value = "Parent(s)";
            worksheet.Cells["E5"].Value = "Emergency Contact(s)";

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

                // Clean and format parent phone numbers
                var parents = dto.Parent.Split('\n')
                    .Select(parent =>
                    {
                        var parts = parent.Split(": ");
                        if (parts.Length == 2)
                        {
                            var cleanedPhone = CleanAndFormatPhoneNumber(parts[1]);
                            return $"{parts[0]}: {cleanedPhone}";
                        }
                        return parent;
                    });

                worksheet.Cells["D" + currentRow].Value = string.Join("\n", parents);

                // Clean and format emergency contact phone numbers
                var emergencyContacts = dto.EmergencyContacts.Split('\n')
                    .Select(contact =>
                    {
                        var parts = contact.Split(": ");
                        if (parts.Length == 2)
                        {
                            var cleanedPhone = CleanAndFormatPhoneNumber(parts[1]);
                            return $"{parts[0]}: {cleanedPhone}";
                        }
                        return contact;
                    });

                worksheet.Cells["E" + currentRow].Value = string.Join("\n", emergencyContacts);

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

            // Enable text wrapping for columns
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
            titleRowRange.Style.Fill.BackgroundColor.SetColor(Color.Yellow);

            // Merge cells C2, D2, and E2
            worksheet.Cells["C3:E3"].Merge = true;

            // Add thick borders around the table
            var tableRange = worksheet.Cells[5, 3, currentRow, 5];
            tableRange.Style.Border.BorderAround(ExcelBorderStyle.Thick);

            ClassroomService classroomNameService = new(_dbContext);
            string classroomName = classroomNameService.GetClassroomName(classroomId);

            // Set the text and formatting for the merged cell
            worksheet.Cells["C3"].Value = $"{classroomName} - Emergency Contacts";
            worksheet.Cells["C3"].Style.Font.Size = 24;
            worksheet.Cells["C3"].Style.Font.Bold = true;
            worksheet.Cells["C3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells["C3"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            worksheet.Cells["C5:E5"].Style.Font.Size = 16;
            worksheet.Row(5).Height = 30;

            package.SaveAs(new FileInfo(filePath));
        }


        public List<EmergencyContactsDto> GetReportDtos(string classroomId)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.
            List<EmergencyContactsDto> reportData = (from child in _dbContext.Childs
                                                     where child.ClassroomIds.Contains(classroomId)
                                                     select new EmergencyContactsDto
                                                     {
                                                         StudentName = child.ChildFirstName + " " + child.ChildLastName,
                                                         Parent = string.Join("\n", child.ChildParents
                                                                        .Select(cp => cp.Parent.FirstName + ": " + cp.Parent.PhoneNumber)
                                                                        .Where(p => !string.IsNullOrEmpty(p))),
                                                         EmergencyContacts = string.Join("\n", child.ChildEmergencyContacts
                                                                                   .Select(cec => cec.EmergencyContact.FullName 
                                                                                    + ": " + cec.EmergencyContact.PhoneNumber)
                                                                                   .Where(ec => !string.IsNullOrEmpty(ec)))

                                                     }).ToList();
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            return reportData;
        }

        public void RunReport(string classroomId, string filePath)
        {
            List<EmergencyContactsDto> emergencyContactsDtos = GetReportDtos(classroomId);
            GenerateReportExcel(emergencyContactsDtos, filePath, classroomId);
        }

        // Unused overload for reports that require a date
        public void RunReport(string classroomId, string filePath, DateTime selectedDate)
        {
            throw new NotImplementedException();
        }

        internal static string CleanAndFormatPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                return string.Empty;

            // Remove all non-digit characters
            var digitsOnly = new string(phoneNumber.Where(char.IsDigit).ToArray());

            // If the phone number starts with a '1' and is 11 digits long, remove the '1'
            if (digitsOnly.Length == 11 && digitsOnly.StartsWith("1"))
            {
                digitsOnly = digitsOnly.Substring(1);
            }

            // If the phone number is exactly 10 digits, format it as 123-123-1234
            if (digitsOnly.Length == 10)
            {
                return $"{digitsOnly.Substring(0, 3)}-{digitsOnly.Substring(3, 3)}-{digitsOnly.Substring(6)}";
            }

            // If the number is not 10 digits, return it as is
            return digitsOnly;
        }
    }
}
