using DependencyInjection1.Services;
using Microsoft.AspNetCore.Builder;
using static DependencyInjection1.Services.SomeService;

namespace DependencyInjection1
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();

            // Đăng ký IProductService và ProductService trong DI container
            services.AddTransient<ITransientService, SomeService>();
            services.AddScoped<IScopedService, SomeService>();
            services.AddSingleton<ISingletonService, SomeService>();
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
