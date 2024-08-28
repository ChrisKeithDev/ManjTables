using Microsoft.Data.Sqlite;

namespace ManjTables.SQLiteReaderLib.Connection
{
    public class DatabaseConnection : IDatabaseConnection
    {
        private SqliteConnection? _connection;
        public void Open(string dbPath)
        {
            _connection = new SqliteConnection($"Data Source={dbPath}");
            _connection.Open();
        }

        public void Close()
        {
            _connection?.Close();
        }

        public SqliteConnection GetConnection()
        {
            if (_connection == null)
            {
                throw new InvalidOperationException("Connection is not open.");
            }
            return _connection;
        }

    }
}
