using AuthorizationServer.Models;
using AuthorizationServer.PolicyCode;
using AuthorizationServer.Services;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace AuthorizationServer.Controllers
{
    [ApiController]
    [Authorize(LocalApi.PolicyName)]
    public class ApiResourcesController : ControllerBase
    {
        [HttpPost("GetResourcesForDataTables")]
        [HasPermission(Models.Permissions.ResourceRead)]
        public async Task<DataTablesResponse<ApiResource>> GetResourcesForDataTables([FromServices] IResourcesService service, DataTableFilterBase filter)
        {
            var resources = await service.GetForDataTables(filter);
            return resources;
        }

        [HttpGet("GetApiResource")]
        [HasPermission(Models.Permissions.ResourceRead)]
        public async Task<ApiResource> GetApiResource([FromServices] IResourcesService service, string name)
        {
            var resource = await service.GetApiResourceByName(name);
            return resource;
        }

        [HttpPost("CreateApiResource")]
        [HasPermission(Models.Permissions.ResourceChange)]
        public async Task<IActionResult> CreateApiResource([FromServices] IResourcesService service, ApiResource model)
        {   
            var resource = await service.CreateApiResource(model);
            return Ok();
        }

        [HttpPost("UpdateApiResource")]
        [HasPermission(Models.Permissions.ResourceChange)]
        public async Task<IActionResult> UpdateApiResource([FromServices] IResourcesService service, ApiResource model)
        {
            var resource = await service.UpdateApiResource(model);
            return Ok();
        }

        [HttpGet("DeleteResource")]
        [HasPermission(Models.Permissions.ResourceChange)]
        public async Task<bool> DeleteResource([FromServices] IResourcesService service, string name)
        {
            var result = await service.DeleteResourceByName(name);
            return result;
        }
    }
}
