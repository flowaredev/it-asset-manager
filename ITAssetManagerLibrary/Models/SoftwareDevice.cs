namespace ITAssetManagerLibrary.Models;

public class SoftwareDevice
{
    public int Id { get; set; }

    public string? Manufacturer { get; set; }

    public string? ProgramName { get; set; }

    public string? SerialNumber { get; set; }

    public string? Notes { get; set; }

    public int SoftwareId { get; set; }
    public Software Software { get; set; } = null!;
}
