using AccesoDatos.Migrations;
using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Excepciones;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mono.TextTemplating;
using Negocio.Dominio;
using Presentacion.Models.ViewModels;

namespace Presentacion.Controllers
{
    public class PrestamosController : Controller
    {
        public IAltaPrestamo CUAltaPrestamo { get; set; }
        public IListarSocios CUListarSocios { get; set; }
        public IListarEquipos CUListarEquipos { get; set; }
        public PrestamosController(IAltaPrestamo cUAltaPrestamo, IListarSocios cUListarSocios, IListarEquipos cUListarEquipos)
        {
            CUAltaPrestamo = cUAltaPrestamo;
            CUListarSocios = cUListarSocios;
            CUListarEquipos = cUListarEquipos;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            PrestamoViewModel model = new PrestamoViewModel();
            CargarListasViewModel(model);
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(PrestamoViewModel vm)
        {
            if (HttpContext.Session.GetString("nombre") == null || HttpContext.Session.GetString("rol") != "Coordinador") return RedirectToAction("Login", "Usuarios");
            int? idLogueado = HttpContext.Session.GetInt32("id");
            try
            {
                if (ModelState.IsValid)
                {
                    vm.Prestamo.CoordinadorId = idLogueado.Value;
                    CUAltaPrestamo.Ejecutar(vm.Prestamo);
                    return RedirectToAction("Index");
                }
            }
            catch (DatosInvalidosException ex)
            {
                ViewBag.Error = ex.Message;
                CargarListasViewModel(vm);
                return View(vm);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "No se pudo dar de alta el prestamo";
                CargarListasViewModel(vm);
                return View(vm);
            }

            CargarListasViewModel(vm);
            return View(vm);
        }

        public void CargarListasViewModel(PrestamoViewModel model)
        {
            IEnumerable<EquipoDTO> equipos = CUListarEquipos.ObtenerListado();
            List<TelescopioDTO> listaTelescopios = new List<TelescopioDTO>();
            List<CamaraDTO> listaCamaras = new List<CamaraDTO>();
            List<OcularDTO> listaOculares = new List<OcularDTO>();
            List<MonturaDTO> listaMonturas = new List<MonturaDTO>();

            foreach (EquipoDTO e in equipos)
            {
                if (e.TipoEquipo == "Telescopio")
                {
                    listaTelescopios.Add((TelescopioDTO)e);
                }
                else if (e.TipoEquipo == "Camara")
                {
                    listaCamaras.Add((CamaraDTO)e);

                }
                else if (e.TipoEquipo == "Ocular")
                {
                    listaOculares.Add((OcularDTO)e);
                }
                else if (e.TipoEquipo == "Montura")
                {
                    listaMonturas.Add((MonturaDTO)e);
                }
            }

            model.Socios = CUListarSocios.ObtenerListado();
            model.Telescopios = listaTelescopios;
            model.Camaras = listaCamaras;
            model.Oculares = listaOculares;
            model.Monturas = listaMonturas;

        }
    }
}
