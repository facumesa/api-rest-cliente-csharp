using Aplicacion.CasosDeUso;
using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Excepciones;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Negocio.Dominio;

namespace Presentacion.Controllers
{
    public class EquiposController : Controller
    {
        public IAltaCamara CUAltaCamara { get; set; }
        public IListarEquipos CUListarEquipos { get; set; }
        public IBuscarEquipo CUBuscarEquipo { get; set; }

        public EquiposController(IAltaCamara cuAltaCamara, IListarEquipos cuListarEquipos, IBuscarEquipo cuBuscarEquipo)
        {
            CUAltaCamara = cuAltaCamara;
            CUListarEquipos = cuListarEquipos;
            CUBuscarEquipo = cuBuscarEquipo;
        }

        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Details(int id) 
        {
            EquipoDTO equipo = CUBuscarEquipo.BuscarEquipo(id);
            if (equipo == null) ViewBag.Error = "El equipo con id " + id + " no existe";
            if (equipo is CamaraDTO)
            {
                return View("DetailsCamara", equipo);
            }
            return View(equipo);
        }

        public IActionResult Index()
        {
            return View(CUListarEquipos.ObtenerListado());
        }

        public IActionResult CrearCamara()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CrearCamara(CamaraDTO dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CUAltaCamara.Ejecutar(dto);
                    return RedirectToAction(nameof(Index));
                }          
            }
            catch (DatosInvalidosException ex)
            {
                ViewBag.Erorr = ex.Message;
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View();
        }

    }
}
