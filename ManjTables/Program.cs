using ManjTables.SQLiteReaderLib;
using ManjTables.SQLiteReaderLib.Connection;
using ManjTables.SQLiteReaderLib.Reader;
using ManjTables.ObjectMapping.ChildInfoMapping;
using ManjTables.ObjectMapping.FormTemplatesMapping;
using ManjTables.JsonParsing;
using ManjTables.ObjectMapping.TablesMapping;
using ManjTables.DataModels;
using Microsoft.EntityFrameworkCore;
using ManjTables.ObjectMapping.ClassroomsMapping;

namespace ManjTables
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                // Get the full path of the executable
                string exePath = AppDomain.CurrentDomain.BaseDirectory;

                // Combine the path with the 'Data' folder
                string dataFolderPath = Path.Combine(exePath, "Data");

                // Check if the directory exists
                if (Directory.Exists(dataFolderPath))
                {
                    // Delete the directory
                    Directory.Delete(dataFolderPath, true);
                }                
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            IHost host = Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureServices(services =>
                {
                    services.AddDbContext<ManjTablesContext>();
                    services.AddHostedService<Worker>();
                    services.AddScoped<WorkerUtility>();
                    services.AddScoped<ISQLiteReader, SQLiteReader>();
                    services.AddScoped<SQLiteReaderService>();
                    services.AddScoped<IDatabaseConnection, DatabaseConnection>();
                    services.AddScoped<IChildInfoMapper, ChildInfoMapper>();
                    services.AddScoped<IFormTemplateIdsMapper, FormTemplateIdsMapper>();
                    services.AddScoped<IFormsJsonParser, FormsJsonParser>();
                    services.AddScoped<ITablesMapper, TablesMapper>();
                    services.AddScoped<IClassroomsMapper, ClassroomsMapper>();
                    services.AddScoped<DataTransformationService>();
                    services.AddScoped<StaffProcessingService>();
                    services.AddScoped<ParentService>();
                    services.AddScoped<AddressService>();
                    services.AddScoped<DatabaseHandler>();
                })
                .Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ManjTablesContext>();
                context.Database.Migrate();
            }

            host.Run();
        }
    }
}
