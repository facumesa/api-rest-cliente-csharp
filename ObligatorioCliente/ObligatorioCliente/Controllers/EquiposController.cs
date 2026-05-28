using Excepciones;
using LibreriaWebMVC.Auxiliar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ObligatorioCliente.DTOs;
using System.Text.Json;

namespace Presentacion.Controllers
{
    public class EquiposController : Controller
    {
        public string URLApiEquipos { get; set; }
        public string URLApiCamaras { get; set; }
        public string URLApiTelescopios { get; set; }
        public string URLApiOculares { get; set; }
        public string URLApiMonturas { get; set; }

        public EquiposController(IConfiguration config, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                URLApiEquipos = config.GetValue<string>("UrlApiEquiposDesarrollo");
                URLApiCamaras = config.GetValue<string>("UrlApiCamarasDesarrollo");
                URLApiTelescopios = config.GetValue<string>("UrlApiTelescopiosDesarrollo");
                URLApiOculares = config.GetValue<string>("UrlApiOcularesDesarrollo");
                URLApiMonturas = config.GetValue<string>("UrlApiMonturasDesarrollo");
            }
            else if (env.IsProduction())
            {
                URLApiEquipos = config.GetValue<string>("UrlApiEquiposProduccion");
                URLApiCamaras = config.GetValue<string>("UrlApiCamarasProduccion");
                URLApiTelescopios = config.GetValue<string>("UrlApiTelescopiosProduccion");
                URLApiOculares = config.GetValue<string>("UrlApiOcularesProduccion");
                URLApiMonturas = config.GetValue<string>("UrlApiMonturasProduccion");
            }
        }
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("token") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");
            return View();
        }

        public IActionResult Details(int id)
        {
            if (HttpContext.Session.GetString("token") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");

            EquipoDTO equipo = null;

            try
            {
                string token = HttpContext.Session.GetString("token");

                HttpResponseMessage respuesta = AuxliarClienteHttp.EnviarSolicitud("GET", URLApiEquipos + id, null, token);

                if (respuesta.IsSuccessStatusCode)
                {
                    var tarea2 = respuesta.Content.ReadAsStringAsync();
                    tarea2.Wait();
                    string jsonResponse = tarea2.Result;

                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    equipo = JsonSerializer.Deserialize<EquipoDTO>(jsonResponse, options);

                    if (equipo != null)
                    {
                        if (equipo.TipoEquipo == "Camara")
                            return View("DetailsCamara", JsonSerializer.Deserialize<CamaraDTO>(jsonResponse, options));

                        if (equipo.TipoEquipo == "Telescopio")
                            return View("DetailsTelescopio", JsonSerializer.Deserialize<TelescopioDTO>(jsonResponse, options));

                        if (equipo.TipoEquipo == "Montura")
                            return View("DetailsMontura", JsonSerializer.Deserialize<MonturaDTO>(jsonResponse, options));

                        if (equipo.TipoEquipo == "Ocular")
                            return View("DetailsOcular", JsonSerializer.Deserialize<OcularDTO>(jsonResponse, options));
                    }
                }
                else
                {
                    string mensajeError = AuxliarClienteHttp.ObtenerError(respuesta);
                    ViewBag.Error = $"Error en la API: {mensajeError} (Código: {respuesta.StatusCode})";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Ocurrió un problema al conectar con el servicio: " + ex.Message;
            }

            ViewBag.Error = "No se pudo reconocer el tipo específico de equipo astronómico.";
            return View("Error");
        }

        //public IActionResult Edit(int id)
        //{
        //    if (HttpContext.Session.GetString("nombre") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");

        //    EquipoDTO equipo = CUBuscarEquipo.BuscarEquipo(id);
        //    if (equipo == null) ViewBag.Error = "El equipo con id " + id + " no existe";

        //    if (equipo is CamaraDTO) return View("EditCamara", equipo);
        //    if (equipo is TelescopioDTO) return View("EditTelescopio", equipo);
        //    if (equipo is MonturaDTO) return View("EditMontura", equipo);
        //    if (equipo is OcularDTO) return View("EditOcular", equipo);

        //    return View(equipo);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult EditCamara(CamaraDTO c)
        //{
        //    if (HttpContext.Session.GetString("nombre") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");

        //    try
        //    {
        //        CUEditarCamara.Ejecutar(c);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch (OperacionInvalidaException ex)
        //    {
        //        ViewBag.Error = ex.Message;
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Error = "Ocurrió un error y no fue posible editar el equipo";
        //    }

        //    return View(c);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult EditTelescopio(TelescopioDTO t)
        //{
        //    if (HttpContext.Session.GetString("nombre") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");

        //    try
        //    {
        //        CUEditarTelescopio.Ejecutar(t);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch (OperacionInvalidaException ex)
        //    {
        //        ViewBag.Error = ex.Message;
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Error = "Ocurrió un error y no fue posible editar el equipo";
        //    }

        //    return View(t);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult EditMontura(MonturaDTO m)
        //{
        //    if (HttpContext.Session.GetString("nombre") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");

        //    try
        //    {
        //        CUEditarMontura.Ejecutar(m);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch (OperacionInvalidaException ex)
        //    {
        //        ViewBag.Error = ex.Message;
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Error = "Ocurrió un error y no fue posible editar el equipo";
        //    }

        //    return View(m);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult EditOcular(OcularDTO o)
        //{
        //    if (HttpContext.Session.GetString("nombre") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");

        //    try
        //    {
        //        CUEditarOcular.Ejecutar(o);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch (OperacionInvalidaException ex)
        //    {
        //        ViewBag.Error = ex.Message;
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Error = "Ocurrió un error y no fue posible editar el equipo";
        //    }

        //    return View(o);
        //}

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("token") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");
            string token = HttpContext.Session.GetString("token");
            List<EquipoDTO> eq = new List<EquipoDTO>();
            var respuesta = AuxliarClienteHttp.EnviarSolicitud("GET", URLApiEquipos, null, token);

            if (respuesta.IsSuccessStatusCode)
            {
                var tarea2 = respuesta.Content.ReadFromJsonAsync<List<EquipoDTO>>();
                tarea2.Wait();
                eq = tarea2.Result;
            }
            else
            {
                ViewBag.Error = AuxliarClienteHttp.ObtenerError(respuesta);
            }

            return View(eq);
        }

        //public IActionResult CrearCamara()
        //{
        //    if (HttpContext.Session.GetString("nombre") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");

        //    return View();
        //}

        //[HttpPost]
        //public IActionResult CrearCamara(CamaraDTO dto)
        //{
        //    if (HttpContext.Session.GetString("nombre") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");

        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            CUAltaCamara.Ejecutar(dto);
        //            return RedirectToAction(nameof(Index));
        //        }
        //    }
        //    catch (DatosInvalidosException ex)
        //    {
        //        ViewBag.Erorr = ex.Message;
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Error = ex.Message;
        //    }

        //    return View();
        //}

        //public IActionResult CrearTelescopio()
        //{
        //    if (HttpContext.Session.GetString("nombre") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");

        //    return View();
        //}

        //[HttpPost]
        //public IActionResult CrearTelescopio(TelescopioDTO dto)
        //{
        //    if (HttpContext.Session.GetString("nombre") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");

        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            CUAltaTelescopio.Ejecutar(dto);
        //            return RedirectToAction(nameof(Index));
        //        }
        //    }
        //    catch (DatosInvalidosException ex)
        //    {
        //        ViewBag.Erorr = ex.Message;
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Error = ex.Message;
        //    }

        //    return View();
        //}

        //public IActionResult CrearMontura()
        //{
        //    if (HttpContext.Session.GetString("nombre") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");

        //    return View();
        //}
        //[HttpPost]
        //public IActionResult CrearMontura(MonturaDTO dto)
        //{
        //    if (HttpContext.Session.GetString("nombre") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");

        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            CUAltaMontura.Ejecutar(dto);
        //            return RedirectToAction(nameof(Index));
        //        }
        //    }
        //    catch (DatosInvalidosException ex)
        //    {
        //        ViewBag.Erorr = ex.Message;
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Error = ex.Message;
        //    }

        //    return View();
        //}

        //public IActionResult CrearOcular()
        //{
        //    if (HttpContext.Session.GetString("nombre") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");

        //    return View();
        //}

        //[HttpPost]
        //public IActionResult CrearOcular(OcularDTO dto)
        //{
        //    if (HttpContext.Session.GetString("nombre") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");

        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            CUAltaOcular.Ejecutar(dto);
        //            return RedirectToAction(nameof(Index));
        //        }
        //    }
        //    catch (DatosInvalidosException ex)
        //    {
        //        ViewBag.Erorr = ex.Message;
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Error = ex.Message;
        //    }

        //    return View();
        //}

        //public IActionResult Delete(int id)
        //{
        //    if (HttpContext.Session.GetString("nombre") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");

        //    EquipoDTO equipo = CUBuscarEquipo.BuscarEquipo(id);
        //    if (equipo == null) ViewBag.Error = "El equipo con id " + id + " no existe";
        //    return View(equipo);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Delete(int id, IFormCollection a)
        //{
        //    if (HttpContext.Session.GetString("nombre") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");

        //    try
        //    {
        //        CUBajaEquipo.Ejecutar(id);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch (OperacionInvalidaException ex)
        //    {
        //        EquipoDTO equipo = CUBuscarEquipo.BuscarEquipo(id);
        //        ViewBag.Error = ex.Message;
        //        return View(equipo);
        //    }
        //    catch (EntidadConRelacionException ex)
        //    {
        //        EquipoDTO equipo = CUBuscarEquipo.BuscarEquipo(id);
        //        ViewBag.Error = ex.Message;
        //        return View(equipo);
        //    }
        //    catch (Exception ex)
        //    {
        //        EquipoDTO equipo = CUBuscarEquipo.BuscarEquipo(id);
        //        ViewBag.Error = "No fue posible realizar el borrado: " + ex.Message;
        //        return View(equipo);
        //    }

        //}

    }
}
