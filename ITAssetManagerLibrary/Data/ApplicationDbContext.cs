using ITAssetManagerLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ITAssetManagerLibrary.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<CommonAsset> CommonAssets { get; set; }
        public DbSet<ServerDevice> ServerDevices { get; set; }
        public DbSet<Server> Servers { get; set; }
        public DbSet<Software> Softwares { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<BackupEquipment> BackupEquipments { get; set; }
        public DbSet<NetworkEquipment> NetworkEquipments { get; set; }
        public DbSet<SecurityEquipment> SecurityEquipments { get; set; }
        public DbSet<SupportEquipment> SupportEquipments { get; set; }
        public DbSet<MiscellaneousEquipment> MiscellaneousEquipments { get; set; }
        public DbSet<Failure> Failures { get; set; }
        public DbSet<Maintenance> Maintenances { get; set; }
        public DbSet<ServiceLevelAgreement> ServiceLevelAgreements { get; set; }
        public DbSet<RoutineCheck> RoutineChecks { get; set; }
        public DbSet<SecurityVulnerability> SecurityVulnerabilities { get; set; }
        public DbSet<UserAppointment> UserAppointments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSeeding((context, _) =>
            {
                // Seed data can be added here if needed
                var administrator = context.Set<ApplicationUser>().FirstOrDefault(u => u.UserName == "administrator");
                if (administrator == null)
                {
                    administrator = new ApplicationUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = "administrator@itsm.com",
                        Email = "administrator@itsm.com"
                    };
                    var passwordHasher = new PasswordHasher<ApplicationUser>();
                    administrator.PasswordHash = passwordHasher.HashPassword(administrator, "Admin@1234");
                    context.Set<ApplicationUser>().Add(administrator);
                    context.SaveChanges();
                }
            });
        }
    }
}
