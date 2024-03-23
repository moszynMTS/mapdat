using AuthorizationServer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;

namespace AuthorizationServer.PolicyCode
{
    public static class PermissionExtensions
    {
        public static bool ThisPermissionIsAllowed(this List<Claim> packedPermissions, string permissionName)
        {
            var usersPermissions = packedPermissions.UnpackPermissionsFromClaims().ToArray();

            if (!Enum.TryParse(permissionName, true, out Permissions permissionToCheck))
                throw new InvalidEnumArgumentException($"{permissionName} could not be converted to a {nameof(Permissions)}.");

            return usersPermissions.UserHasThisPermission(permissionToCheck);
        }

        public static Permissions ConvertFromString(string permission)
        {
            int intValue = 0;
            int.TryParse(permission, out intValue);
            if (intValue != 0)
            {
                Permissions enumPermission = (Permissions)intValue;
                return enumPermission;
            }
            else
            {
                throw new ArgumentNullException(nameof(permission));
            }
        }

        public static IEnumerable<Permissions> UnpackPermissionsFromClaims(this List<Claim> packedPermissions)
        {
            if (packedPermissions == null)
                throw new ArgumentNullException(nameof(packedPermissions));
            foreach (var claim in packedPermissions)
            {
                yield return ConvertFromString(claim.Value);
            }
        }

        public static Claim ToClaim(this Permissions enumPermission)
        {
            if (enumPermission == null)
                throw new ArgumentNullException(nameof(enumPermission));
            return new Claim(CustomPermissionTypes.EnumPermission, ((int)enumPermission).ToString());
        }

        public static Permissions ToPermission(this Claim claimPermission)
        {
            if (claimPermission == null || claimPermission.Type != CustomPermissionTypes.EnumPermission)
                throw new ArgumentNullException(nameof(claimPermission));
            int numberValue;
            int.TryParse(claimPermission.Value, out numberValue);
            return (Permissions)numberValue;
        }

        public static bool UserHasThisPermission(this Permissions[] usersPermissions, Permissions permissionToCheck)
        {
            return usersPermissions.Contains(permissionToCheck) || usersPermissions.Contains(Permissions.AccessAll);
        }
    }
}
