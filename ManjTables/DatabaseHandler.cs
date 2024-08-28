
namespace ManjTables
{
    public class DatabaseHandler
    {
        private readonly ILogger<DatabaseHandler> _logger;

        public DatabaseHandler(ILogger<DatabaseHandler> logger)
        {
            _logger = logger;
        }

        public string ReadDatabasePath(string filePath)
        {
            _logger.LogInformation("Reading database path from {filePath}", filePath);
            string dbPath = File.ReadAllText(filePath); // Simplified for example
            _logger.LogInformation("Logging database change at path = {dbPath}", dbPath);
            return dbPath;
        }
    }

}
