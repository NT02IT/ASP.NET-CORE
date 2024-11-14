using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Threading.Tasks;

namespace Middleware.Middleware
{
    public class CheckAcessMiddleware
    {
        // Lưu middlewware tiếp theo trong Pipeline
        private readonly RequestDelegate _next;

        public CheckAcessMiddleware(RequestDelegate next){
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path == "/testxxx")
            {
                Console.WriteLine("CheckAcessMiddleware: Cấm truy cập");
                await Task.Run(
                  async () =>
                  {
                      string html = "<h1>CAM KHONG DUOC TRUY CAP</h1>";
                      httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                      await httpContext.Response.WriteAsync(html);
                  }
                );
            }
            else
            {
                // Thiết lập Header cho HttpResponse
                httpContext.Response.Headers.Add("throughCheckAcessMiddleware", new[] { DateTime.Now.ToString() });
                Console.WriteLine("CheckAcessMiddleware: Cho truy cập");

                // Chuyển Middleware tiếp theo trong pipeline
                await _next(httpContext);
            }
        }
    }

    // Mở rộng cho IApplicationBuilder phương thức UseCheckAccess
    public static class CheckAcessMiddlewareExtensions
    {
        public static IApplicationBuilder UseCheckAccess(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CheckAcessMiddleware>();
        }
    }
}
