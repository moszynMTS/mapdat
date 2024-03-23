using System.Threading.Tasks;
using AuthorizationServer.Models;
using AuthorizationServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationServer.Controllers
{
    public class RolesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Roles([FromServices] IRolesService service)
        {
            return View(await service.GetAll());
        }

        public async Task<IActionResult> RoleDetails([FromServices] IRolesService service, string roleId)
        {
            return View(await service.GetRoleClaims(roleId));
        }

        public IActionResult AddRole([FromServices] IRolesService service, string roleName)
        {
            service.AddRole(roleName);
            return Redirect("Roles");
        }

        public IActionResult AddClaimToRole([FromServices] IRolesService service, string roleId, string claimName)
        {
            service.AddClaimToRole(roleId, claimName);
            return Redirect("Roles");
        }

        public IActionResult AddEnumClaimToRole([FromServices] IRolesService service, string roleId, Permissions claim)
        {
            service.AddEnumClaimToRole(roleId, claim);
            return Redirect("Roles");
        }
    }
}
