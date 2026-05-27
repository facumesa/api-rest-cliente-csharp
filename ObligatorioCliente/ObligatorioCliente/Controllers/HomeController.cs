using Microsoft.AspNetCore.Mvc;
using ObligatorioCliente.Models;
using Presentacion.Models;
using System.Diagnostics;
using ObligatorioCliente.DTOs;

namespace ObligatorioCliente.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //if(HttpContext.Session.GetString("nombre") == null) return RedirectToAction("Login", "Usuarios");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
