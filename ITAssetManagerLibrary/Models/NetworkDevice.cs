namespace ITAssetManagerLibrary.Models;

public class NetworkDevice
{
    public int Id { get; set; }

    public string? Manufacturer { get; set; }

    public string? Model { get; set; }

    public string? SerialNumber { get; set; }

    public string? OsVersion { get; set; }

    public string? MainMemory { get; set; }

    public string? FlashMemory { get; set; }

    public string? IpAddress { get; set; }

    public string? Rack { get; set; }

    public int? UnitSize { get; set; }

    public string? Notes { get; set; }

    public int NetworkEquipmentId { get; set; }
    public NetworkEquipment NetworkEquipment { get; set; } = null!;
}
