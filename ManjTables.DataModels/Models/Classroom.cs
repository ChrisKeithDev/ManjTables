using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManjTables.DataModels.Models
{
    public class Classroom
    {
        public Classroom()
        {
            StaffClassrooms = new HashSet<StaffClassroom>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string? ClassroomId { get; set; }
        public string? ClassroomName { get; set; }//index
        public int? LessonSetId { get; set; }
        public bool? Active { get; set; }
        public string? Level { get; set; }

        public virtual ICollection<StaffClassroom> StaffClassrooms { get; set; }

        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
