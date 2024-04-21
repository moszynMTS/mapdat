using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

            return services;

            // part name label
        }
    }
}