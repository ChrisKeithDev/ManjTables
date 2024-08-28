using ManjTables.DataModels.Models;
using Microsoft.Data.Sqlite;

namespace ManjTables.SQLiteReaderLib.Reader
{
    public interface ISQLiteReader
    {
        List<ChildInfo> ReadChildInfoFromSQLite(SqliteConnection connection);
        FormTemplateIds? ReadFormTemplateIdsFromSQLite(SqliteConnection connection);
        List<Classroom> ReadClassroomsFromSQLite(SqliteConnection connection);
    }
}
