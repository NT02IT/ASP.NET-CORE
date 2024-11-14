using Microsoft.AspNetCore.Mvc;
using MVCCoreApp.Models;
using System.Text;

namespace MVCCoreApp.Controllers
{
    public class HomeController : Controller
    {
        HttpContext ctx;
        public HomeController(IHttpContextAccessor _ctx)
        {
            ctx = _ctx.HttpContext;
        }

        [Route("")]
        [Route("Home")]
        public IActionResult Index()
        {
            HomeModel message = new HomeModel();
            return View(message);
        }


        public IActionResult About()
        {
            return View();
        }
    }
}
