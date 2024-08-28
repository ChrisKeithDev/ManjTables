using System;
using System.Windows;
using ManjTables.DataModels;
using ManjTables.Reports.ReportServices;
using ManjTables.SQLiteReaderLib.Connection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ManjTables.Reports.GUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(configure => configure.AddConsole())
                    .AddScoped<AllergiesPermissionsService>()
                    .AddScoped<ApprovedPickupsService>()
                    .AddScoped<AgeAtDateService>()
                    .AddScoped<EcpDismissalService>()
                    .AddScoped<EmergencyContactsService>()
                    .AddScoped<GoingOutPermissionService>()
                    .AddScoped<MailingListService>()
                    .AddScoped<ParentDirectoryService>()
                    .AddScoped<SchoolEnrollmentService>()
                    .AddScoped<ClassroomService>()
                    .AddScoped<MainWindow>()
                    .AddDbContext<ManjTablesContext>(options =>
                    {
                        options.UseSqlite(@"Data Source=O:\Apps From Chris\Data\ManjTables Data\manjtables.db");
                    });
            services.AddSingleton<IDatabaseConnection, DatabaseConnection>();
            services.AddSingleton(provider =>  new DispatcherService(Current.Dispatcher));
            // Add other services and dependencies...
        }


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = _serviceProvider.GetService<MainWindow>();
            if (mainWindow != null)
            {
                mainWindow.Show();
            }
            else
            {
                // Handle the error appropriately
                // For example, log the error or show an error message

                MessageBox.Show("Error creating the main window.");
            }
        }

    }
}
