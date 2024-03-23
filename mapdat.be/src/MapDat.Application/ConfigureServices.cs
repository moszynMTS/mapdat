using DinkToPdf;
using DinkToPdf.Contracts;
using MapDat.Application.Profiles;
using MapDat.Domain.Authorization;
using MapDat.Persistance.Context;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            services.AddScoped<IMapDatDbContext, MapDatDbContext>();
            services.AddScoped<ICurrentUser, CurrentUserService>();

            services.AddAutoMapper(typeof(AutomapperProfile).Assembly);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
            services.AddSingleton<IConverter>(new SynchronizedConverter(new PdfTools()));
            return services;

            // part name label
        }
    }
}