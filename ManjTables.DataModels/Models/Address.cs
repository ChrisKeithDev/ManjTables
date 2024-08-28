using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManjTables.DataModels.Models
{
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AddressId { get; set; }
        public string? StreetAddress { get; set; }//index
        public string? StreetAddress2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public virtual ICollection<Child> Children { get; set; } = new HashSet<Child>();
        public virtual ICollection<Parent> Parents { get; set; } = new HashSet<Parent>();
    }
}
