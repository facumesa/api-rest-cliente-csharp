using Aplicacion.CasosDeUso;
using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Excepciones;
using Excepciones.ExcepcionesPropias;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Negocio.Dominio;

namespace Presentacion.Controllers
{
    public class EquiposController : Controller
    {
        public IAltaCamara CUAltaCamara { get; set; }
        public IAltaTelescopio CUAltaTelescopio { get; set; }
        public IAltaMontura CUAltaMontura { get; set; }
        public IAltaOcular CUAltaOcular { get; set; }
        public IEditarTelescopio CUEditarTelescopio { get; set; }
        public IEditarCamara CUEditarCamara { get; set; }
        public IEditarMontura CUEditarMontura { get; set; }
        public IEditarOcular CUEditarOcular { get; set; }
        public IBajaEquipo CUBajaEquipo { get; set; }
        public IListarEquipos CUListarEquipos { get; set; }
        public IBuscarEquipo CUBuscarEquipo { get; set; }

        public EquiposController(IAltaCamara cuAltaCamara, IListarEquipos cuListarEquipos, IBuscarEquipo cuBuscarEquipo, IAltaTelescopio cuAltaTelescopio, IAltaMontura cuAltaMontura, IAltaOcular cuAltaOcular, IBajaEquipo cuBajaEquipo, IEditarTelescopio cuEditarTelescopio, IEditarCamara cuEditarCamara, IEditarMontura cuEditarMontura, IEditarOcular cuEditarOcular)
        {
            CUAltaCamara = cuAltaCamara;
            CUListarEquipos = cuListarEquipos;
            CUBuscarEquipo = cuBuscarEquipo;
            CUAltaTelescopio = cuAltaTelescopio;
            CUAltaMontura = cuAltaMontura;
            CUAltaOcular = cuAltaOcular;
            CUBajaEquipo = cuBajaEquipo;
            CUEditarTelescopio = cuEditarTelescopio;
            CUEditarCamara = cuEditarCamara;
            CUEditarMontura = cuEditarMontura;
            CUEditarOcular = cuEditarOcular;
        }

        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("nombre") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");
            return View();
        }
        public IActionResult Details(int id)
        {
            if (HttpContext.Session.GetString("nombre") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");

            EquipoDTO equipo = CUBuscarEquipo.BuscarEquipo(id);
            if (equipo == null) ViewBag.Error = "El equipo con id " + id + " no existe";

            if (equipo is CamaraDTO) return View("DetailsCamara", equipo);
            if (equipo is TelescopioDTO) return View("DetailsTelescopio", equipo);
            if (equipo is MonturaDTO) return View("DetailsMontura", equipo);
            if (equipo is OcularDTO) return View("DetailsOcular", equipo);

            return View(equipo);
        }

        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("nombre") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");

            EquipoDTO equipo = CUBuscarEquipo.BuscarEquipo(id);
            if (equipo == null) ViewBag.Error = "El equipo con id " + id + " no existe";

            if (equipo is CamaraDTO) return View("EditCamara", equipo);
            if (equipo is TelescopioDTO) return View("EditTelescopio", equipo);
            if (equipo is MonturaDTO) return View("EditMontura", equipo);
            if (equipo is OcularDTO) return View("EditOcular", equipo);

            return View(equipo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCamara(CamaraDTO c)
        {
            if (HttpContext.Session.GetString("nombre") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");

            try
            {
                CUEditarCamara.Ejecutar(c);
                return RedirectToAction(nameof(Index));
            }
            catch (OperacionInvalidaException ex)
            {
                ViewBag.Error = ex.Message;
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Ocurrió un error y no fue posible editar el equipo";
            }

            return View(c);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditTelescopio(TelescopioDTO t)
        {
            if (HttpContext.Session.GetString("nombre") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");

            try
            {
                CUEditarTelescopio.Ejecutar(t);
                return RedirectToAction(nameof(Index));
            }
            catch (OperacionInvalidaException ex)
            {
                ViewBag.Error = ex.Message;
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Ocurrió un error y no fue posible editar el equipo";
            }

            return View(t);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditMontura(MonturaDTO m)
        {
            if (HttpContext.Session.GetString("nombre") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");

            try
            {
                CUEditarMontura.Ejecutar(m);
                return RedirectToAction(nameof(Index));
            }
            catch (OperacionInvalidaException ex)
            {
                ViewBag.Error = ex.Message;
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Ocurrió un error y no fue posible editar el equipo";
            }

            return View(m);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditOcular(OcularDTO o)
        {
            if (HttpContext.Session.GetString("nombre") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");

            try
            {
                CUEditarOcular.Ejecutar(o);
                return RedirectToAction(nameof(Index));
            }
            catch (OperacionInvalidaException ex)
            {
                ViewBag.Error = ex.Message;
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Ocurrió un error y no fue posible editar el equipo";
            }

            return View(o);
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("nombre") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");

            return View(CUListarEquipos.ObtenerListado());
        }

        public IActionResult CrearCamara()
        {
            if (HttpContext.Session.GetString("nombre") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");

            return View();
        }

        [HttpPost]
        public IActionResult CrearCamara(CamaraDTO dto)
        {
            if (HttpContext.Session.GetString("nombre") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");

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

        public IActionResult CrearTelescopio()
        {
            if (HttpContext.Session.GetString("nombre") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");

            return View();
        }

        [HttpPost]
        public IActionResult CrearTelescopio(TelescopioDTO dto)
        {
            if (HttpContext.Session.GetString("nombre") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");

            try
            {
                if (ModelState.IsValid)
                {
                    CUAltaTelescopio.Ejecutar(dto);
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

        public IActionResult CrearMontura()
        {
            if (HttpContext.Session.GetString("nombre") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");

            return View();
        }
        [HttpPost]
        public IActionResult CrearMontura(MonturaDTO dto)
        {
            if (HttpContext.Session.GetString("nombre") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");

            try
            {
                if (ModelState.IsValid)
                {
                    CUAltaMontura.Ejecutar(dto);
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

        public IActionResult CrearOcular()
        {
            if (HttpContext.Session.GetString("nombre") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");

            return View();
        }

        [HttpPost]
        public IActionResult CrearOcular(OcularDTO dto)
        {
            if (HttpContext.Session.GetString("nombre") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");

            try
            {
                if (ModelState.IsValid)
                {
                    CUAltaOcular.Ejecutar(dto);
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

        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("nombre") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");

            EquipoDTO equipo = CUBuscarEquipo.BuscarEquipo(id);
            if (equipo == null) ViewBag.Error = "El equipo con id " + id + " no existe";
            return View(equipo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection a)
        {
            if (HttpContext.Session.GetString("nombre") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");

            try
            {
                CUBajaEquipo.Ejecutar(id);
                return RedirectToAction(nameof(Index));
            }
            catch (OperacionInvalidaException ex)
            {
                EquipoDTO equipo = CUBuscarEquipo.BuscarEquipo(id);
                ViewBag.Error = ex.Message;
                return View(equipo);
            }
            catch (EntidadConRelacionException ex)
            {
                EquipoDTO equipo = CUBuscarEquipo.BuscarEquipo(id);
                ViewBag.Error = ex.Message;
                return View(equipo);
            }
            catch (Exception ex)
            {
                EquipoDTO equipo = CUBuscarEquipo.BuscarEquipo(id);
                ViewBag.Error = "No fue posible realizar el borrado: " + ex.Message;
                return View(equipo);
            }

        }

    }
}
