using MapDat.Domain.Extensions;
using System.Security.Claims;

namespace MapDat.Domain.Authorization
{
    public class CurrentUserService : ICurrentUser
    {
        private string _email;
        private string _id;
        private string _fullName;

        public CurrentUserService()
        {
            Permissions = new List<Permission>();
        }

        public string Email => GetEmail();
        public string Id => GetId();

        public string FullName => GetFullName();
        public List<Permission> Permissions { get; private set; }

        public bool HasPermision(Permission permission) => Permissions.Contains(permission) || Permissions.Contains(Permission.AccessAll);

        public Roles? Roles { get; private set; }

        public void SetCurrentUser(IEnumerable<Claim> claims)
        {
            SetUser(claims);
            SetRole(claims);
            SetPermissions(claims);
        }

      //  public void SetCurrentUser(string email)
       // {
       //     _email = email;
      //  }

        private string GetEmail()
        {
            if (_email == null)
            {
                throw new ArgumentNullException("Current user id is null or empty!");
            }

            return _email;
        }

        private string GetId()
        {
            if (_id == null)
            {
                throw new ArgumentNullException("Current user id is null or empty!");
            }

            return _id;
        }

        private string GetFullName()
        {
            if (_fullName == null)
            {
                _fullName = "admin admin";
            }

            return _fullName;
        }

        private void SetPermissions(IEnumerable<Claim> claims)
        {
            Permissions = claims.GetUserPermissions();
        }

        private void SetRole(IEnumerable<Claim> claims)
        {
            var role = claims.GetUserRole();
            if (role == null)
            {
                role = "None";
            }
        }

        private void SetUser(IEnumerable<Claim> claims)
        {
            _email = claims.GetEmail();
            _id = claims.GetUserId();
            _fullName = claims.GetFullName();
        }
    }
}
