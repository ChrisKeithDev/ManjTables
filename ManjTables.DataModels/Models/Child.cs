using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ManjTables.DataModels.Models
{
    public class Child
    {
        public Child()
        {
            ChildApprovedAdults = new HashSet<ChildApprovedAdult>();
            ChildEmergencyContacts = new HashSet<ChildEmergencyContact>();
            ChildParents = new HashSet<ChildParent>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ChildId { get; set; }
        public string? ChildFirstName { get; set; }
        public string? ChildLastName { get; set; }
        public string? ChildMiddleName { get; set; }
        public string? Photo { get; set; }
        public string? BirthDate { get; set; } //index
        public string? Gender { get; set; }
        public string? PrimaryLanguage { get; set; }
        public string? Hours { get; set; } //index
        public string? Allergies { get; set; }
        public int? AddressId { get; set; }
        public virtual Address? Address { get; set; }
        public int? ProgrammeId { get; set; }
        public virtual Programme? Programme { get; set; }

        [ForeignKey("Classroom")]
        public string? ClassroomIds { get; set; }//index
                
        public virtual ICollection<ChildApprovedAdult> ChildApprovedAdults { get; set; } = new HashSet<ChildApprovedAdult>();
        public virtual ICollection<ChildEmergencyContact>? ChildEmergencyContacts { get; set; }
        public virtual ICollection<ChildParent> ChildParents { get; set; }
        

        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
