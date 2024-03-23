using AuthorizationServer.Middleware;
using Microsoft.AspNetCore.Builder;

namespace AuthorizationServer.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
                    => app.UseMiddleware<ExceptionsMiddleware>();
    }
}