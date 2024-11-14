using Middleware.Middleware;

namespace Middleware
{
    public class Startup
    {
        // Đăng ký các dịch vụ sử dụng bởi ứng dụng, services là một DI container
        public void ConfigureServices(IServiceCollection services)
        {
            // Thêm dịch vụ dùng bộ nhớ lưu cache (session sử dụng dịch vụ này)
            services.AddDistributedMemoryCache();
            // Thêm  dịch vụ Session, dịch vụ này cung cấp Middleware
            services.AddSession();

            services.AddTransient<FrontMiddleware>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseFront();

            // Thêm StaticFileMiddleware - nếu Request là yêu cầu truy cập file tĩnh,
            // Nó trả ngay về Response nội dung file và là điểm cuối pipeline, nếu  khác
            // nó gọi  Middleware phía sau trong Pipeline
            app.UseStaticFiles();

            // Thêm SessionMiddleware:  khôi phục, thiết lập - tạo ra session
            // gán context.Session, sau đó chuyển gọi ngay middleware tiếp theo trong pipeline
            app.UseSession();

            app.UseCheckAccess();

            // Thêm EndpointRoutingMiddleware: ánh xạ Request gọi đến Endpoint (Middleware cuối)
            // phù hợp định nghĩa bởi EndpointMiddleware
            app.UseRouting();

            // app.UseEndpoint dùng để xây dựng các endpoint - điểm cuối  của pipeline theo Url truy cập
            app.UseEndpoints(endpoints =>
            {
                // EndPoint(1) khi truy vấn đến /Testpost với phương thức post hoặc put
                endpoints.MapMethods("/Testpost", new string[] { "post", "put" }, async context => {
                    await context.Response.WriteAsync("post/pust");
                });

                // EndPoint(2) -  Middleware khi truy cập /Home với phương thức GET - nó làm Middleware cuối Pipeline
                endpoints.MapGet("/Home", async context => {
                    int? count = context.Session.GetInt32("count");
                    count = (count != null) ? count + 1 : 1;
                    context.Session.SetInt32("count", count.Value);
                    await context.Response.WriteAsync($"Home page! {count}");
                });
            });

            // EndPoint(3)  app.Run tham số là hàm delegate tham số là HttpContex
            // - nó tạo điểm cuối của pipeline.
            app.Run(async context => {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync("Page not found");
            });
        }
    }
}
