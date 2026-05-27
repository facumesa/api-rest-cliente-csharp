using Microsoft.AspNetCore.Mvc;
using ObligatorioCliente.DTOs;

namespace ObligatorioCliente.Controllers
{
    public class ObjetosCelestesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
