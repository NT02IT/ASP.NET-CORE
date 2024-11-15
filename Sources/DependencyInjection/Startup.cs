using DependencyInjection.Services;
using Microsoft.AspNetCore.Builder;

namespace DependencyInjection
{
    public class Startup
    {
        IWebHostEnvironment _env;
        public Startup(IWebHostEnvironment env)
        {
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();

            // Đăng ký IProductService và ProductService trong DI container
            // services.AddTransient<IProductService, BetterProductService>();

            if (_env.IsProduction())
            {
                services.AddTransient<IProductService, BetterProductService>();
            }
            else
            {
                services.AddTransient<IProductService, ProductService>();
            }
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run(async context => {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync("Page not found");
            });
        }
    }
}
