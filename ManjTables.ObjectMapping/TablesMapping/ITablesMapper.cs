using ManjTables.DataModels;
using ManjTables.DataModels.Models;
using ManjTables.DataModels.Models.Forms;

namespace ManjTables.ObjectMapping.TablesMapping
{
    public interface ITablesMapper
    {
        Address ChildAddressTableMapper
            (AggregatedChildAndFormData aggregatedData);

        List<Address> ParentAddressesTableMapper
            (AggregatedChildAndFormData aggregatedData);

        List<ApprovedAdult> ApprovedAdultTableMapper
            (AggregatedChildAndFormData aggregatedData);

        Child ChildTableMapper
            (AggregatedChildAndFormData aggregatedData);

        List<EmergencyContact> EmergencyContactTableMapper
            (AggregatedChildAndFormData aggregatedData);

        List<Parent> ParentTableMapper
            (AggregatedChildAndFormData aggregatedData);

        Programme ProgrammeTableMapper
            (AggregatedChildAndFormData aggregatedData);

        AnimalApprovalForm AnimalApprovalFormTableMapper
            (AggregatedChildAndFormData aggregatedData);

        ApprovedPickupForm ApprovedPickupFormTableMapper
            (AggregatedChildAndFormData aggregatedData);

        EmergencyInformationForm EmergencyInformationFormTableMapper
            (AggregatedChildAndFormData aggregatedData);

        PhotoReleaseForm PhotoReleaseFormTableMapper
            (AggregatedChildAndFormData aggregatedData);

        GoingOutPermissionForm GoingOutPermissionFormTableMapper
            (AggregatedChildAndFormData aggregatedData);

        public void MapAllTables(
                                AggregatedChildAndFormData aggregatedData,
                                out Child child,
                                out Programme programme,
                                out Address childAddress,
                                out List<Address> parentAddresses,
                                out List<ApprovedAdult> approvedAdults,
                                out List<EmergencyContact> emergencyContacts,
                                out List<Parent> parents,
                                out AnimalApprovalForm animalApprovalForm,
                                out ApprovedPickupForm approvedPickupForm,
                                out EmergencyInformationForm emergencyInformationForm,
                                out PhotoReleaseForm photoReleaseForm,
                                out GoingOutPermissionForm goingOutPermissionForm);
    }
}
