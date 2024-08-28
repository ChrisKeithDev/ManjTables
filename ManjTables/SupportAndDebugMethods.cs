using ManjTables.DataModels.Models.Forms;
using ManjTables.DataModels.Models;

namespace ManjTables
{
    public class SupportAndDebugMethods
    {
        public static void CheckForMissingForms(List<FormModel> filteredForms, FormTemplateIds formTemplateIds, ILogger logger)
        {
            // Handle null cases for nullable value types
            if (formTemplateIds.EmergencyInformationTemplateId is null ||
                formTemplateIds.ApprovedPickupTemplateId is null ||
                formTemplateIds.PhotoReleaseTemplateId is null ||
                formTemplateIds.AnimalApprovalTemplateId is null ||
                formTemplateIds.GoingOutPermissionTemplateId is null)
            {
                logger.LogError("One or more FormTemplateIds are null.");
                return;
            }

            // Step 1: Group forms by child_id
            var groupedForms = filteredForms.GroupBy(f => f.ChildId);

            // Step 2 & 3: Check for missing forms for each child
            foreach (var group in groupedForms)
            {
                var childId = group.Key;
                var formTemplateIdsForChild = group.Select(f => f.FormTemplateId).ToList();

                // Initialize a list to store missing form template IDs for this child
                List<int> missingForms = new();

                // Check each expected form template ID
                CheckForm(formTemplateIdsForChild, formTemplateIds.EmergencyInformationTemplateId.Value, missingForms);
                CheckForm(formTemplateIdsForChild, formTemplateIds.ApprovedPickupTemplateId.Value, missingForms);
                CheckForm(formTemplateIdsForChild, formTemplateIds.PhotoReleaseTemplateId.Value, missingForms);
                CheckForm(formTemplateIdsForChild, formTemplateIds.AnimalApprovalTemplateId.Value, missingForms);
                CheckForm(formTemplateIdsForChild, formTemplateIds.GoingOutPermissionTemplateId.Value, missingForms);

                // If there are missing forms, log the information or write to a file
                if (missingForms.Count > 0)
                {
                    var missingFormsStr = string.Join(", ", missingForms);
                    logger.LogInformation("Child {childId} is missing forms with template IDs: {missingFormsStr}", childId, missingFormsStr);
                }
            }
        }
        private static void CheckForm(List<int> formTemplateIdsForChild, int templateId, List<int> missingForms)
        {
            if (!formTemplateIdsForChild.Contains(templateId))
            {
                missingForms.Add(templateId);
            }
        }
    }
}
