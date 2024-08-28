using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;

namespace ManjTables.DataModels
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ManjTablesContext>
    {
        public ManjTablesContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ManjTablesContext>();
            // Replace with your actual DB connection string
            optionsBuilder.UseSqlite("Data Source=manjtables.db");

            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = loggerFactory.CreateLogger<ManjTablesContext>();

            return new ManjTablesContext(optionsBuilder.Options, logger);
        }
    }
}
