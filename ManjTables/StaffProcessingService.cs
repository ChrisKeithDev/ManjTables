using ManjTables.DataModels;
using ManjTables.DataModels.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ManjTables
{
    public class StaffProcessingService
    {

        public StaffProcessingService()
        {
            
        }

        public static async Task ProcessStaffMembersFromJson(ManjTablesContext context, string jsonFilePath)
        {
            using StreamReader r = new(jsonFilePath);
            string json = r.ReadToEnd();
            List<StaffMember>? staffMembersFromJson = JsonConvert.DeserializeObject<List<StaffMember>>(json);

            if (staffMembersFromJson != null)
            {
                foreach (var staffMemberJson in staffMembersFromJson)
                {
                    var staffMemberEntity = await context.StaffMembers
                        .FirstOrDefaultAsync(sm => sm.StaffId == staffMemberJson.StaffId);

                    if (staffMemberEntity != null)
                    {
                        staffMemberEntity.FirstName = staffMemberJson.FirstName;
                        staffMemberEntity.LastName = staffMemberJson.LastName;
                    }
                    else
                    {
                        var newStaffMember = new StaffMember
                        {
                            StaffId = staffMemberJson.StaffId,
                            FirstName = staffMemberJson.FirstName,
                            LastName = staffMemberJson.LastName
                        };
                        context.StaffMembers.Add(newStaffMember);
                    }
                }

                await context.SaveChangesAsync();

                foreach (var staffMemberJson in staffMembersFromJson)
                {
                    if (staffMemberJson.ClassroomId != null)
                    {
                        var staffClassroomEntity = new StaffClassroom
                        {
                            StaffId = staffMemberJson.StaffId,
                            ClassroomId = staffMemberJson.ClassroomId,
                            DateCreated = DateTime.Now,
                            DateModified = DateTime.Now
                        };
                        context.StaffClassrooms.Add(staffClassroomEntity);
                    }
                }

                await context.SaveChangesAsync();
            }
        }
    }
}
