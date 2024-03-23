using MapDat.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureServices
    {
        private const string _authScheme = JwtBearerDefaults.AuthenticationScheme;

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<FilesConfiguration>(configuration.GetSection("FilesConfiguration"));

            services.AddAuthentication(_authScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = configuration["Identity:Url"];
                    options.Audience = configuration["Identity:Audience"];
                    options.RequireHttpsMetadata = false;
                });

            services.AddAuthorization();
            return services;
        }
    }
}