using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITAssetManagerLibrary.Models
{
    public class Failure
    {
        public int Id { get; set; }
        
        public DateTime FailureDateTime { get; set; } = DateTime.Now;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public DateTime VisitDateTime { get; set; } = DateTime.Now;

        [Required]
        public DateTime ResolveDateTime { get; set; } = DateTime.Now;

        [Required]
        public int DisabilityHours { get; set; }

        [Required]
        public string ResolveDescription { get; set; } = string.Empty;

        [Required]
        public bool IsResolved { get; set; } = false;

        [Timestamp]
        public byte[] Version { get; set; } = [];

        public int CommonAssetId { get; set; }
        public CommonAsset CommonAsset { get; set; } = null!;
    }
}
