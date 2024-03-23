
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using Is4 = IdentityServer4.Models;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Stores;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using AuthorizationServer.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthorizationServer.Services
{
    public class ClientsService : IClientsService
    {
        private readonly ConfigurationDbContext _context;
        private readonly IClientStore _clientStore;
        

        public ClientsService(
            ConfigurationDbContext context,
            IClientStore clientStore
            )
        {
            _context = context;
            _clientStore = clientStore;
        }

        public List<Is4.Client> GetClients()
        {
            var clients = _context.Clients
                .Include(x => x.AllowedCorsOrigins)
                .Include(x => x.RedirectUris)
                .Include(x => x.PostLogoutRedirectUris)
                .Include(x => x.AllowedScopes)
                .Include(x => x.AllowedGrantTypes)
                .ToList();
            var result = clients.Select(x => ClientMappers.ToModel(x)).ToList();
            return result;
        }

        public async Task<DataTablesResponse<Is4.Client>> GetForDataTables(DataTableFilterBase filters)
        {
            DataTablesResponse<Is4.Client> response = new DataTablesResponse<Is4.Client>();

            var clients = _context.Clients.ToList();

            if (filters.filterWords.Any())
            {
                clients = clients.Where(x => x.ClientId.Contains(filters.filterWords.First())).ToList();
            }

            if (filters.sortDirection == "asc")
            {
                if (filters.sortColumn == "id") clients = clients.OrderBy(x => x.Id).ToList();
            }
            else
            {
                if (filters.sortColumn == "id") clients = clients.OrderByDescending(x => x.Id).ToList();
            }

            var result = clients.Skip(filters.pageNumber * filters.pageSize).Take(filters.pageSize).Select(x => ClientMappers.ToModel(x)).ToList();

            response.Data = result;
            response.TotalPages = clients.Count();

            return response;
        }

        public async Task<Is4.Client> GetClient(string id)
        {
            //var client = await _clientStore.FindClientByIdAsync(id); nie zwraca niepoprawnie skonfigurowanych clientów


            IQueryable<IdentityServer4.EntityFramework.Entities.Client> baseQuery = _context.Clients
                .Where(x => x.ClientId == id)
                .Take(1);

            var client = await baseQuery.FirstOrDefaultAsync();
            if (client == null) return null;

            await baseQuery.Include(x => x.AllowedCorsOrigins).SelectMany(c => c.AllowedCorsOrigins).LoadAsync();
            await baseQuery.Include(x => x.AllowedGrantTypes).SelectMany(c => c.AllowedGrantTypes).LoadAsync();
            await baseQuery.Include(x => x.AllowedScopes).SelectMany(c => c.AllowedScopes).LoadAsync();
            await baseQuery.Include(x => x.Claims).SelectMany(c => c.Claims).LoadAsync();
            await baseQuery.Include(x => x.ClientSecrets).SelectMany(c => c.ClientSecrets).LoadAsync();
            await baseQuery.Include(x => x.IdentityProviderRestrictions).SelectMany(c => c.IdentityProviderRestrictions).LoadAsync();
            await baseQuery.Include(x => x.PostLogoutRedirectUris).SelectMany(c => c.PostLogoutRedirectUris).LoadAsync();
            await baseQuery.Include(x => x.Properties).SelectMany(c => c.Properties).LoadAsync();
            await baseQuery.Include(x => x.RedirectUris).SelectMany(c => c.RedirectUris).LoadAsync();

            var model = client.ToModel();

            return model;
        }

        public async Task<bool> AddClient(Is4.Client clientModel)
        {
            var entity = ClientMappers.ToEntity(clientModel);
            _context.Clients.Add(entity);
            _context.SaveChanges();
            return true;
        }

        public async Task<bool> UpdateClient(Is4.Client clientModel)
        {
            var existingClient = await GetClientWithIncludes(clientModel.ClientId);

            var modelAsEntity = ClientMappers.ToEntity(clientModel);

            existingClient.ClientName = clientModel.ClientName;
            existingClient.RequireConsent = clientModel.RequireConsent;
            existingClient.RequirePkce = clientModel.RequirePkce;
            existingClient.AllowOfflineAccess = clientModel.AllowOfflineAccess;
            existingClient.AlwaysIncludeUserClaimsInIdToken = clientModel.AlwaysIncludeUserClaimsInIdToken;
            existingClient.RequireClientSecret = clientModel.RequireClientSecret;

            existingClient.AllowedGrantTypes = modelAsEntity.AllowedGrantTypes;
            existingClient.RedirectUris = modelAsEntity.RedirectUris;
            existingClient.PostLogoutRedirectUris = modelAsEntity.PostLogoutRedirectUris;
            existingClient.AllowedCorsOrigins = modelAsEntity.AllowedCorsOrigins;
            existingClient.AllowedScopes= modelAsEntity.AllowedScopes;
            
            //existingClient.Enabled = clientModel.Enabled;

            _context.Clients.Update(existingClient);
            _context.SaveChanges();
            return true;
        }

        private async Task<IdentityServer4.EntityFramework.Entities.Client> GetClientWithIncludes(string clientId)
        {
            IQueryable<IdentityServer4.EntityFramework.Entities.Client> baseQuery = _context.Clients
               .Where(x => x.ClientId == clientId)
               .Take(1);

            var client = await baseQuery.FirstOrDefaultAsync();
            if (client == null) return null;

            await baseQuery.Include(x => x.AllowedCorsOrigins).SelectMany(c => c.AllowedCorsOrigins).LoadAsync();
            await baseQuery.Include(x => x.AllowedGrantTypes).SelectMany(c => c.AllowedGrantTypes).LoadAsync();
            await baseQuery.Include(x => x.AllowedScopes).SelectMany(c => c.AllowedScopes).LoadAsync();
            await baseQuery.Include(x => x.Claims).SelectMany(c => c.Claims).LoadAsync();
            await baseQuery.Include(x => x.ClientSecrets).SelectMany(c => c.ClientSecrets).LoadAsync();
            await baseQuery.Include(x => x.IdentityProviderRestrictions).SelectMany(c => c.IdentityProviderRestrictions).LoadAsync();
            await baseQuery.Include(x => x.PostLogoutRedirectUris).SelectMany(c => c.PostLogoutRedirectUris).LoadAsync();
            await baseQuery.Include(x => x.Properties).SelectMany(c => c.Properties).LoadAsync();
            await baseQuery.Include(x => x.RedirectUris).SelectMany(c => c.RedirectUris).LoadAsync();

            return client;
        }

        public async Task<List<string>> GetPossibleAllowedScopes()
        {
            var apiResources = _context.ApiResources.ToList();
            //var model = apiResources.Select(x => ApiResourceMappers.ToModel(x)).ToList();
            var identityResources = _context.IdentityResources.ToList();

            var result = apiResources.Select(x => x.Name).Concat(identityResources.Select(y => y.Name)).ToList();
            
            return result;

        }

        public List<string> GetAllGrantTypes()
        {
            var result = GrantTypes.ClientCredentials.ToList();
            result.AddRange(GrantTypes.Code);
            result.AddRange(GrantTypes.DeviceFlow);
            result.AddRange(GrantTypes.Hybrid);
            result.AddRange(GrantTypes.Implicit);
            result.AddRange(GrantTypes.ResourceOwnerPassword);
            return result;
        }

        public async Task<bool> DeleteClientById(string Id)
        {
            var client = _context.Clients.Where(x => x.ClientId == Id).First();
            var result = _context.Clients.Remove(client);
            _context.SaveChanges();
            return true;
        }

        public async Task<List<string>> GetClientSecrets(string clientId)
        {
            var client = await GetClientWithIncludes(clientId);

            if (client.ClientSecrets == null) client.ClientSecrets = new List<ClientSecret>();
            var secrets = client.ClientSecrets.Select(x => x.Value).ToList();
            return secrets;
        }

        public async Task<bool> RemoveClientSecret(string clientId, string secretHash)
        {
            var client = await GetClientWithIncludes(clientId);
            var secretToRemove = client.ClientSecrets.Where(x => x.Value == secretHash).FirstOrDefault();
            if (secretToRemove != null)
            {
                client.ClientSecrets.Remove(secretToRemove);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public async Task<bool> AddClientSecret(string clientId, string secret)
        {
            var client = await GetClientWithIncludes(clientId);
            ClientSecret secretEntity = new ClientSecret()
            {
                Client = client,
                Value = secret.Sha256(),
                Type = ""
            };

            if (client.ClientSecrets == null) client.ClientSecrets = new List<ClientSecret>();
            client.ClientSecrets.Add(secretEntity);
            _context.SaveChanges();
            return true;
            
        }
    }
}
