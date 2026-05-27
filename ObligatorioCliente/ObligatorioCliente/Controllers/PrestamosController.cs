using Excepciones;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Presentacion.Models.ViewModels;
using System.Collections;
using ObligatorioCliente.DTOs;

namespace ObligatorioCliente.Controllers
{
    public class PrestamosController : Controller
    {
    //    public IActionResult Index()
    //    {
    //        return View(CUListarPrestamos.ObtenerListado());
    //    }

    //    public IActionResult Create()
    //    {
    //        PrestamoViewModel model = new PrestamoViewModel();
    //        CargarListasViewModel(model);
    //        return View(model);
    //    }

    //    [HttpPost]
    //    public IActionResult Create(PrestamoViewModel vm)
    //    {
    //        if (HttpContext.Session.GetString("nombre") == null || HttpContext.Session.GetString("rol") != "Coordinador") return RedirectToAction("Login", "Usuarios");
    //        int? idLogueado = HttpContext.Session.GetInt32("id");
    //        try
    //        {
    //            vm.Prestamo.CoordinadorId = idLogueado.Value;
    //            CUAltaPrestamo.Ejecutar(vm.Prestamo);
    //            return RedirectToAction("Index");
    //        }
    //        catch (DatosInvalidosException ex)
    //        {
    //            ViewBag.Error = ex.Message;
    //            CargarListasViewModel(vm);
    //            return View(vm);
    //        }
    //        catch (SinStockException ex)
    //        {
    //            ViewBag.Error = ex.Message;
    //            CargarListasViewModel(vm);
    //            return View(vm);
    //        }
    //        catch (Exception ex)
    //        {
    //            ViewBag.Error = "No se pudo dar de alta el prestamo";
    //            CargarListasViewModel(vm);
    //            return View(vm);
    //        }
    //    }

    //    public IActionResult SociosConPrestamo () 
    //    {
    //        return View(CUListarSociosConPrestamo.ObtenerListado());
    //    }

    //    public IActionResult MisPrestamos()
    //    {
    //        int? idLogueado = HttpContext.Session.GetInt32("id");

    //        if (idLogueado == null)
    //        {
    //            return RedirectToAction("Login", "Usuarios");
    //        }

    //        return RedirectToAction("PrestamosSocio", new { id = idLogueado.Value });
    //    }

    //    public IActionResult PrestamosSocio(int id, string? fecha) 
    //    {
    //        int? usuarioId = HttpContext.Session.GetInt32("id");
    //        string? rol = HttpContext.Session.GetString("rol");

    //        if (rol == "Socio")
    //        {
    //            if (usuarioId == null || usuarioId != id)
    //            {
    //                return RedirectToAction("Login", "Usuarios");
    //            }
    //        }

    //        ViewBag.SocioId = id;
    //        ViewBag.EsFiltro = !string.IsNullOrEmpty(fecha);

    //        if (!string.IsNullOrEmpty(fecha))
    //        {
    //            string[] partes = fecha.Split('-');
    //            int anio = int.Parse(partes[0]);
    //            int mes = int.Parse(partes[1]);

    //            return View(CUListarPrestamosEntreFechas.ObtenerListado(id, mes, anio));
    //        }
    //        else
    //        {
    //            return View(CUListarPrestamosPorSocio.ObtenerListado(id));
    //        }

    //    }

    //    [HttpPost]
    //    public IActionResult DevolverPrestamo(int prestamoId, int id) 
    //    {
    //        int? idLogueado = HttpContext.Session.GetInt32("id");
    //        try
    //        {
    //            CUDevolucionPrestamo.Ejecutar(prestamoId, idLogueado.Value);
    //            return RedirectToAction("SociosConPrestamo");
    //        }
    //        catch (Exception ex)
    //        {

    //            TempData["Error"] = "No se pudo devolver el préstamo";
    //            return RedirectToAction("PrestamosSocio", new { id = id });
    //        }
    //    }

    //    public void CargarListasViewModel(PrestamoViewModel model)
    //    {
    //        IEnumerable<EquipoDTO> equipos = CUListarEquipos.ObtenerListado();
    //        List<TelescopioDTO> listaTelescopios = new List<TelescopioDTO>();
    //        List<CamaraDTO> listaCamaras = new List<CamaraDTO>();
    //        List<OcularDTO> listaOculares = new List<OcularDTO>();
    //        List<MonturaDTO> listaMonturas = new List<MonturaDTO>();

    //        foreach (EquipoDTO e in equipos)
    //        {
    //            if (e.TipoEquipo == "Telescopio")
    //            {
    //                listaTelescopios.Add((TelescopioDTO)e);
    //            }
    //            else if (e.TipoEquipo == "Camara")
    //            {
    //                listaCamaras.Add((CamaraDTO)e);

    //            }
    //            else if (e.TipoEquipo == "Ocular")
    //            {
    //                listaOculares.Add((OcularDTO)e);
    //            }
    //            else if (e.TipoEquipo == "Montura")
    //            {
    //                listaMonturas.Add((MonturaDTO)e);
    //            }
    //        }

    //        model.Socios = CUListarSocios.ObtenerListado();
    //        model.Telescopios = listaTelescopios;
    //        model.Camaras = listaCamaras;
    //        model.Oculares = listaOculares;
    //        model.Monturas = listaMonturas;

    //    }
    }
}
