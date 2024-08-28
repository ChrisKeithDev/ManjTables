using ManjTables.DataModels.Models;
using Microsoft.Data.Sqlite;

namespace ManjTables.ObjectMapping.ChildInfoMapping
{
    public interface IChildInfoMapper
    {
        ChildInfo Map(SqliteDataReader reader);
    }
}
