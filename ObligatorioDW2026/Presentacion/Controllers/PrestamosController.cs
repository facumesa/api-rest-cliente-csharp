using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    public class PrestamosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
