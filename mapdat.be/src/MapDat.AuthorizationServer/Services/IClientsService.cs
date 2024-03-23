using AuthorizationServer.Models;
using IdentityServer4.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthorizationServer.Services
{
    public interface IClientsService
    {
        List<Client> GetClients();
        Task<DataTablesResponse<Client>> GetForDataTables(DataTableFilterBase filters);
        Task<Client> GetClient(string id);
        Task<bool> AddClient(Client clientModel);
        Task<bool> UpdateClient(Client clientModel);
        Task<List<string>> GetPossibleAllowedScopes();
        List<string> GetAllGrantTypes();
        Task<bool> DeleteClientById(string Id);

        Task<List<string>> GetClientSecrets(string clientId);
        Task<bool> RemoveClientSecret(string clientId, string secretHash);
        Task<bool> AddClientSecret(string clientId, string secret);
    }
}
