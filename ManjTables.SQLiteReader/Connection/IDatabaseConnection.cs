using Microsoft.Data.Sqlite;

namespace ManjTables.SQLiteReaderLib.Connection
{
    public interface IDatabaseConnection
    {
        void Open(string dbPath);
        void Close();
        SqliteConnection GetConnection();
    }
}
