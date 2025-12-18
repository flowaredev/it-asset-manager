using System.ComponentModel.DataAnnotations;

namespace ITAssetManagerLibrary.Models
{
    public class SecurityEquipment
    {
        public int Id { get; set; }


        public int CommonAssetId { get; set; }
        public CommonAsset CommonAsset { get; set; } = null!;

        public ICollection<SecurityDevice> SecurityDevices { get; } = [];
    }
}
