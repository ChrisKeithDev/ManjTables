using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManjTables.DataModels.Models
{
    public class StaffMember
    {
        public StaffMember()
        {
            StaffClassrooms = new HashSet<StaffClassroom>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StaffId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [NotMapped]
        public string? ClassroomId { get; set; }

        public virtual ICollection<StaffClassroom> StaffClassrooms { get; set; }

        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
