using ManjTables.DataModels.Models;
using Microsoft.Data.Sqlite;

namespace ManjTables.ObjectMapping.ClassroomsMapping
{
    public class ClassroomsMapper : IClassroomsMapper
    {
        public Classroom Map(SqliteDataReader reader)
        {
            return new Classroom
            {
                ClassroomId = reader.GetString(reader.GetOrdinal("ClassroomId")),
                ClassroomName = reader.IsDBNull(reader.GetOrdinal("ClassroomName")) ? null : reader.GetString(reader.GetOrdinal("ClassroomName")),
                LessonSetId = reader.GetInt32(reader.GetOrdinal("LessonSetId")),
                Active = reader.GetBoolean(reader.GetOrdinal("Active")),
                Level = reader.IsDBNull(reader.GetOrdinal("Level")) ? null : reader.GetString(reader.GetOrdinal("Level")),
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };
        }

    }
}
