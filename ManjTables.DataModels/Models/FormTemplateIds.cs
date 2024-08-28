using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManjTables.DataModels.Models
{
    public class FormTemplateIds
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? EnrollmentContractId { get; set; }
        public int? EmergencyInformationTemplateId { get; set; }
        public int? ApprovedPickupTemplateId { get; set; }
        public int? PhotoReleaseTemplateId { get; set; }
        public int? AnimalApprovalTemplateId { get; set; }
        public int? GoingOutPermissionTemplateId { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

    }
}
