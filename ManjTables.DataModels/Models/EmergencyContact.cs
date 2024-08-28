using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManjTables.DataModels.Models
{
    public class EmergencyContact
    {
        public EmergencyContact()
        {
            ChildEmergencyContacts = new HashSet<ChildEmergencyContact>();
            EmergencyContactEmergencyInformationForms = new HashSet<EmergencyContactEmergencyInformationForm>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmergencyContactId { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }

        public virtual ICollection<ChildEmergencyContact>? ChildEmergencyContacts { get; set; }
        public virtual ICollection<EmergencyContactEmergencyInformationForm>? EmergencyContactEmergencyInformationForms { get; set; }



        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
