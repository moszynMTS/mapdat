using AuthorizationServer.Models;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthorizationServer.Services
{
    public class CustomProfileService : DefaultProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRolesService _rolesService;
        private readonly IUserService _userService;

        public CustomProfileService(
            UserManager<ApplicationUser> userManager, 
            IRolesService rolesService,
            IUserService userService,
            ILogger<DefaultProfileService> logger
            )
            : base(logger)
        {
            _userManager = userManager;
            _rolesService = rolesService;
            _userService = userService;
        }

        public override async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            await base.GetProfileDataAsync(context);

            var user = await _userManager.GetUserAsync(context.Subject);
            
            if (user != null)
            {
                //var claims = await _userManager.GetClaimsAsync(user);

                await AddCustomUserClaims(context, user);
                await AddRoleClaims(context, user);
                await AddDataKey(context, user);

                //var roleClaim = claims.Where(x => string.CompareOrdinal(x.Type, "EnumClaim") == 0).FirstOrDefault();
                //AddClaim(context, roleClaim);

                //var virtualClaim = new Claim(JwtClaimTypes.Locale, "pl");
                //AddClaim(context, virtualClaim);
            }
        }

        private async Task<bool> AddCustomUserClaims(ProfileDataRequestContext context, ApplicationUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            context.IssuedClaims.AddRange(claims);
            return true;
        }

        private async Task<bool> AddRoleClaims(ProfileDataRequestContext context, ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                var roleClaims = await _rolesService.GetClaimsByRoleName(role);
                context.IssuedClaims.AddRange(roleClaims);
            }
            return true;
        }

        private async Task<bool> AddDataKey(ProfileDataRequestContext context, ApplicationUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            var dataKey = claims.Where(x => string.CompareOrdinal(x.Type, CustomPermissionTypes.DataKey) == 0).FirstOrDefault();
            AddClaim(context, dataKey);
            return true;
        }

        private void AddClaim(ProfileDataRequestContext context, Claim claim)
        {
            if (context != null && claim != null && !context.IssuedClaims.Any(x => string.CompareOrdinal(x.Type, claim.Type) == 0))
            {
                context.IssuedClaims.Add(claim);
            }
        }
    }
}
