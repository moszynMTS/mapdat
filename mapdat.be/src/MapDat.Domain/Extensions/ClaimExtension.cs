using MapDat.Domain.Authorization;
using System.Security.Claims;

namespace MapDat.Domain.Extensions
{
    public static class ClaimExtension
    {
        public static Claim GetClaim(this IEnumerable<Claim> claims, string key) =>
            claims.FirstOrDefault(x => x.Type == key);

        public static string GetEmail(this IEnumerable<Claim> claims)
        {
            var claim = claims.GetClaim(ClaimTypes.Email);

            return claim?.Value;
        }

        public static string GetFullName(this IEnumerable<Claim> claims)
        {
            var givenNameClaim = claims.GetClaim(ClaimTypes.GivenName);
            var surnameClaim = claims.GetClaim(ClaimTypes.Surname);

            return $"{surnameClaim?.Value} {givenNameClaim?.Value}";
        }

        public static string GetUserId(this IEnumerable<Claim> claims)
        {
            var claim = claims.GetClaim(ClaimTypes.NameIdentifier);

            return claim?.Value;
        }

        public static List<Permission> GetUserPermissions(this IEnumerable<Claim> claims)
        {
            var permissions = claims.Where(x => x.Type == CustomPermissionTypes.EnumPermission).Select(x => x.ToPermission()).ToList();
            return permissions;
        }

        public static string GetUserRole(this IEnumerable<Claim> claims)
        {
            var claim = claims.GetClaim(ClaimTypes.Role);

            return claim != null ? claim.Value : null;
        }
    }
}
