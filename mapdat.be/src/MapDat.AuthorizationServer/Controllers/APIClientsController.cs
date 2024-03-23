using AuthorizationServer.Models;
using AuthorizationServer.PolicyCode;
using AuthorizationServer.Services;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace AuthorizationServer.Controllers
{
    [ApiController]
    [Authorize(LocalApi.PolicyName)]
    public class ApiClientsController : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet("GetClients")]
        [HasPermission(Models.Permissions.ClientRead)]
        public async Task<List<Client>> GetClients([FromServices] IClientsService service)
        {
            var clients = service.GetClients();
            return clients;
        }

        [HttpPost("GetClientsForDataTables")]
        [HasPermission(Models.Permissions.ClientRead)]
        public async Task<DataTablesResponse<Client>> GetClientsForDataTables([FromServices] IClientsService service, DataTableFilterBase filter)
        {
            var clients = await service.GetForDataTables(filter);
            return clients;
        }

        [HttpGet("GetClient")]
        [HasPermission(Models.Permissions.ClientRead)]
        public async Task<Client> GetClient([FromServices] IClientsService service, string id)
        {
            var client = await service.GetClient(id);
            return client;
        }

        [HttpGet("GetAllGrantTypes")]
        [HasPermission(Models.Permissions.ClientRead)]
        public async Task<List<string>> GetAllGrantTypes([FromServices] IClientsService service)
        {
            var grantTypes = service.GetAllGrantTypes();
            return grantTypes;
        }

        [HttpGet("GetPossibleAllowedScopes")]
        [HasPermission(Models.Permissions.ClientRead)]
        public async Task<List<string>> GetPossibleAllowedScopes([FromServices] IClientsService service)
        {
            var grantTypes = await service.GetPossibleAllowedScopes();
            return grantTypes;
        }

        [HttpPost("CreateClient")]
        [HasPermission(Models.Permissions.ClientChange)]
        public async Task<IActionResult> CreateClient([FromServices] IClientsService service, ClientDTO model)
        {
            var resource = await service.AddClient(model.ToClient());
            return Ok();
        }

        [HttpPost("UpdateClient")]
        [HasPermission(Models.Permissions.ClientChange)]
        public async Task<IActionResult> UpdateClient([FromServices] IClientsService service, ClientDTO model)
        {
            var resource = await service.UpdateClient(model.ToClient());
            return Ok();
        }

        [HttpGet("DeleteClient")]
        [HasPermission(Models.Permissions.ClientChange)]
        public async Task<bool> DeleteClient([FromServices] IClientsService service, string clientId)
        {
            var result = await service.DeleteClientById(clientId);
            return result;
        }

        [HttpGet("GetClientSecrets")]
        [HasPermission(Models.Permissions.ClientRead)]
        public async Task<List<string>> GetClientSecrets([FromServices] IClientsService service, string clientId)
        {
            var result = await service.GetClientSecrets(clientId);
            return result;
        }

        [HttpGet("RemoveClientSecret")]
        [HasPermission(Models.Permissions.ClientChange)]
        public async Task<bool> RemoveClientSecret([FromServices] IClientsService service, string clientId, string secretHash)
        {
            var result = await service.RemoveClientSecret(clientId,secretHash);
            return result;
        }

        [HttpGet("AddClientSecret")]
        [HasPermission(Models.Permissions.ClientChange)]
        public async Task<bool> AddClientSecret([FromServices] IClientsService service, string clientId, string secret)
        {
            var result = await service.AddClientSecret(clientId, secret);
            return result;
        }
    }
}
