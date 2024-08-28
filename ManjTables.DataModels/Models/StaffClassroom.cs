using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManjTables.DataModels.Models
{
    public class StaffClassroom
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StaffClassroomId { get; set; }

        [ForeignKey("Staff")]
        public int StaffId { get; set; }
        public StaffMember? StaffMember { get; set; }  // Navigation Property

        [ForeignKey("Classroom")]
        public string? ClassroomId { get; set; }
        public Classroom? Classroom { get; set; }  // Navigation Property

        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
