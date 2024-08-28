using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManjTables.DataModels.Models.Forms
{
    public class PhotoReleaseForm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PhotoReleaseFormId { get; set; }
        public int? FormTemplateId { get; set; }
        [ForeignKey("Child")]
        public int? ChildId { get; set; }//index
        public string? Print { get; set; }
        public string? Website { get; set; }
        public string? Internal { get; set; }
        public string? Signature { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
