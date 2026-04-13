using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    public class UsuariosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
