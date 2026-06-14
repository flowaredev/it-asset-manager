using ITAssetManagerLibrary.Helpers;
using System.ComponentModel.DataAnnotations;

namespace ITAssetManagerLibrary.Models
{
    public class OperationDocument
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string DocumentType { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        [Required]
        public DateTime UpdatedAt { get; set; } = TimeZoneHelper.GetKoreaTimeNow();

        public List<OperationDocumentComment> Comments { get; set; } = new();
    }
}
