using System.ComponentModel.DataAnnotations;

namespace ITAssetManagerLibrary.Models
{
    public class BackupEquipment
    {
        public int Id { get; set; }

        [Timestamp]
        public byte[] Version { get; set; } = [];

        public int CommonAssetId { get; set; }
        public CommonAsset CommonAsset { get; set; } = null!;
    }
}
