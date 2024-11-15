using DependencyInjection.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace DependencyInjection.Controllers
{
    public class HomeController : Controller
    {
        private IProductService _productService;
        public HomeController(IProductService productService)
        {
            _productService = productService;
        }


        [Route("")]
        [Route("Home")]
        public IActionResult Index()
        {
            return View(_productService.getAll());
        }
    }
}
