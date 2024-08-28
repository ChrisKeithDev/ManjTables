using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManjTables.DataModels.Models
{
    public class Programme
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProgrammeId { get; set; }
        public string? Level { get; set; }
        public string? ProgrammeName { get; set; }
        public bool? Ecp { get; set; }
        public string? DismissalTime { get; set; }
        public string? ClassroomIds { get; set; }//index
        public virtual ICollection<Child> Children { get; set; } = new HashSet<Child>();
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
