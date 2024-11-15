using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ASPNetCoreForms.Models;

namespace ASPNetCoreForms.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(ProductEditModel model)
    {
        string message = "";

        if (ModelState.IsValid)
        {
            message = "product " + model.Name + " Rate " + model.Rate.ToString() + " With Rating " + model.Rating.ToString() + " created successfully";
        }
        else
        {
            message = "Failed to create the product. Please try again";
        }
        return Content(message);
    }
}

