using ManjTables.DataModels.Models;
using Microsoft.Data.Sqlite;

namespace ManjTables.ObjectMapping.ChildInfoMapping
{
    public class ChildInfoMapper : IChildInfoMapper
    {
        public ChildInfo Map(SqliteDataReader reader)
        {
            // Your mapping logic here
            return new ChildInfo
            {
                ChildId = reader.GetInt32(0),
                FirstName = reader.IsDBNull(1) ? null : reader.GetString(1),
                LastName = reader.IsDBNull(2) ? null : reader.GetString(2),
                ProfilePhoto = reader.IsDBNull(3) ? null : reader.GetString(3),
                BirthDate = reader.IsDBNull(4) ? null : reader.GetString(4),
                MiddleName = reader.IsDBNull(5) ? null : reader.GetString(5),
                Gender = reader.IsDBNull(6) ? null : reader.GetString(6),
                HoursString = reader.IsDBNull(7) ? null : reader.GetString(7),
                DominantLanguage = reader.IsDBNull(8) ? null : reader.GetString(8),
                Allergies = reader.IsDBNull(9) ? null : reader.GetString(9),
                Program = reader.IsDBNull(10) ? null : reader.GetString(10),
                FirstDay = reader.IsDBNull(11) ? null : reader.GetString(11),
                ApprovedAdultsString = reader.IsDBNull(12) ? null : reader.GetString(12),
                EmergencyContactsString = reader.IsDBNull(13) ? null : reader.GetString(13),
                ClassroomIdsString = reader.IsDBNull(14) ? null : reader.GetString(14),
                ParentIdsString = reader.IsDBNull(15) ? null : reader.GetString(15),
                FormsRawJson = reader.IsDBNull(16) ? null : reader.GetString(16),
                LastDay = reader.IsDBNull(17) ? null : reader.GetString(17)
            };
        }
    }
}
