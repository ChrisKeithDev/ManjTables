using ManjTables.DataModels.Models;
using ManjTables.SQLiteReaderLib.Connection;
using ManjTables.SQLiteReaderLib.Reader;
using Microsoft.Extensions.Logging;

namespace ManjTables.SQLiteReaderLib
{
    public class SQLiteReaderService
    {
        private readonly IDatabaseConnection _databaseConnection;
        private readonly ISQLiteReader _sqliteReader;
        private readonly ILogger<SQLiteReaderService> _logger;

        public SQLiteReaderService(IDatabaseConnection databaseConnection, 
            ISQLiteReader sqliteReader, ILogger<SQLiteReaderService> logger)
        {
            _databaseConnection = databaseConnection;
            _sqliteReader = sqliteReader;
            _logger = logger;
        }

        public (List<ChildInfo>, FormTemplateIds?, List<Classroom>) ReadChildInfoAndTemplateIdsFromSQLite(string dbPath)
        {
            List<ChildInfo> childInfos;

            _logger.LogInformation("{Now} Attempting to open database connection to {dbPath}.", DateTime.Now, dbPath);
            _databaseConnection.Open(dbPath);

            _logger.LogInformation("{Now} Reading ChildInfo data from SQLite.", DateTime.Now);
            childInfos = _sqliteReader.ReadChildInfoFromSQLite(_databaseConnection.GetConnection());

            _logger.LogInformation("============================================================ {childInfos.Count} ChildInfo records retrieved.", childInfos.Count);

            //foreach (var childInfo in childInfos)
            //{
            //    _logger.LogInformation("================================================+-----============= {first} {last}", childInfo.FirstName, childInfo.LastName);
            //}
            //Thread.Sleep(90000);

            _logger.LogInformation("{Now} Reading FormTemplateIds from SQLite.", DateTime.Now);
            FormTemplateIds? formTemplateIds = _sqliteReader.ReadFormTemplateIdsFromSQLite(_databaseConnection.GetConnection());

            _logger.LogInformation("{Now} Reading Classrooms from SQLite.", DateTime.Now);
            List<Classroom> classrooms = _sqliteReader.ReadClassroomsFromSQLite(_databaseConnection.GetConnection());

            _logger.LogInformation("{Now} Closing database connection.", DateTime.Now);
            _databaseConnection.Close();

            _logger.LogInformation("{Now} Retrieved {childInfos.Count} ChildInfo records.", DateTime.Now, childInfos.Count);

            return (childInfos, formTemplateIds, classrooms);
        }
    }
}
