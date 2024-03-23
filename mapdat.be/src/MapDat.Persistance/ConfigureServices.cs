using MapDat.Persistance.Context;
using MapDat.Persistance.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AuditableEntitySaveChangesInterceptor>();

            services.AddDbContext<MapDatDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("MapDatDb"), builder => builder.MigrationsAssembly(typeof(MapDatDbContext).Assembly.FullName)));
            return services;
        }
    }
}