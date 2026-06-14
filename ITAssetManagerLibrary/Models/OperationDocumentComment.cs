using ITAssetManagerLibrary.Helpers;
using System.ComponentModel.DataAnnotations;

namespace ITAssetManagerLibrary.Models
{
    public class OperationDocumentComment
    {
        public int Id { get; set; }

        [Required]
        public int OperationDocumentId { get; set; }

        public OperationDocument OperationDocument { get; set; } = null!;

        [Required]
        public string Comment { get; set; } = string.Empty;

        [Required]
        public DateTime CreatedAt { get; set; } = TimeZoneHelper.GetKoreaTimeNow();

        [MaxLength(256)]
        public string? AuthorUserName { get; set; }

        [MaxLength(100)]
        public string? AuthorName { get; set; }
    }
}
