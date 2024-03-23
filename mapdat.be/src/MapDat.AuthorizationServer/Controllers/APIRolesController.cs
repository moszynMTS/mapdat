using AuthorizationServer.Models;
using AuthorizationServer.PolicyCode;
using AuthorizationServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace AuthorizationServer.Controllers
{
    [ApiController]
    [Authorize(LocalApi.PolicyName)]
    public class ApiRolesController : ControllerBase
    {
        [HttpGet("GetRoles")]
        [HasPermission(Models.Permissions.RoleRead)]
        public async Task<List<string>> GetRoles([FromServices] IRolesService service)
        {
            var roles = new List<IdentityRole>();
            if (User.HasClaim(CustomPermissionTypes.EnumPermission, ((int)Permissions.AccessAll).ToString()))
            {
                roles = (await service.GetAll()).ToList();
            }
            else
            {
                roles = (await service.GetAllWithoutSuperUser()).ToList();
            }
            var rolesNames = roles.Select(x => x.Name).ToList();
            return rolesNames;
        }

        [HttpPost("GetRolesForDataTables")]
        [HasPermission(Models.Permissions.RoleRead)]
        public async Task<DataTablesResponse<RoleModel>> GetRolesForDataTables([FromServices] IRolesService service, DataTableFilterBase filter)
        {
            var roles = await service.GetForDataTables(filter);
            return roles;
        }

        [HttpPost("CreateOrUpdateRole")]
        [HasPermission(Models.Permissions.RoleChange)]
        public async Task<IActionResult> CreateOrUpdateRole([FromServices] IRolesService service, RoleModel role)
        {
            if (string.IsNullOrEmpty(role.Id))
            {
                await service.AddRole(role);
            }
            else
            {
                await service.UpdateRole(role);
            }
            return Ok();
        }

        [HttpGet("GetRoleDetails")]
        [HasPermission(Models.Permissions.RoleRead)]
        public async Task<RoleModel> GetRoleDetails([FromServices] IRolesService service, string id)
        {
            var role = await service.GetRole(id);
            return role;
        }

        [HttpGet("DeleteRole")]
        [HasPermission(Models.Permissions.RoleChange)]
        public async Task<bool> DeleteRole([FromServices] IRolesService service, string roleId)
        {
            var result = await service.DeleteRole(roleId);
            return result;
        }
    }
}
