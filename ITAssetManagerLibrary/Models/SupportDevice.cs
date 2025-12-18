namespace ITAssetManagerLibrary.Models;

public class SupportDevice
{
    public int Id { get; set; }

    public string? Manufacturer { get; set; }

    public string? Model { get; set; }

    public string? SerialNumber { get; set; }

    public string? DeviceSpec { get; set; }

    public string? IpAddress { get; set; }

    public string? Location { get; set; }

    public string? Notes { get; set; }

    public int SupportEquipmentId { get; set; }
    public SupportEquipment SupportEquipment { get; set; } = null!;
}
