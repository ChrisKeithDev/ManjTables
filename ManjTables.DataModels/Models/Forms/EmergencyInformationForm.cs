using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManjTables.DataModels.Models.Forms
{
    public class EmergencyInformationForm
    {
        public EmergencyInformationForm()
        {
               EmergencyContactEmergencyInformationForms = new HashSet<EmergencyContactEmergencyInformationForm>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmergencyInformationFormId { get; set; }
        public int? FormTemplateId { get; set; }
        [ForeignKey("Child")]
        public int? ChildId { get; set; }//index

        public virtual ICollection<EmergencyContactEmergencyInformationForm>? EmergencyContactEmergencyInformationForms { get; set; }
        
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
