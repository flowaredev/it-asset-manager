using System.ComponentModel.DataAnnotations;

namespace ITAssetManagerLibrary.Models
{
    public class ServerDevice
    {
        public int Id { get; set; }

        [Required]
        public string Manufacturer { get; set; } = string.Empty;

        [Required]
        public string Model { get; set; } = string.Empty;

        [Required]
        public double Cpu { get; set; }

        [Required]
        public double Ram { get; set; }

        [Required]
        public double Disk { get; set; }

        [Required]
        public string Rack { get; set; } = string.Empty;

        [Timestamp]
        public byte[] Version { get; set; } = [];

        public int ServerId { get; set; }
        public Server Server { get; set; } = null!;
    }
}
