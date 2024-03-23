
using IdentityServer4.EntityFramework.DbContexts;
using Is4 = IdentityServer4.Models;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Stores;
using System.Linq;
using System.Threading.Tasks;
using AuthorizationServer.Models;
using System;

namespace AuthorizationServer.Services
{
    public class ResourcesService : IResourcesService
    {
        private readonly ConfigurationDbContext _context;
        private readonly IResourceStore _resourceStore;



        public ResourcesService(
            ConfigurationDbContext context,
            IResourceStore resourceStore
            )
        {
            _context = context;
            _resourceStore = resourceStore;
        }

        public async Task<DataTablesResponse<Is4.ApiResource>> GetForDataTables(DataTableFilterBase filters)
        {
            DataTablesResponse<Is4.ApiResource> response = new DataTablesResponse<Is4.ApiResource>();

            var resources = _context.ApiResources.ToList();

            if (filters.filterWords.Any())
            {
                resources = resources.Where(x => x.Name.Contains(filters.filterWords.First()) || x.DisplayName.Contains(filters.filterWords.First())).ToList();
            }

            if (filters.sortDirection == "asc")
            {
                if (filters.sortColumn == "id") resources = resources.OrderBy(x => x.Id).ToList();
                if (filters.sortColumn == "name") resources = resources.OrderBy(x => x.Name).ToList();
            }
            else
            {
                if (filters.sortColumn == "id") resources = resources.OrderByDescending(x => x.Id).ToList();
                if (filters.sortColumn == "name") resources = resources.OrderByDescending(x => x.Name).ToList();
            }

            var result = resources.Skip(filters.pageNumber * filters.pageSize).Take(filters.pageSize).Select(x => ApiResourceMappers.ToModel(x)).ToList();

            response.Data = result;
            response.TotalPages = resources.Count();

            return response;
        }

        public async Task<Is4.ApiResource> GetApiResourceByName(string name)
        {
            try
            {
                var apiResources = await _resourceStore.GetAllResourcesAsync();
                var result = await _resourceStore.FindApiResourceAsync(name);
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> CreateApiResource(Is4.ApiResource model)
        {
            var x = new Is4.ApiResource(model.Name, model.DisplayName);
            var entity = ApiResourceMappers.ToEntity(x);
            _context.ApiResources.Add(entity);
            _context.SaveChanges();
            return true;
        }

        public async Task<bool> UpdateApiResource(Is4.ApiResource model)
        {
            var existingResource = _context.ApiResources.Where(x => x.Name == model.Name).First();
            existingResource.DisplayName = model.DisplayName;
            existingResource.Description = model.Description;
            existingResource.Enabled = model.Enabled;

            _context.ApiResources.Update(existingResource);
            _context.SaveChanges();
            return true;
        }

        public async Task<bool> DeleteResourceByName(string name)
        {
            var resource = _context.ApiResources.Where(x => x.Name == name).First();
            var result = _context.ApiResources.Remove(resource);
            _context.SaveChanges();
            return true;
        }

    }
}
