using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthorizationServer.Models;
using AuthorizationServer.PolicyCode;
using AuthorizationServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationServer.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Users([FromServices] IUserService service)
        {
            return View(await service.GetAll());
        }

        [HasPermission(Models.Permissions.UserRead)]
        public async Task<IActionResult> UserDetails([FromServices] IUserService service,string userId)
        {
            return View(await service.GetById(userId));
        }

        public IActionResult AddUserToRole([FromServices] IUserService service, string userId, string roleName)
        {
            service.AddRoleToUser(roleName,userId);
            return Redirect("Users");
        }

        public IActionResult SetDataKey([FromServices] IUserService service, string userId, string dataKey)
        {
            service.SetDataKey(userId,dataKey);
            return Redirect("Users");
        }

        public async Task<List<ApplicationUser>> Test([FromServices] IUserService service, string userId, string dataKey)
        {
            var users = await service.GetAll();
            return users.ToList(); 
        }


    }
}
