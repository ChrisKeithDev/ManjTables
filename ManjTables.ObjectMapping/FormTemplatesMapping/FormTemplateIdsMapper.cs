using ManjTables.DataModels.Models;
using Microsoft.Data.Sqlite;

namespace ManjTables.ObjectMapping.FormTemplatesMapping
{
    public class FormTemplateIdsMapper : IFormTemplateIdsMapper
    {
        public FormTemplateIds Map(SqliteDataReader reader)
        {
            return new FormTemplateIds
            {
                EnrollmentContractId = reader.IsDBNull(1) ? null : reader.GetInt32(1),
                EmergencyInformationTemplateId = reader.IsDBNull(2) ? null : reader.GetInt32(2),
                ApprovedPickupTemplateId = reader.IsDBNull(3) ? null : reader.GetInt32(3),
                PhotoReleaseTemplateId = reader.IsDBNull(4) ? null : reader.GetInt32(4),
                AnimalApprovalTemplateId = reader.IsDBNull(5) ? null : reader.GetInt32(5),
                GoingOutPermissionTemplateId = reader.IsDBNull(6) ? null : reader.GetInt32(6),
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };
        }
    }
}
