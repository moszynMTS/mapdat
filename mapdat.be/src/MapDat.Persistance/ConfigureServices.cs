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
            services.AddSingleton<IMongoDBSettings>(sp =>
                sp.GetRequiredService<IOptions<MongoDBSettings>>().Value);

            services.AddSingleton<IMongoClient>(s =>
                new MongoClient(configuration.GetSection("MongoDBSettings:MongoDbConnectionString").Value));

            services.AddScoped<IWojewodztwaService, WojewodztwaService>();

            return services;
        }
    }
}