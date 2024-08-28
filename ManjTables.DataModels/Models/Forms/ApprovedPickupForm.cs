using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManjTables.DataModels.Models.Forms
{
    public class ApprovedPickupForm
    {
        public ApprovedPickupForm()
        {
            ApprovedAdultAndPickupForms = new HashSet<ApprovedAdultAndPickupForm>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ApprovedPickupFormId { get; set; }
        public int? FormTemplateId { get; set; }
        public string? ParentGuardian { get; set; }
        [ForeignKey("Child")]
        public int? ChildId { get; set; }//index

        public virtual ICollection<ApprovedAdultAndPickupForm>? ApprovedAdultAndPickupForms { get; set; }

        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
