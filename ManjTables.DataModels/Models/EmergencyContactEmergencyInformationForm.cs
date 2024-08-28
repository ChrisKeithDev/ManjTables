using ManjTables.DataModels.Models.Forms;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManjTables.DataModels.Models
{
    public class EmergencyContactEmergencyInformationForm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmergencyContactEmergencyInformationFormId { get; set; }

        [ForeignKey("EmergencyContactId")]
        public int EmergencyContactId { get; set; }
        public EmergencyContact? EmergencyContact { get; set; }  // Navigation Property

        [ForeignKey("EmergencyInformationFormId")]
        public int EmergencyInformationFormId { get; set; }
        public EmergencyInformationForm? EmergencyInformationForm { get; set; }  // Navigation Property

        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
