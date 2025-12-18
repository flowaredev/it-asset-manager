using ITAssetManagerLibrary.Models;
using ITAssetManagerLibrary.Constants;
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
        public DbSet<StorageDevice> StorageDevices { get; set; }
        public DbSet<BackupEquipment> BackupEquipments { get; set; }
        public DbSet<NetworkEquipment> NetworkEquipments { get; set; }
        public DbSet<NetworkDevice> NetworkDevices { get; set; }
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
                const string administratorEmail = "administrator@itsm.com";

                // First, ensure all roles exist
                foreach (var roleName in RoleConstants.AllRoles)
                {
                    var role = context.Set<IdentityRole>().FirstOrDefault(r => r.Name == roleName);
                    if (role == null)
                    {
                        role = new IdentityRole
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = roleName,
                            NormalizedName = roleName.ToUpper(),
                            ConcurrencyStamp = Guid.NewGuid().ToString()
                        };
                        context.Set<IdentityRole>().Add(role);
                    }
                }
                context.SaveChanges();

                // Create administrator user if it doesn't exist
                var administrator = context.Set<ApplicationUser>().FirstOrDefault(u => u.UserName == administratorEmail);
                if (administrator == null)
                {
                    administrator = new ApplicationUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = administratorEmail,
                        Email = administratorEmail,
                        NormalizedEmail = administratorEmail.ToUpper(),
                        NormalizedUserName = administratorEmail.ToUpper(),
                        EmailConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        ConcurrencyStamp = Guid.NewGuid().ToString()
                    };
                    var passwordHasher = new PasswordHasher<ApplicationUser>();
                    administrator.PasswordHash = passwordHasher.HashPassword(administrator, "Admin@1234");
                    context.Set<ApplicationUser>().Add(administrator);
                    context.SaveChanges();

                    // Assign Administrator role to the administrator user
                    var administratorRole = context.Set<IdentityRole>().FirstOrDefault(r => r.Name == RoleConstants.Administrator);
                    if (administratorRole != null)
                    {
                        var userRole = new IdentityUserRole<string>
                        {
                            UserId = administrator.Id,
                            RoleId = administratorRole.Id
                        };
                        context.Set<IdentityUserRole<string>>().Add(userRole);
                        context.SaveChanges();
                    }
                }
            });
        }
    }
}
