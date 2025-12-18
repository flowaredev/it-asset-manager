using ITAssetManagerLibrary.Helpers;
using System.ComponentModel.DataAnnotations;

namespace ITAssetManagerLibrary.Models
{
    public class CommonAsset
    {
        public int Id { get; set; }

        [Required]
        public string ManagementTag { get; set; } = string.Empty;

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = string.Empty;

        [Required]
        public DateTime ApplyDateTime { get; set; } = TimeZoneHelper.GetKoreaTimeNow();

        [Required]
        public string ResponsibleCompany { get; set; } = string.Empty;

        [Required]
        public string ResponsiblePerson { get; set; } = string.Empty;

        [Required]
        public string ResponsiblePersonPhone { get; set; } = string.Empty;

        [Required]
        public string OnSiteManager { get; set; } = string.Empty;

        [Required]
        public string OnSiteManagerPhone { get; set; } = string.Empty;

        public Server? Server { get; set; }
        public Software? Software { get; set; }
        public Storage? Storage { get; set; }
        public NetworkEquipment? NetworkEquipment { get; set; }
        public SecurityEquipment? SecurityEquipment { get; set; }
        public SupportEquipment? SupportEquipment { get; set; }
        public MiscellaneousEquipment? MiscellaneousEquipment { get; set; }
        public ICollection<RoutineCheck> RoutineChecks { get; } = [];
        public ICollection<SecurityVulnerability> SecurityVulnerabilities { get; } = [];
        public ICollection<Maintenance> Maintenances { get; } = [];
        public ICollection<Failure> Failures { get; } = [];

    }
}
