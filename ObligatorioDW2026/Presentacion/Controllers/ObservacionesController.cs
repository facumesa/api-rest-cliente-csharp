using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    public class ObservacionesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
