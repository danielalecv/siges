using Microsoft.AspNetCore.Mvc;
using System;

namespace siges.Controllers
{
    //Clase de prueba, para revisar pruebas unitarias
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            Console.WriteLine("Prueba 1");
            return View();
        }

        public IActionResult Index2()
        {
            ViewData["Message"] = "Prueba 2";
            return View();
        }
        public int Index3(int a, int b)
        {
            return a + b;
        }
    }
}
