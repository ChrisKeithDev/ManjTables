
using ManjTables.DataModels.Models.Forms;
using ManjTables.DataModels.Models;
using ManjTables.DataModels;

namespace ManjTables
{
    public class DataTransformationService
    {
        private readonly ILogger<DataTransformationService> _logger;

        public DataTransformationService(ILogger<DataTransformationService> logger)
        {
            _logger = logger;
        }

        public List<AggregatedChildAndFormData> AggregateChildAndFormData(
            List<ChildInfo> childInfos,
            List<FormModel> formModels,
            FormTemplateIds formTemplateIds)
        {
            List<AggregatedChildAndFormData> aggregatedDataList = new();
            foreach (var childInfo in childInfos)
            {
                _logger.LogInformation("Processing ChildId: {ChildId}", childInfo.ChildId);

                // Aggregate the data for each child
                AggregatedChildAndFormData aggregatedData = CreateAggregatedData(childInfo, formModels, formTemplateIds);
                if (aggregatedData != null)
                {
                    aggregatedDataList.Add(aggregatedData);
                }
            }
            return aggregatedDataList;
        }

        private AggregatedChildAndFormData CreateAggregatedData(
            ChildInfo childInfo,
            List<FormModel> formModels,
            FormTemplateIds formTemplateIds)
        {
            var emergencyContactForm = formModels.FirstOrDefault(
                        f => f.ChildId == childInfo.ChildId &&
                        f.FormTemplateId == formTemplateIds.EmergencyInformationTemplateId);

            if (emergencyContactForm == null)
            {
                _logger.LogError("EmergencyContactForm was not found for ChildId {ChildId}", childInfo.ChildId);
                
            }

            var approvedPickupForm = formModels.FirstOrDefault(
                f => f.ChildId == childInfo.ChildId &&
                f.FormTemplateId == formTemplateIds.ApprovedPickupTemplateId);

            if (approvedPickupForm == null)
            {
                _logger.LogError("ApprovedPickupForm was not found for ChildId {ChildId}", childInfo.ChildId);
                
            }

            var photoReleaseForm = formModels.FirstOrDefault(
                f => f.ChildId == childInfo.ChildId &&
                f.FormTemplateId == formTemplateIds.PhotoReleaseTemplateId);

            if (photoReleaseForm == null)
            {
                _logger.LogError("PhotoReleaseForm was not found for ChildId {ChildId}", childInfo.ChildId);
                
            }

            var animalApprovalForm = formModels.FirstOrDefault(
                f => f.ChildId == childInfo.ChildId &&
                f.FormTemplateId == formTemplateIds.AnimalApprovalTemplateId);

            if (animalApprovalForm == null)
            {
                _logger.LogError("AnimalApprovalForm was not found for ChildId {ChildId}", childInfo.ChildId);
                
            }

            var goingOutPermissionForm = formModels.FirstOrDefault(
                f => f.ChildId == childInfo.ChildId &&
                f.FormTemplateId == formTemplateIds.GoingOutPermissionTemplateId);

            if (goingOutPermissionForm == null)
            {
                _logger.LogError("GoingOutPermissionForm was not found for ChildId {ChildId}", childInfo.ChildId);
                
            }

            return new AggregatedChildAndFormData(
                childInfo,
                emergencyContactForm,
                approvedPickupForm,
                photoReleaseForm,
                animalApprovalForm,
                goingOutPermissionForm);
        }
    }


}
