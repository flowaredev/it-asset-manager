using System.ComponentModel.DataAnnotations;

namespace ITAssetManagerLibrary.Models
{
    public class NetworkEquipment
    {
        public int Id { get; set; }

        public int CommonAssetId { get; set; }
        public CommonAsset CommonAsset { get; set; } = null!;

        public ICollection<NetworkDevice> NetworkDevices { get; } = [];
    }
}
