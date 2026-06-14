using System.ComponentModel.DataAnnotations;

namespace ITAssetManagerLibrary.Models
{
    public class OperationDocumentAttachment
    {
        public int Id { get; set; }

        [Required]
        public int OperationDocumentId { get; set; }

        public OperationDocument OperationDocument { get; set; } = null!;

        [Required]
        [MaxLength(260)]
        public string RelativePath { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string OriginalFileName { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string StoredFileName { get; set; } = string.Empty;

        [Required]
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}
