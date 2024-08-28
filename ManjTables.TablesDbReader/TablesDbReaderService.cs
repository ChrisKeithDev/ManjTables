using ManjTables.SQLiteReaderLib.Connection;
// Other using statements

namespace ManjTables.TablesDbReader
{
    public class TablesDbReaderService
    {
        private readonly IDatabaseConnection _dbConnection;

        public TablesDbReaderService(IDatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        // Methods to fetch data from database
    }
}
