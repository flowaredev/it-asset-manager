using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITAssetManagerLibrary.Models
{
    public class RoutineCheck
    {
        public int Id { get; set; }

        [Required]
        public string Detail { get; set; } = string.Empty;

        [Required]
        public DateTime StartDateTime { get; set; } = DateTime.Now;

        [Required]
        public DateTime EndDateTime { get; set; } = DateTime.Now;

        [Timestamp]
        public byte[] Version { get; set; } = [];

        public int CommonAssetId { get; set; }
        public CommonAsset CommonAsset { get; set; } = null!;
    }
}
