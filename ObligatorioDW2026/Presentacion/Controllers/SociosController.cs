using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Excepciones;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    public class SociosController : Controller
    {
        public IAltaSocio CUAltaSocio { get; set; }
        public IListarSocios CUListarSocios { get; set; }

        public SociosController(IAltaSocio cuAltaSocio, IListarSocios cuListarSocios)
        {
            CUAltaSocio = cuAltaSocio;
            CUListarSocios = cuListarSocios;
        }
        public IActionResult Index()
        {
            return View(CUListarSocios.ObtenerListado());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(SocioDTO dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CUAltaSocio.Ejecutar(dto);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DatosInvalidosException ex)
            {
                ViewBag.Erorr = ex.Message;
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View();
        }
    }
}
