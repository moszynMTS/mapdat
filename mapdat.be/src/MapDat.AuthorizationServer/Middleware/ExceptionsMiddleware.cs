using AuthorizationServer.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace AuthorizationServer.Middleware
{
    public class ExceptionsMiddleware
    {
        private readonly ILogger<ExceptionsMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ExceptionsMiddleware(RequestDelegate next, ILogger<ExceptionsMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private string CreateResponse(Response response) =>
            JsonConvert.SerializeObject(response);

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, int errorCode = 400)
        {
            _logger.LogError(exception, $"Message: {exception.Message}\nStackTrace:\n{exception.StackTrace}");

            var response = new Response<string>(System.Net.HttpStatusCode.BadRequest, exception.StackTrace);
            context.Response.StatusCode = errorCode;
            context.Response.ContentType = "application/json";
            response.AddError(exception.ExceptionMessages());

            await context.Response.WriteAsync(CreateResponse(response));
        }
    }
}