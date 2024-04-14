using MapDat.Persistance.Context;
using MapDat.Persistance.Interceptors;
using MapDat.Persistance.MongoSettings;
using MapDat.Persistance.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AuditableEntitySaveChangesInterceptor>();

            services.AddSingleton<IMongoDBSettings>(sp =>
                sp.GetRequiredService<IOptions<MongoDBSettings>>().Value);

            services.AddSingleton<IMongoClient>(s =>
                new MongoClient(configuration.GetSection("MongoDBSettings:MongoDbConnectionString").Value));

            services.AddSingleton<IWojewodztwaService, WojewodztwaService>();

            services.AddDbContext<MapDatDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("MapDatDb"), builder => builder.MigrationsAssembly(typeof(MapDatDbContext).Assembly.FullName)));
            return services;
        }
    }
}