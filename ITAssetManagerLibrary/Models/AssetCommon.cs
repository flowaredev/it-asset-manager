using System.ComponentModel.DataAnnotations;

namespace ITAssetManagerLibrary.Models
{
    public class AssetCommon
    {
        public int Id { get; set; }

        [Required]
        public required string ManagementTag { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Role { get; set; }

        [Required]
        public required string Manufacturer { get; set; }

        [Required]
        public required string Model { get; set; }

        [Required]
        public DateTime ApplyDateTime { get; set; } = DateTime.Now;

        [Timestamp]
        public required byte[] Version { get; set; }
    }
}
