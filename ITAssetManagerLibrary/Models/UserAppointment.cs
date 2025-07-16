using ITAssetManagerLibrary.Data;
using ITAssetManagerLibrary.Helpers;
using System.ComponentModel.DataAnnotations;

namespace ITAssetManagerLibrary.Models
{
    public class UserAppointment
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required]
        public DateTime Start { get; set; } = TimeZoneHelper.GetKoreaTimeNow();

        [Required]
        public DateTime End { get; set; } = TimeZoneHelper.GetKoreaTimeNow();

        public bool AllDay { get; set; } = false;

        public string? RecurrenceRule { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        public ApplicationUser User { get; set; } = null!;
    }
}