using ManjTables.DataModels;
using ManjTables.DataModels.Models;
using ManjTables.Reports.ReportModels;
using ManjTables.SQLiteReaderLib.Connection;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Text;

namespace ManjTables.Reports.ReportServices
{
    public class MailingListService : IReportService<MailingListDto>
    {
        private readonly ManjTablesContext _dbContext;

        public MailingListService(ManjTablesContext dbContext)
        {
            _dbContext = dbContext;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public void GenerateReportExcel(List<MailingListDto> reportDtos, string filename, string classroomId)
        {
            // Create a StringBuilder to store the CSV content
            StringBuilder csvContent = new();

            // Append the column headers to the CSV content
            csvContent.AppendLine("Salutation,Child Name,Street Address,Street Address Line 2,City,State,Zip Code");

            // Iterate over each DTO and append the data
            foreach (var dto in reportDtos)
            {
                // Prepare the line content by escaping commas and quotes as needed
                var line = new List<string>
                {
                    $"\"{dto.Salutation}\"",
                    $"\"{dto.ChildName}\"",
                    $"\"{dto.StreetAddress}\"",
                    $"\"{dto.StreetAddressLine2}\"",
                    $"\"{dto.City}\"",
                    $"\"{dto.State}\"",
                    $"\"{dto.ZipCode}\""
                };
                // Join the line content with commas and append it to the CSV content
                csvContent.AppendLine(string.Join(",", line));
            }
            // Write the CSV content to the file
            File.WriteAllText(filename, csvContent.ToString());
        }


        public List<MailingListDto> GetReportDtos(string classroomId)
        {
            IQueryable<Child> childrenQuery;

            if (classroomId == "All")
            {
                // Generate report for the entire school
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                childrenQuery = _dbContext.Childs
                    .Include(child => child.ChildParents)
                    .ThenInclude(cp => cp.Parent)
                    .ThenInclude(p => p.Address);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            }
            else
            {
                // Generate report for a single classroom
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                childrenQuery = _dbContext.Childs
                    .Include(child => child.ChildParents)
                    .ThenInclude(cp => cp.Parent)
                    .ThenInclude(p => p.Address)
                    .Where(child => child.ClassroomIds != null && child.ClassroomIds.Contains(classroomId)); ;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            }

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var childrenWithParents = childrenQuery.Select(child => new
            {
                ChildName = child.ChildFirstName + " " + child.ChildLastName,
                Parents = child.ChildParents
                .Where(cp => cp.Parent != null)    
                .Select(cp => new
                {
                    ParentName = cp.Parent.FirstName + " " + cp.Parent.LastName,
                    cp.Parent.Address
                }).ToList()
            }).ToList();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            var mailingList = new List<MailingListDto>();

            foreach (var child in childrenWithParents)
            {
                var distinctAddresses = child.Parents
                    .GroupBy(p => p.Address?.AddressId)
                    .Select(grp => grp.First())
                    .ToList();

                foreach (var parent in distinctAddresses)
                {
                    var salutation = distinctAddresses.Count == 1 ? "The Parents of" : "The Parent of";
                    mailingList.Add(new MailingListDto
                    {
                        Salutation = salutation,
                        ChildName = child.ChildName,
                        StreetAddress = parent.Address?.StreetAddress ?? "Unknown",
                        StreetAddressLine2 = parent.Address?.StreetAddress2 ?? "Unknown",
                        City = parent.Address?.City ?? "Unknown",
                        State = parent.Address?.State ?? "Unknown",
                        ZipCode = parent.Address?.ZipCode ?? "Unknown"
                    });
                }
            }
            return mailingList;
        }

        public void RunReport(string classroomId, string filePath)
        {
            List<MailingListDto> reportDtos = GetReportDtos(classroomId);
            GenerateReportExcel(reportDtos, filePath, classroomId);
        }

        // Unused overload 
        public void RunReport(string classroomId, string filePath, DateTime selectedDate)
        {
            throw new NotImplementedException();
        }
    }
}
