using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace InventoryManagementSystem.APIs.MiddleWare
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class UseExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<UseExceptionHandlingMiddleware> _logger;

        public UseExceptionHandlingMiddleware(RequestDelegate next, ILogger<UseExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }

            catch(Exception ex)
            {
                if (ex.InnerException is not null)
                {
                    _logger.LogError("{ExceptionType} {ExceptionMessage}",
                        ex.InnerException.GetType().ToString(),
                        ex.InnerException.Message);
                }
                else
                {
                    _logger.LogError("{ExceptionType} {ExceptionMessage}",
                        ex.GetType().ToString(),
                        ex.Message);
                }

                httpContext.Response.StatusCode = 550;
                await httpContext.Response.WriteAsync("An error occurred while processing your request.");

            }



        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    // Change this extension method
    public static class ExceptionHandlingMiddleWareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UseExceptionHandlingMiddleware>();
        }
    }
}
