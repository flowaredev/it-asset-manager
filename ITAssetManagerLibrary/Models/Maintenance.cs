using ITAssetManagerLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITAssetManagerLibrary.Models
{
    public class Maintenance
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public DateTime VisitDateTime { get; set; } = TimeZoneHelper.GetKoreaTimeNow();

        [Required]
        public DateTime ResolveDateTime { get; set; } = TimeZoneHelper.GetKoreaTimeNow();

        public int CommonAssetId { get; set; }
        public CommonAsset CommonAsset { get; set; } = null!;
    }
}
