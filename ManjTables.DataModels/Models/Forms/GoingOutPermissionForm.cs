using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManjTables.DataModels.Models.Forms
{
    public class GoingOutPermissionForm
    {
        public GoingOutPermissionForm()
        {
            
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GoingOutPermissionFormId { get; set; }

        public int? FormTemplateId { get; set; }

        [ForeignKey("Child")]
        public int? ChildId { get; set; } // index

        public string? Contact { get; set; }
        public string? DriverAvailability { get; set; }
        public string? ParentGuardianSignature { get; set; }
        public bool? PermissionToParticipate { get; set; }

        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
