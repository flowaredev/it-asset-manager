using System.ComponentModel.DataAnnotations;

namespace ITAssetManagerLibrary.Models
{
    public class ServerDevice
    {
        public int Id { get; set; }

        public string? Manufacturer { get; set; }

        public string? Model { get; set; }

        public string? SerialNumber { get; set; }

        public double? Ram { get; set; }

        public double? Disk { get; set; }

        public string? Rack { get; set; }

        public string? NetworkType { get; set; }

        public string? MountedPhysicalServer { get; set; }

        public string? OsType { get; set; }

        public string? OsVersion { get; set; }

        public string? OsBit { get; set; }

        public double? CpuClockGhz { get; set; }

        public int? CpuCores { get; set; }

        public string? InternalDisk { get; set; }

        public string? ExternalDisk { get; set; }

        public int? NicCount { get; set; }

        public int? HbaCount { get; set; }

        public string? IpAddress { get; set; }

        public int? UnitSize { get; set; }

        public string? Notes { get; set; }

        public int ServerId { get; set; }
        public Server Server { get; set; } = null!;
    }
}
