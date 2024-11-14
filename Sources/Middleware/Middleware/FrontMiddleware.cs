using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Threading.Tasks;

namespace Middleware.Middleware
{
    public class FrontMiddleware : IMiddleware
    {
        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Console.Clear();
            Console.WriteLine("FrontMiddleware: " + context.Request.Path);
            return next(context);
        }
    }

    public static class FrontMiddlewareExtensions
    {
        public static IApplicationBuilder UseFront(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FrontMiddleware>();
        }
    }
}
