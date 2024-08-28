using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ManjTables.DataModels.Models
{
    public class Parent
    {
        public Parent()
        {
            ChildParents = new HashSet<ChildParent>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ParentId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? ShareContactInfo { get; set; }
        public int? ParentSelector { get; set; }
        public int? AddressId { get; set; }
        public virtual Address? Address { get; set; }

        public virtual ICollection<ChildParent>? ChildParents { get; set; }


        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

    }
}
