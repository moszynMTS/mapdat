using System.Collections.Generic;

namespace AuthorizationServer.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class UserDetails
    {
        public string UserId { get; set; }
        public string UserName { get; set; }

        public List<string> Roles { get; set; }

        public List<string> Claims { get; set; }
    }
}
