using ITAssetManagerLibrary.Models;
using Microsoft.AspNetCore.Identity;

namespace ITAssetManagerLibrary.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ICollection<UserAppointment> UserAppointments { get; set; } = new List<UserAppointment>();
    }
}
