namespace ITAssetManagerLibrary.Models;

public class SecurityDevice
{
    public int Id { get; set; }

    public string? Manufacturer { get; set; }

    public string? Model { get; set; }

    public string? SerialNumber { get; set; }

    public string? DeviceSpec { get; set; }

    public string? IpAddress { get; set; }

    public string? Rack { get; set; }

    public int? UnitSize { get; set; }

    public string? Notes { get; set; }

    public int SecurityEquipmentId { get; set; }
    public SecurityEquipment SecurityEquipment { get; set; } = null!;
}
