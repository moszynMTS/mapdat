using System.Collections.Generic;

namespace AuthorizationServer.Models
{
    public class RoleModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Permissions> Permissions { get; set; }
    }
}
