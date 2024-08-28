using Newtonsoft.Json;


namespace ManjTables.DataModels.Models.Forms
{
    public class FormModel
    {
        [JsonProperty("id")]
        public int? FormId { get; set; }
        [JsonProperty("form_template_id")]
        public int FormTemplateId { get; set; }
        [JsonProperty("child_id")]
        public int? ChildId { get; set; }
        [JsonProperty("state")]
        public string? State { get; set; }
        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }
        [JsonProperty("fields")]
        public Dictionary<string, object>? Fields { get; set; }
    }
}
