using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManjTables.DataModels.Models
{
    public class ChildApprovedAdult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ChildApprovedAdultId { get; set; }

        [ForeignKey("Child")]
        public int ChildId { get; set; }
        public Child? Child { get; set; }  // Navigation Property

        [ForeignKey("ApprovedAdult")]
        public int ApprovedAdultId { get; set; }
        public ApprovedAdult? ApprovedAdult { get; set; }  // Navigation Property

        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }

}
