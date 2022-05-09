using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using siges.Models;
using System;
using System.Diagnostics;

namespace siges.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            Console.WriteLine("Home page");
            return View();
        }

        public IActionResult Privacy()
        {
            Console.WriteLine("Privacy page");
            ViewData["Message"] = "Privacy page";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
