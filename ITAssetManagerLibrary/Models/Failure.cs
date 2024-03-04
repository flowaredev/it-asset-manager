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
        public required string Description { get; set; }

        public Server? CommonAsset { get; set; }
    }
}
