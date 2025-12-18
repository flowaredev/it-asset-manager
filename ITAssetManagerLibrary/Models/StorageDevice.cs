using System.ComponentModel.DataAnnotations;

namespace ITAssetManagerLibrary.Models;

public class StorageDevice
{
    public int Id { get; set; }

    public string? Manufacturer { get; set; }

    public string? Model { get; set; }

    public string? SerialNumber { get; set; }

    public string? PhysicalDiskInfo { get; set; }

    public string? DiskBackupInfo { get; set; }

    public string? IpAddress { get; set; }

    public string? Rack { get; set; }

    public int? UnitSize { get; set; }

    public string? Notes { get; set; }

    public int StorageId { get; set; }
    public Storage Storage { get; set; } = null!;
}
