using ManjTables.DataModels.Models;
using ManjTables.ObjectMapping.ChildInfoMapping;
using ManjTables.ObjectMapping.ClassroomsMapping;
using ManjTables.ObjectMapping.FormTemplatesMapping;
using Microsoft.Data.Sqlite;

namespace ManjTables.SQLiteReaderLib.Reader
{
    public class SQLiteReader : ISQLiteReader
    {
        private readonly IChildInfoMapper _childInfoMapper;
        private readonly IFormTemplateIdsMapper _formTemplateIdsMapper;
        public SQLiteReader(IChildInfoMapper childInfoMapper, IFormTemplateIdsMapper formTemplateIdsMapper)
        {
            _childInfoMapper = childInfoMapper;
            _formTemplateIdsMapper = formTemplateIdsMapper;
        }
        public List<ChildInfo> ReadChildInfoFromSQLite(SqliteConnection connection)
        {
            List<ChildInfo> childInfos = new();  // Initialize the list here

            // Check if table exists
            var checkTableCmd = new SqliteCommand("SELECT name FROM sqlite_master WHERE type='table' AND name='ChildInfos';", connection);
            var tableName = checkTableCmd.ExecuteScalar();
            if (tableName == null)
            {
                // Log or throw an exception that the table does not exist
                return childInfos;  // Return the empty list
            }

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM ChildInfos;"; // Could limit the columns and rows

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                ChildInfo childInfo = _childInfoMapper.Map(reader);

                // Check if LastDay is set and if it has passed
                if (!string.IsNullOrEmpty(childInfo.LastDay))
                {
                    if (DateTime.TryParse(childInfo.LastDay, out DateTime lastDay))
                    {
                        // If LastDay has not passed, add the childInfo to the list
                        if (lastDay.Date > DateTime.Today)
                        {
                            childInfos.Add(childInfo);
                        }
                    }
                    else
                    {
                        // Handle or log the case where LastDay is not a valid date                        
                    }
                }
                else
                {
                    // If LastDay is not set, add the childInfo to the list
                    childInfos.Add(childInfo);
                }
            }

            return childInfos;  // Return the populated list
        }



        public FormTemplateIds? ReadFormTemplateIdsFromSQLite(SqliteConnection connection)
        {
            var checkTableCmd = new SqliteCommand("SELECT name FROM sqlite_master WHERE type='table' AND name='FormTemplateIds';", connection);
            var tableName = checkTableCmd.ExecuteScalar();
            if (tableName == null)
            {
                // Throw an exception that the table does not exist
                return null;
            }

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM FormTemplateIds LIMIT 1;"; // Assuming there's only one row

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                FormTemplateIds formTemplateIds = _formTemplateIdsMapper.Map(reader);

                return formTemplateIds;
            }

            return null;
        }
        public List<Classroom> ReadClassroomsFromSQLite(SqliteConnection connection)
        {
            List<Classroom> classrooms = new();  // Initialize the list here

            var checkTableCmd = new SqliteCommand("SELECT name FROM sqlite_master WHERE type='table' AND name='Classrooms';", connection);
            var tableName = checkTableCmd.ExecuteScalar();
            if (tableName == null)
            {
                // Log or throw an exception that the table does not exist
                return classrooms;  // Return the empty list
            }

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Classrooms;"; // Could limit the columns and rows

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                Classroom classroom = new ClassroomsMapper().Map(reader);
                classrooms.Add(classroom);  // Add to the list
            }

            return classrooms;  // Return the populated list
        }


    }
}
