using ManjTables.Reports.ReportModels;
using ManjTables.Reports.ReportServices;
using System;
using System.Collections.Generic;
using System.Windows;

namespace ManjTables.Reports.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isProgrammaticCheckChange = false;
        private readonly SelectedReportEngine _selectedReportEngine;
        private readonly ClassroomService _classroomService;
        
        

        public MainWindow(
            AgeAtDateService ageAtDateService, AllergiesPermissionsService allergiesPermissionsService,
            ApprovedPickupsService approvedPickupsService, ClassroomService classroomService,
            EcpDismissalService ecpDismissalService, EmergencyContactsService emergencyContactsService,
            GoingOutPermissionService goingOutPermissionService,
            MailingListService mailingListService, ParentDirectoryService parentDirectoryService,
            SchoolEnrollmentService schoolEnrollmentService)
        {
            InitializeComponent();
            datePicker.SelectedDate = DateTime.Now;

            cbAllClassrooms.IsChecked = true;
            cbAllFiles.IsChecked = true;

            _classroomService = classroomService;
            _selectedReportEngine = new SelectedReportEngine(
                ageAtDateService, allergiesPermissionsService, approvedPickupsService,
                ecpDismissalService, emergencyContactsService, goingOutPermissionService,
                mailingListService, parentDirectoryService, schoolEnrollmentService);

            _selectedReportEngine.StatusUpdated += SelectedReportEngine_StatusUpdated;
        }

        private void BtnBuildSelected_Click(object sender, RoutedEventArgs e)
        {
            DateTime selectedDate = datePicker.SelectedDate ?? DateTime.Now;
            List<ClassroomDto> allClassrooms = _classroomService.GetClassrooms();
            var selectedClassrooms = GetSelectedClassrooms(allClassrooms);

            _selectedReportEngine.CbAgesChecked = cbAges.IsChecked == true;
            _selectedReportEngine.CbAllergiesChecked = cbAllergies.IsChecked == true;
            _selectedReportEngine.CbApprovedPickupsChecked = cbApprovedPickups.IsChecked == true;
            _selectedReportEngine.CbEcpDismissalChecked = cbEcpDismissal.IsChecked == true;
            _selectedReportEngine.CbEmergencyChecked = cbEmergency.IsChecked == true;
            _selectedReportEngine.CbEnrollmentChecked = cbEnrollment.IsChecked == true;
            _selectedReportEngine.CbMailingListChecked = cbMailingList.IsChecked == true;
            _selectedReportEngine.CbParentDirChecked = cbParentDirFile.IsChecked == true;
            _selectedReportEngine.CbGoingOutPermissionChecked = cbGoingOutPermission.IsChecked == true;
            _selectedReportEngine.CbStudentZipCodesChecked = cbStudentZipCodes.IsChecked == true;
             
            _selectedReportEngine.CbAllClassroomsChecked = cbAllClassrooms.IsChecked == true;
            _selectedReportEngine.CbAllFilesChecked = cbAllFiles.IsChecked == true;

            _selectedReportEngine.GenerateReports(selectedDate, selectedClassrooms);
            

            MessageBox.Show("Files generated successfully!");
        }
        private void SelectedReportEngine_StatusUpdated(object? sender, string e)
        {
            // Update the UI based on the status message e
            Dispatcher.Invoke(() =>
            {
                statusPopup.IsOpen = !string.IsNullOrEmpty(e);
                progressBar.Visibility = !string.IsNullOrEmpty(e) ? Visibility.Visible : Visibility.Collapsed;
                statusTextBlock.Text = e;
            });
        }
        private List<ClassroomDto> GetSelectedClassrooms(List<ClassroomDto> allClassrooms)
        {
            List<ClassroomDto> selectedClassrooms = new();

            if (cbAllClassrooms.IsChecked == true)
            {
                return allClassrooms;
            }

            if (cbClassroomA.IsChecked == true)
                selectedClassrooms.Add(ClassroomService.GetDtoByName(allClassrooms, "Classroom A"));
            if (cbClassroomB.IsChecked == true)
                selectedClassrooms.Add(ClassroomService.GetDtoByName(allClassrooms, "Classroom B"));
            if (cbClassroomC.IsChecked == true)
                selectedClassrooms.Add(ClassroomService.GetDtoByName(allClassrooms, "Classroom C"));
            if (cbClassroomD.IsChecked == true)
                selectedClassrooms.Add(ClassroomService.GetDtoByName(allClassrooms, "Classroom D"));
            if (cbClassroomE.IsChecked == true)
                selectedClassrooms.Add(ClassroomService.GetDtoByName(allClassrooms, "Classroom E"));

            return selectedClassrooms;
        }
        private void OnCheckedAllClassrooms(object sender, RoutedEventArgs e)
        {
            if (isProgrammaticCheckChange) return;

            isProgrammaticCheckChange   = true;
            cbClassroomA.IsChecked      = false;
            cbClassroomB.IsChecked      = false;
            cbClassroomC.IsChecked      = false;
            cbClassroomD.IsChecked      = false;
            cbClassroomE.IsChecked      = false;
            isProgrammaticCheckChange   = false;
        }

        private void OnCheckedClassroom(object sender, RoutedEventArgs e)
        {
            if (isProgrammaticCheckChange) return;

            // If all checkboxes are checked
            if (cbClassroomA.IsChecked == true &&
                cbClassroomB.IsChecked == true &&
                cbClassroomC.IsChecked == true &&
                cbClassroomD.IsChecked == true &&
                cbClassroomE.IsChecked == true)
            {
                isProgrammaticCheckChange   = true;
                cbAllClassrooms.IsChecked   = true;
                cbClassroomA.IsChecked      = false;
                cbClassroomB.IsChecked      = false;
                cbClassroomC.IsChecked      = false;
                cbClassroomD.IsChecked      = false;
                cbClassroomE.IsChecked      = false;
                isProgrammaticCheckChange   = false;
            }
            else
            {
                cbAllClassrooms.IsChecked = false;
            }
        }

        private void OnUncheckedClassroom(object sender, RoutedEventArgs e)
        {
            if (isProgrammaticCheckChange) return;

            // If all individual checkboxes are unchecked, check the "All..." checkbox.
            if (!( cbClassroomA.IsChecked == true || cbClassroomB.IsChecked == true
                || cbClassroomC.IsChecked == true || cbClassroomD.IsChecked == true
                || cbClassroomE.IsChecked == true))
            {
                isProgrammaticCheckChange = true;
                cbAllClassrooms.IsChecked = true;
                isProgrammaticCheckChange = false;
            }
        }

        private void OnCheckedAllFiles(object sender, RoutedEventArgs e)
        {
            if (isProgrammaticCheckChange) return;

            isProgrammaticCheckChange   = true;
            cbAllergies.IsChecked       = false;
            cbAges.IsChecked            = false;
            cbEnrollment.IsChecked      = false;
            cbEmergency.IsChecked       = false;
            cbApprovedPickups.IsChecked          = false;
            cbParentDirFile.IsChecked   = false;
            cbEcpDismissal.IsChecked    = false;
            cbMailingList.IsChecked     = false;
            cbStudentZipCodes.IsChecked = false;
            isProgrammaticCheckChange   = false;
        }

        private void OnCheckedFile(object sender, RoutedEventArgs e)
        {
            if (isProgrammaticCheckChange) return;

            // If all checkboxes are checked

            if (cbAllergies.IsChecked       == true &&
                cbAges.IsChecked            == true &&
                cbEnrollment.IsChecked      == true &&
                cbEmergency.IsChecked       == true &&
                cbApprovedPickups.IsChecked          == true &&
                cbParentDirFile.IsChecked   == true &&
                cbMailingList.IsChecked     == true &&
                cbEcpDismissal.IsChecked    == true &&
                cbStudentZipCodes.IsChecked == true)
            {
                isProgrammaticCheckChange   = true;
                cbAllFiles.IsChecked        = true;
                cbAllergies.IsChecked       = false;
                cbAges.IsChecked            = false;
                cbEnrollment.IsChecked      = false;
                cbEmergency.IsChecked       = false;
                cbApprovedPickups.IsChecked          = false;
                cbParentDirFile.IsChecked   = false;
                cbMailingList.IsChecked     = false;
                cbEcpDismissal.IsChecked    = false;
                cbStudentZipCodes.IsChecked = false;
                isProgrammaticCheckChange   = false;
            }
            else
            {
                cbAllFiles.IsChecked = false;
            }
        }

        private void OnUncheckedFile(object sender, RoutedEventArgs e)
        {
            if (isProgrammaticCheckChange) return;
            // If all individual checkboxes are unchecked, check the "All..." checkbox.
            if (!( cbAllergies.IsChecked        == true || cbAges.IsChecked          == true
                || cbEnrollment.IsChecked       == true || cbEmergency.IsChecked     == true
                || cbApprovedPickups.IsChecked  == true || cbParentDirFile.IsChecked == true
                || cbMailingList.IsChecked      == true || cbEcpDismissal.IsChecked  == true
                || cbStudentZipCodes.IsChecked  == true))
            {
                isProgrammaticCheckChange = true;
                cbAllFiles.IsChecked      = true;
                isProgrammaticCheckChange = false;
            }
        }

        private void OnUncheckedAllClassrooms(object sender, RoutedEventArgs e)
        {
            // Could want to handle this event to provide specific functionality when "All Classrooms" checkbox is unchecked.
        }

        private void OnUncheckedAllFiles(object sender, RoutedEventArgs e)
        {
            // Could want to handle this event to provide specific functionality when "All Files" checkbox is unchecked.
        }
    }
}
