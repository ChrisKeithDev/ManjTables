using ManjTables.DataModels.Models;
using ManjTables.DataModels.Models.Forms;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ManjTables.JsonParsing
{
    public class FormsJsonParser : IFormsJsonParser
    {
        private readonly ILogger<FormsJsonParser> _logger;

        public FormsJsonParser(ILogger<FormsJsonParser> logger)
        {
            _logger = logger;
        }
        public List<FormModel> ParseRawJsonIntoFormModels(List<ChildInfo> childInfos)
        {
            List<FormModel> formModels = new();

            foreach (var childInfo in childInfos)
            {
                _logger.LogInformation("{Now} Parsing raw JSON for {ChildId}", DateTime.Now, childInfo.ChildId);
                if (string.IsNullOrEmpty(childInfo.FormsRawJson))
                {
                    continue;
                }

                try
                {
                    List<FormModel>? forms = JsonConvert.DeserializeObject<List<FormModel>>(childInfo.FormsRawJson);

                    if (forms != null)
                    {
                        _logger.LogInformation("Parsed {Count} forms for child_id {ChildId}", forms.Count, childInfo.ChildId);
                        formModels.AddRange(forms);
                    }

                }
                catch (JsonReaderException ex)
                {
                    _logger.LogError("An error occurred while parsing JSON: {Message}", ex.Message);
                }
            }

            return formModels;
        }
        public List<FormModel> FilterFormsUsingFormTemplateIds(List<FormModel> formModels, FormTemplateIds formTemplateIds)
        {
            _logger.LogInformation("Inside FilterFormsUSingFormTemplateIds: EnrollmentContract: {EC}, EmergencyInformation: {E}, ApprovedPickup: {A}, PhotoRelease: {P}, AnimalApproval: {An}, GoingOutPermission: {G}",
                           formTemplateIds.EnrollmentContractId,
                           formTemplateIds.EmergencyInformationTemplateId,
                           formTemplateIds.ApprovedPickupTemplateId,
                           formTemplateIds.PhotoReleaseTemplateId,
                           formTemplateIds.AnimalApprovalTemplateId,
                           formTemplateIds.GoingOutPermissionTemplateId);
            _logger.LogInformation("Count of formModels before filtering: {Count}", formModels.Count);
            _logger.LogInformation("Distinct FormTemplateIds in formModels: {DistinctIds}", formModels.Select(fm => fm.FormTemplateId).Distinct());

            List<FormModel> filteredForms = new();
            filteredForms = formModels
                .Where(fm => fm.FormTemplateId == formTemplateIds.EnrollmentContractId 
                            || fm.FormTemplateId == formTemplateIds.EmergencyInformationTemplateId
                            || fm.FormTemplateId == formTemplateIds.ApprovedPickupTemplateId
                            || fm.FormTemplateId == formTemplateIds.PhotoReleaseTemplateId
                            || fm.FormTemplateId == formTemplateIds.AnimalApprovalTemplateId
                            || fm.FormTemplateId == formTemplateIds.GoingOutPermissionTemplateId)
                .ToList();
            _logger.LogInformation("Inside Filter method: Count of formModels after filtering: {Count}", filteredForms.Count);
            return filteredForms;
        }
    }
}
