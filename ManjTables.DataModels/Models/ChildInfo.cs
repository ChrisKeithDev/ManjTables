using System.ComponentModel.DataAnnotations;

namespace ManjTables.DataModels.Models
{
    public class ChildInfo
    {
        [Key]
        public int ChildId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ProfilePhoto { get; set; }
        public string? BirthDate { get; set; }
        public string? MiddleName { get; set; }
        public string? Gender { get; set; }
        public string? HoursString { get; set; }
        public string? DominantLanguage { get; set; }
        public string? Allergies { get; set; }
        public string? Program { get; set; }
        public string? FirstDay { get; set; }
        public string? LastDay { get; set; }
        public string? ApprovedAdultsString { get; set; }
        public string? EmergencyContactsString { get; set; }
        public string? ClassroomIdsString { get; set; }
        public string? ParentIdsString { get; set; }
        public string? FormsRawJson { get; set; }
    }
}
