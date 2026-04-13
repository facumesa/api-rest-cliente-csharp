using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    public class EquiposController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
