using System.Collections.Generic;

namespace AuthorizationServer.Models
{
    public class UserModel
    {
        public string DataKey { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public List<Permissions> Permissions { get; set; }
        public List<string> Roles { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}