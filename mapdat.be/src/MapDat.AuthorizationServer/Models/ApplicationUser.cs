using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AuthorizationServer.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(64)]
        public string Firstname { get; set; }

        [MaxLength(64)]
        public string Lastname { get; set; }
        public int Lp { get; set; }
    }
}