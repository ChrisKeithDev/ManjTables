using ManjTables.DataModels.Models;
using Microsoft.Data.Sqlite;

namespace ManjTables.ObjectMapping.ClassroomsMapping
{
    public interface IClassroomsMapper
    {
        Classroom Map(SqliteDataReader reader);
    }
}