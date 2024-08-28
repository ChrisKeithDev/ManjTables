using ManjTables.DataModels.Models;
using ManjTables.DataModels.Models.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManjTables.DataModels
{
    public class AggregatedChildAndFormData
    {
        public ChildInfo ChildInfoData { get; set; }
        public FormModel? EmergencyContactForm { get; set; }
        public FormModel? ApprovedPickupForm { get; set; }
        public FormModel? PhotoReleaseForm { get; set; }
        public FormModel? AnimalApprovalForm { get; set; }
        public FormModel? GoingOutPermissionForm { get; set; }

        // Constructor
        public AggregatedChildAndFormData(
            ChildInfo childInfoData,
            FormModel? emergencyContactForm,
            FormModel? approvedPickupForm,
            FormModel? photoReleaseForm,
            FormModel? animalApprovalForm,
            FormModel? goingOutPermissionForm)
        {
            ChildInfoData = childInfoData;
            EmergencyContactForm = emergencyContactForm;
            ApprovedPickupForm = approvedPickupForm;
            PhotoReleaseForm = photoReleaseForm;
            AnimalApprovalForm = animalApprovalForm;
            GoingOutPermissionForm = goingOutPermissionForm;
        }
    }
}
