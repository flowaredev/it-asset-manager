using ITAssetManagerLibrary.Models;
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
        public DbSet<ServiceLevelAgreement> ServiceLevelAgreements { get; set; }
    }
}
