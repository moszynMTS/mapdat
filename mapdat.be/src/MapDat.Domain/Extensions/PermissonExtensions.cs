using MapDat.Domain.Authorization;
using System.ComponentModel;
using System.Security.Claims;

namespace MapDat.Domain.Extensions
{
    public static class PermissionExtensions
    {
        public static Permission? ConvertFromString(string permission)
        {
            if (int.TryParse(permission, out var intValue))
            {
                return (Permission)intValue;
            }

            return null;
        }

        public static bool ThisPermissionIsAllowed(this List<Claim> packedPermissions, string permissionName)
        {
            var usersPermissions = packedPermissions.UnpackPermissionsFromClaims().ToArray();

            if (!Enum.TryParse(permissionName, true, out Permission permissionToCheck))
                throw new InvalidEnumArgumentException($"{permissionName} could not be converted to a {nameof(Permission)}.");

            return usersPermissions.UserHasThisPermission(permissionToCheck);
        }

        public static Claim ToClaim(this Permission enumPermission)
        {
            return new Claim(CustomPermissionTypes.EnumPermission, ((int)enumPermission).ToString());
        }

        public static Permission ToPermission(this Claim claimPermission)
        {
            if (claimPermission == null || claimPermission.Type != CustomPermissionTypes.EnumPermission)
                throw new ArgumentNullException(nameof(claimPermission));
            int numberValue;
            int.TryParse(claimPermission.Value, out numberValue);
            return (Permission)numberValue;
        }

        public static IEnumerable<Permission> UnpackPermissionsFromClaims(this List<Claim> packedPermissions)
        {
            if (packedPermissions == null)
                throw new ArgumentNullException(nameof(packedPermissions));
            foreach (var claim in packedPermissions)
            {
                var result = ConvertFromString(claim.Value);

                if (result.HasValue)
                {
                    yield return result.Value;
                }
            }
        }

        public static bool UserHasThisPermission(this Permission[] usersPermissions, Permission permissionToCheck)
        {
            return usersPermissions.Contains(permissionToCheck) || usersPermissions.Contains(Permission.AccessAll);
        }
    }
}