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

        [MaxLength(256)]
        public string? AuthorUserName { get; set; }

        [MaxLength(100)]
        public string? AuthorName { get; set; }

        public List<OperationDocumentComment> Comments { get; set; } = new();
        public List<OperationDocumentAttachment> Attachments { get; set; } = new();
    }
}
