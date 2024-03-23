using AuthorizationServer.Data;
using AuthorizationServer.Models;
using AuthorizationServer.PolicyCode;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthorizationServer.Services
{
    public class RolesService : IRolesService
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesService(ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<IdentityRole>> GetAll()
        {
            var roles = _context.Roles.ToList();
            return roles;

            //var x = _userManager.GetUsersInRoleAsync("admin");
            //var y = await _userManager.FindByIdAsync("id");
            //var claims = _userManager.GetClaimsAsync(y);
            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<IdentityRole>> GetAllWithoutSuperUser()
        {
            var roles = _context.Roles.ToList();
            var tmp = roles.First();
            return roles.Where(x=>x.NormalizedName != "SUPERUSER");
        }

        public async Task<List<string>> GetRoleClaims(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            var claims = await _roleManager.GetClaimsAsync(role);
            var stringClaims = claims.Select(x => x.Value).ToList();
            return stringClaims;
        }

        public async Task<List<Claim>> GetClaimsByRoleName(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            var claims = await _roleManager.GetClaimsAsync(role);
            return claims.ToList();
        }

        public async void AddRole(string roleName)
        {
            bool exist = await _roleManager.RoleExistsAsync(roleName);
            if (!exist)
            {
                var role = new IdentityRole();
                role.Name = roleName;
                await _roleManager.CreateAsync(role);
            }
        }

        public async Task<bool> AddRole(RoleModel roleModel)
        {
            bool exist = await _roleManager.RoleExistsAsync(roleModel.Name);
            if (!exist)
            {
                var role = new IdentityRole();
                role.Name = roleModel.Name;
                await _roleManager.CreateAsync(role);

                foreach (var permission in roleModel.Permissions)
                {
                    var claim = permission.ToClaim();
                    await _roleManager.AddClaimAsync(role, claim);
                }
            }
            return true;
        }

        public async Task<bool> UpdateRole(RoleModel roleModel)
        {
            //bool exist = await _roleManager.RoleExistsAsync(roleModel.Name);
            var role = await _roleManager.FindByIdAsync(roleModel.Id);
            if (role != null)
            {
                role.Name = roleModel.Name;
                await _roleManager.UpdateAsync(role);

                //delete existing permissions
                var existingPermissions = (await _roleManager.GetClaimsAsync(role)).Where(x=>x.Type == CustomPermissionTypes.EnumPermission).ToList();
                foreach (var permission in existingPermissions)
                {
                    await _roleManager.RemoveClaimAsync(role, permission);
                }
                //add new permissions
                foreach (var permission in roleModel.Permissions)
                {
                    await _roleManager.AddClaimAsync(role,permission.ToClaim());
                }

            }
            return true;
        }

        public async void AddClaimToRole(string roleId, string claimName)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            Claim claim = new Claim(CustomPermissionTypes.EnumPermission, claimName);
            await _roleManager.AddClaimAsync(role, claim);
        }

        public async void AddEnumClaimToRole(string roleId, Permissions permission)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            var stringPermission = ((int)permission).ToString();
            Claim claim = new Claim(CustomPermissionTypes.EnumPermission, stringPermission);
            await _roleManager.AddClaimAsync(role, claim);
        }

        public async Task<DataTablesResponse<RoleModel>> GetForDataTables(DataTableFilterBase filters)
        {
            DataTablesResponse<RoleModel> response = new DataTablesResponse<RoleModel>();

            var roles = _context.Roles.Select(x=>new RoleModel() { 
                Id = x.Id,
                Name = x.Name
            }).ToList();

            if (filters.filterWords.Any())
            {
                roles = roles.Where(x => x.Name.Contains(filters.filterWords.First())).ToList();
            }

            if (filters.sortDirection == "asc")
            {
                if (filters.sortColumn == "id") roles = roles.OrderBy(x => x.Id).ToList();
                if (filters.sortColumn == "name") roles = roles.OrderBy(x => x.Name).ToList();
            }
            else
            {
                if (filters.sortColumn == "id") roles = roles.OrderByDescending(x => x.Id).ToList();
                if (filters.sortColumn == "name") roles = roles.OrderByDescending(x => x.Name).ToList();
            }

            var filteredRoles = roles.Skip(filters.pageNumber * filters.pageSize).Take(filters.pageSize).ToList();


            response.Data = filteredRoles;
            response.TotalPages = roles.Count();

            return response;
        }

        public async Task<RoleModel> GetRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            var roleClaims = await _roleManager.GetClaimsAsync(role);
            var rolePermissions = roleClaims.Where(x => x.Type == CustomPermissionTypes.EnumPermission).Select(x => x.ToPermission()).ToList();

            RoleModel model = new RoleModel();
            model.Id = role.Id;
            model.Name = role.Name;
            model.Permissions = rolePermissions;

            return model;
        }

        public async Task<bool> DeleteRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
