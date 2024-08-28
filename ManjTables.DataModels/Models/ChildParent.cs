using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManjTables.DataModels.Models
{
    public class ChildParent
    {
        // Composite key configuration will be done in OnModelCreating

        public int ChildId { get; set; }
        public Child? Child { get; set; } // Navigation Property

        public int ParentId { get; set; }
        public Parent? Parent { get; set; } // Navigation Property

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
