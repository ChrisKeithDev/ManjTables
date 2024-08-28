using Microsoft.EntityFrameworkCore;
using ManjTables.DataModels.Models;
using ManjTables.DataModels.Models.Forms;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace ManjTables.DataModels
{
    public class ManjTablesContext : DbContext
    {
        private readonly ILogger<ManjTablesContext> _logger;

        public ManjTablesContext(DbContextOptions<ManjTablesContext> options, ILogger<ManjTablesContext> logger) 
            : base(options)
        {
            _logger = logger;
        }
        #region DbSets
        public DbSet<Address> Addresses { get; set; }
        public DbSet<AnimalApprovalForm> AnimalApprovalForms { get; set; }
        public DbSet<ApprovedAdult> ApprovedAdults { get; set; }
        public DbSet<ApprovedAdultAndPickupForm> ApprovedAdultAndPickupForms { get; set; }
        public DbSet<ApprovedPickupForm> ApprovedPickupForms { get; set; }
        public DbSet<Child> Childs { get; set; }
        public DbSet<ChildApprovedAdult> ChildApprovedAdults { get; set; }
        public DbSet<ChildEmergencyContact> ChildEmergencyContacts { get; set; }
        public DbSet<ChildParent> ChildParents { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<EmergencyContact> EmergencyContacts { get; set; }
        public DbSet<EmergencyContactEmergencyInformationForm> EmergencyContactEmergencyInformationForms { get; set; }
        public DbSet<EmergencyInformationForm> EmergencyInformationForms { get; set; }
        public DbSet<FormTemplateIds> FormTemplateIdss { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<PhotoReleaseForm> PhotoReleaseForms { get; set; }
        public DbSet<Programme> Programmes { get; set; }
        public DbSet<StaffClassroom> StaffClassrooms { get; set; }
        public DbSet<StaffMember> StaffMembers { get; set; }
        public DbSet<GoingOutPermissionForm> GoingOutPermissionForms { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Child>().HasIndex(p => p.BirthDate);
            modelBuilder.Entity<Child>().HasIndex(p => p.Hours);
            modelBuilder.Entity<Child>().HasIndex(p => p.AddressId);
            modelBuilder.Entity<Child>().HasIndex(p => p.ClassroomIds);
            modelBuilder.Entity<Parent>().HasIndex(p => p.AddressId);
            modelBuilder.Entity<AnimalApprovalForm>().HasIndex(p => p.ChildId).IsUnique();
            modelBuilder.Entity<ApprovedPickupForm>().HasIndex(p => p.ChildId).IsUnique();
            modelBuilder.Entity<EmergencyInformationForm>().HasIndex(p => p.ChildId).IsUnique();
            modelBuilder.Entity<PhotoReleaseForm>().HasIndex(p => p.ChildId).IsUnique();
            modelBuilder.Entity<Address>().HasIndex(p => p.StreetAddress);
            modelBuilder.Entity<Classroom>().HasIndex(p => p.ClassroomName);
            modelBuilder.Entity<Classroom>().HasIndex(p => p.Level);
            modelBuilder.Entity<Programme>().HasIndex(p => p.ClassroomIds);
            modelBuilder.Entity<StaffMember>().Ignore(p => p.ClassroomId);

            // ApprovedAdultAndPickupForm relationships
            modelBuilder.Entity<ApprovedAdultAndPickupForm>()
                .HasOne(a => a.ApprovedAdult)
                .WithMany(a => a.ApprovedAdultAndPickupForms)
                .HasForeignKey(a => a.ApprovedAdultId);

            modelBuilder.Entity<ApprovedAdultAndPickupForm>()
                .HasOne(a => a.ApprovedPickupForm)
                .WithMany(a => a.ApprovedAdultAndPickupForms)
                .HasForeignKey(a => a.PickupFormId);

            // ChildApprovedAdult relationships
            modelBuilder.Entity<ChildApprovedAdult>()
                .HasOne(c => c.Child)
                .WithMany(c => c.ChildApprovedAdults)
                .HasForeignKey(c => c.ChildId);

            modelBuilder.Entity<ChildApprovedAdult>()
                .HasOne(c => c.ApprovedAdult)
                .WithMany(c => c.ChildApprovedAdults)
                .HasForeignKey(c => c.ApprovedAdultId);

            // ChildEmergencyContact relationships
            modelBuilder.Entity<ChildEmergencyContact>()
                .HasOne(c => c.Child)
                .WithMany(c => c.ChildEmergencyContacts)
                .HasForeignKey(c => c.ChildId);

            modelBuilder.Entity<ChildEmergencyContact>()
                .HasOne(c => c.EmergencyContact)
                .WithMany(c => c.ChildEmergencyContacts)
                .HasForeignKey(c => c.EmergencyContactId);

            // ChildParent relationships
            modelBuilder.Entity<ChildParent>()
                .HasKey(cp => new { cp.ChildId, cp.ParentId });

            modelBuilder.Entity<ChildParent>()
                .HasOne(c => c.Child)
                .WithMany(c => c.ChildParents)
                .HasForeignKey(c => c.ChildId);

            modelBuilder.Entity<ChildParent>()
                .HasOne(c => c.Parent)
                .WithMany(c => c.ChildParents)
                .HasForeignKey(c => c.ParentId);

            // EmergencyContactEmergencyInformationForm relationships
            modelBuilder.Entity<EmergencyContactEmergencyInformationForm>()
                .HasOne(e => e.EmergencyContact)
                .WithMany(e => e.EmergencyContactEmergencyInformationForms)
                .HasForeignKey(e => e.EmergencyContactId);

            modelBuilder.Entity<EmergencyContactEmergencyInformationForm>()
                .HasOne(e => e.EmergencyInformationForm)
                .WithMany(e => e.EmergencyContactEmergencyInformationForms)
                .HasForeignKey(e => e.EmergencyInformationFormId);

            // StaffClassroom relationships
            modelBuilder.Entity<StaffClassroom>()
                .HasOne(s => s.StaffMember)
                .WithMany(s => s.StaffClassrooms)
                .HasForeignKey(s => s.StaffId);

            modelBuilder.Entity<StaffClassroom>()
                .HasOne(s => s.Classroom)
                .WithMany(s => s.StaffClassrooms)
                .HasForeignKey(s => s.ClassroomId);

            modelBuilder.Entity<Child>()
                .HasOne(c => c.Programme)
                .WithMany(p => p.Children)
                .HasForeignKey(c => c.ProgrammeId);

            modelBuilder.Entity<Child>()
                .HasOne(c => c.Address)
                .WithMany(a => a.Children)
                .HasForeignKey(c => c.AddressId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Parent>()
                .HasOne(p => p.Address)
                .WithMany(a => a.Parents)
                .HasForeignKey(p => p.AddressId)
                .OnDelete(DeleteBehavior.Restrict);

            // GoingOutPermissionForm relationships
            modelBuilder.Entity<GoingOutPermissionForm>()
                .HasOne<Child>()
                .WithOne()
                .HasForeignKey<GoingOutPermissionForm>(g => g.ChildId)
                .OnDelete(DeleteBehavior.Cascade);

            // Set an index on ChildId for better performance
            modelBuilder.Entity<GoingOutPermissionForm>()
                .HasIndex(g => g.ChildId)
                .IsUnique();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Load configuration
                var config = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("dbappsettings.json")
                    .Build();

                // Get the database path from the configuration
                string? dbPath = config["Database:DbPath"];

                if (dbPath != null)
                {
                    string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dbPath);

                    var dirName = Path.GetDirectoryName(fullPath);
                    if (dirName != null)
                    {
                        Directory.CreateDirectory(dirName);
                    }

                    optionsBuilder.UseSqlite($"Data Source={fullPath}");
                    _logger.LogInformation("Using SQLite database at {fullPath}", fullPath);

                }
                else
                {
                    _logger.LogWarning("Database path is not set in configuration.");
                }
            }
        }
    }
}
