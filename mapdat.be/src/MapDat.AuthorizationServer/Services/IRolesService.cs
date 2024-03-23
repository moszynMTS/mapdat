using AuthorizationServer.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthorizationServer.Services
{
    public interface IRolesService
    {
        Task<IEnumerable<IdentityRole>> GetAll();
        Task<IEnumerable<IdentityRole>> GetAllWithoutSuperUser();
        void AddRole(string roleName);
        Task<List<string>> GetRoleClaims(string roleId);
        Task<List<Claim>> GetClaimsByRoleName(string roleName);
        Task<bool> AddRole(RoleModel roleModel);
        Task<bool> UpdateRole(RoleModel roleModel);
        void AddClaimToRole(string roleId, string claimName);
        void AddEnumClaimToRole(string roleId, Permissions permission);

        Task<DataTablesResponse<RoleModel>> GetForDataTables(DataTableFilterBase filters);

        Task<RoleModel> GetRole(string name);
        Task<bool> DeleteRole(string roleId);
    }
}
