using System.ComponentModel.DataAnnotations;

namespace ITAssetManagerLibrary.Models
{
    public class Server : AssetCommon
    {
        [Required]
        public required string Cpu { get; set; }

        [Required]
        public required string Ram { get; set; }

        [Required]
        public required string Disk { get; set; }
    }
}
