using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthorizationServer.Logic
{
    public class ClaimsTransformer : IClaimsTransformation
    {
        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            ((ClaimsIdentity)principal.Identity).AddClaim(new Claim(JwtClaimTypes.Locale, "pl"));
            return Task.FromResult(principal);
        }
    }
}
