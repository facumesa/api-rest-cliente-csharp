using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    public class ObjetosCelestesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
