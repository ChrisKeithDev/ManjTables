using ManjTables.DataModels.Models;
using Microsoft.Data.Sqlite;

namespace ManjTables.ObjectMapping.FormTemplatesMapping
{
    public interface IFormTemplateIdsMapper
    {
        FormTemplateIds Map(SqliteDataReader reader);
    }
}
