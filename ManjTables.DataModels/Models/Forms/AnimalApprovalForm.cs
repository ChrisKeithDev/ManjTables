using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManjTables.DataModels.Models.Forms
{
    public class AnimalApprovalForm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AnimalApprovalFormId { get; set; }
        public int? FormTemplateId { get; set; }
        public string? Permission { get; set; }
        public string? SignatureOne { get; set; }
        public string? SignatureTwo { get; set; }
        [ForeignKey("Child")]
        public int? ChildId { get; set; }//index
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

    }
}
