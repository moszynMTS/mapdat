using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthorizationServer.Models;
using AuthorizationServer.Services;
using MapDat.AuthorizationServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static IdentityServer4.IdentityServerConstants;

namespace AuthorizationServer.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class TestController : Controller
    {
        [HttpPost("GetUsers")]
        public async Task<List<ApplicationUser>> GetUsers([FromServices] IUserService service, DataTableFilterBase filter)
        {
            var users = await service.GetAll();
            return users.ToList();
        }

        [Authorize(LocalApi.PolicyName)]
        [HttpPost("GetUsersForDataTables")]
        public async Task<DataTablesResponse<ApplicationUser>> GetUsersForDataTables([FromServices] IUserService service, DataTableFilterBase filter)
        {
            var users = await service.GetForDataTables(filter);
            return users;
        }

        [HttpGet("selectable")]
        public async Task<ActionResult<IEnumerable<UsersSelectableModel>>> GetSelectableUsers([FromServices] IUserService service, string word, bool onlyEmployees)
        {
            var result = await service.GetSelectableUsers(word, onlyEmployees);

            return result;
        }

        [HttpGet]
        public async Task<ActionResult<UserLpModel>> GetUserLp([FromServices] IUserService service, string Id)
        {
            var result = await service.GetUserLp(Id);

            return result;
        }
    }
}
