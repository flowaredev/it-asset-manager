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
        public DateTime ApplyDateTime { get; set; } = DateTime.Now;

        [Timestamp]
        public byte[] Version { get; set; } = [];

        public Server? Server { get; set; }

    }
}
