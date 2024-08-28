using ManjTables.DataModels.Models.Forms;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManjTables.DataModels.Models
{
    public class ApprovedAdultAndPickupForm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ApprovedAdultAndPickupFormId { get; set; }

        [ForeignKey("ApprovedAdult")]
        public int ApprovedAdultId { get; set; }
        public ApprovedAdult? ApprovedAdult { get; set; }  // Navigation Property

        [ForeignKey("PickupForm")]
        public int PickupFormId { get; set; }
        public ApprovedPickupForm? ApprovedPickupForm { get; set; }  // Navigation Property

        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
