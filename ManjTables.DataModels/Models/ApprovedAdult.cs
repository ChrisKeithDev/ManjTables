using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManjTables.DataModels.Models
{
    public class ApprovedAdult
    {
        public ApprovedAdult()
        {
            ChildApprovedAdults = new HashSet<ChildApprovedAdult>();
            ApprovedAdultAndPickupForms = new HashSet<ApprovedAdultAndPickupForm>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ApprovedAdultId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Relationship { get; set; }
        public string? PhoneNumber { get; set; }
        public string? WithoutNotice { get; set; }

        public virtual ICollection<ChildApprovedAdult>? ChildApprovedAdults { get; set; }
        public virtual ICollection<ApprovedAdultAndPickupForm>? ApprovedAdultAndPickupForms { get; set; }


        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

    }
}
