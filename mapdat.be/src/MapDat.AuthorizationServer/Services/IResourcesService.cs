using AuthorizationServer.Models;
using IdentityServer4.Models;
using System.Threading.Tasks;

namespace AuthorizationServer.Services
{
    public interface IResourcesService
    {
        Task<DataTablesResponse<ApiResource>> GetForDataTables(DataTableFilterBase filters);
        Task<ApiResource> GetApiResourceByName(string name);
        Task<bool> CreateApiResource(ApiResource model);
        Task<bool> UpdateApiResource(ApiResource model);
        Task<bool> DeleteResourceByName(string name);
    }
}
