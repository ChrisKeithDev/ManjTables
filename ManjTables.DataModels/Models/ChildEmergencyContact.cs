using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManjTables.DataModels.Models
{
    public class ChildEmergencyContact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ChildEmergencyContactId { get; set; }

        [ForeignKey("Child")]
        public int ChildId { get; set; }
        public Child? Child { get; set; }  // Navigation Property

        [ForeignKey("EmergencyContact")]
        public int EmergencyContactId { get; set; }
        public EmergencyContact? EmergencyContact { get; set; }  // Navigation Property
        
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
