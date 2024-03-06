using ITAssetManagerLibrary.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ITAssetManagerLibrary.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<CommonAsset> CommonAssets { get; set; }
        public DbSet<ServerDevice> ServerDevices { get; set; }
        public DbSet<Failure> Failures { get; set; }
    }
}
