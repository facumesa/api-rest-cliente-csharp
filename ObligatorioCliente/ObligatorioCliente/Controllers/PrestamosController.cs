using CasosUso.DTOs;
using Excepciones;
using LibreriaWebMVC.Auxiliar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using ObligatorioCliente.DTOs;
using Presentacion.Models.ViewModels;
using System.Collections;
using System.Reflection;
using System.Text.Json;

namespace ObligatorioCliente.Controllers
{
    public class PrestamosController : Controller
    {
        public string URLApiPrestamos { get; set; }
        public string URLApiEquipos { get; set; }
        public string URLApiSocios { get; set; }
        public string URLApiSociosConPrestamo { get; set; }
        public string URLApiCoordinadores { get; set; }

        public PrestamosController(IConfiguration config, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                URLApiPrestamos = config.GetValue<string>("UrlApiPrestamosDesarrollo");
                URLApiEquipos = config.GetValue<string>("UrlApiEquiposDesarrollo");
                URLApiSocios = config.GetValue<string>("URLApiSociosDesarrollo");
                URLApiSociosConPrestamo = config.GetValue<string>("UrlApiSociosConPrestamoDesarrollo");
                URLApiCoordinadores = config.GetValue<string>("UrlApiCoordinadoresDesarrollo");
            }
            else if (env.IsProduction())
            {
                URLApiPrestamos = config.GetValue<string>("UrlApiPrestamosProduccion");
                URLApiEquipos = config.GetValue<string>("UrlApiEquiposProduccion");
                URLApiSocios = config.GetValue<string>("URLApiSociosProduccion");
                URLApiSociosConPrestamo = config.GetValue<string>("UrlApiSociosConPrestamoProduccion");
                URLApiCoordinadores = config.GetValue<string>("UrlApiCoordinadoresProduccion");

            }
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("token") == null || (HttpContext.Session.GetString("rol") != "Admin" && HttpContext.Session.GetString("rol") != "Coordinador")) return RedirectToAction("Login", "Usuarios");

            string token = HttpContext.Session.GetString("token");
            List<PrestamoListadoDTO> prestamos = new List<PrestamoListadoDTO>();
            var respuesta = AuxliarClienteHttp.EnviarSolicitud("GET", URLApiPrestamos, null, token);

            if (respuesta.IsSuccessStatusCode)
            {
                var tarea2 = respuesta.Content.ReadFromJsonAsync<List<PrestamoListadoDTO>>();
                tarea2.Wait();
                prestamos = tarea2.Result;
            }
            else
            {
                ViewBag.Error = AuxliarClienteHttp.ObtenerError(respuesta);
            }

            return View(prestamos);
        }

        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("token") == null || (HttpContext.Session.GetString("rol") != "Admin" && HttpContext.Session.GetString("rol") != "Coordinador")) return RedirectToAction("Login", "Usuarios");

            PrestamoViewModel model = new PrestamoViewModel();
            if (model.Prestamo == null)
            {
                model.Prestamo = new PrestamoDTO();
            }
            model.Prestamo.FechaInicio = DateTime.Today;
            model.Prestamo.FechaFin = DateTime.Today;   
            CargarListasViewModel(model);
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(PrestamoViewModel vm)
        {
            if (HttpContext.Session.GetString("token") == null || (HttpContext.Session.GetString("rol") != "Admin" && HttpContext.Session.GetString("rol") != "Coordinador")) return RedirectToAction("Login", "Usuarios");
            int? idLogueado = HttpContext.Session.GetInt32("id");

            if (ModelState.IsValid)
            {
                try
                {
                    string token = HttpContext.Session.GetString("token");
                    vm.Prestamo.CoordinadorId = idLogueado.Value;
                    var respuesta = AuxliarClienteHttp.EnviarSolicitud("POST", URLApiPrestamos, vm.Prestamo, token);


                    if (respuesta.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.Error = AuxliarClienteHttp.ObtenerError(respuesta);
                        CargarListasViewModel(vm);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Ocurrió un problema inesperado";
                    CargarListasViewModel(vm);
                }

            }
            CargarListasViewModel(vm);
            return View(vm);

        }

        public IActionResult SociosConPrestamo()
        {
            if (HttpContext.Session.GetString("token") == null || (HttpContext.Session.GetString("rol") != "Admin" && HttpContext.Session.GetString("rol") != "Coordinador")) return RedirectToAction("Login", "Usuarios");

            string token = HttpContext.Session.GetString("token");
            List<SocioDTO> socios = new List<SocioDTO>();
            var respuesta = AuxliarClienteHttp.EnviarSolicitud("GET", URLApiSociosConPrestamo, null, token);

            if (respuesta.IsSuccessStatusCode)
            {
                var tarea2 = respuesta.Content.ReadFromJsonAsync<List<SocioDTO>>();
                tarea2.Wait();
                socios = tarea2.Result;
            }
            else
            {
                ViewBag.Error = AuxliarClienteHttp.ObtenerError(respuesta);
            }

            return View(socios);
        }

        public IActionResult MisPrestamos()
        {
            int? idLogueado = HttpContext.Session.GetInt32("id");

            if (idLogueado == null)
            {
                return RedirectToAction("Login", "Usuarios");
            }

            return RedirectToAction("PrestamosSocio", new { id = idLogueado.Value });
        }

        public IActionResult PrestamosSocio(int id, string? fecha)
        {
            int? usuarioId = HttpContext.Session.GetInt32("id");
            string? rol = HttpContext.Session.GetString("rol");
            string? token = HttpContext.Session.GetString("token");

            if (rol == "Socio")
            {
                if (usuarioId == null || usuarioId != id)
                {
                    return RedirectToAction("Login", "Usuarios");
                }
            }

            ViewBag.SocioId = id;
            ViewBag.EsFiltro = !string.IsNullOrEmpty(fecha);

            string url = "";
            if (!string.IsNullOrEmpty(fecha))
            {
                string[] partes = fecha.Split('-');
                int anio = int.Parse(partes[0]);
                int mes = int.Parse(partes[1]);

                url = $"{URLApiPrestamos}socio/{id}?fecha={fecha}";
            }
            else
            {
                url = $"{URLApiPrestamos}socio/{id}";

            }

            HttpResponseMessage respuesta = AuxliarClienteHttp.EnviarSolicitud("GET", url, null, token);
            IEnumerable<PrestamoListadoDTO> prestamos = new List<PrestamoListadoDTO>();

            if (respuesta.IsSuccessStatusCode)
            {
                var tarea2 = respuesta.Content.ReadFromJsonAsync<List<PrestamoListadoDTO>>();
                tarea2.Wait();
                prestamos = tarea2.Result;
            }
            else
            {
                ViewBag.Error = AuxliarClienteHttp.ObtenerError(respuesta);
            }

            return View(prestamos);


        }

        [HttpPost]
        public IActionResult DevolverPrestamo(int prestamoId, int id)
        {
            if (HttpContext.Session.GetString("token") == null || (HttpContext.Session.GetString("rol") != "Admin" && HttpContext.Session.GetString("rol") != "Coordinador")) return RedirectToAction("Login", "Usuarios");
            int? idLogueado = HttpContext.Session.GetInt32("id");
            string? token = HttpContext.Session.GetString("token");

            try
            {
                HttpResponseMessage response = AuxliarClienteHttp.EnviarSolicitud("POST", $"{URLApiPrestamos}devolver?prestamoId={prestamoId}&coordinadorId={idLogueado.Value}", new { }, token);

                if (response.IsSuccessStatusCode)
                {
                    string mensajeExito = response.Content.ReadAsStringAsync().Result;
                    TempData["Exito"] = mensajeExito;
                    return RedirectToAction("SociosConPrestamo");   
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                         response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return RedirectToAction("Login", "Usuarios");
                }
                else
                {
                    TempData["Error"] = AuxliarClienteHttp.ObtenerError(response);

                    return RedirectToAction("PrestamosSocio", new { id = id });
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Ocurrió un error de comunicación con el servicio.";
                return RedirectToAction("PrestamosSocio", new { id = id });
            }
        }

        public IActionResult SeleccionarCoordinador()
        {
            if (HttpContext.Session.GetString("token") == null || (HttpContext.Session.GetString("rol") != "Admin")) return RedirectToAction("Login", "Usuarios");
            string token = HttpContext.Session.GetString("token");
            List<CoordinadorDTO> coords = new List<CoordinadorDTO>();
            var respuesta = AuxliarClienteHttp.EnviarSolicitud("GET", URLApiCoordinadores, null, token);

            if (respuesta.IsSuccessStatusCode)
            {
                var tarea2 = respuesta.Content.ReadFromJsonAsync<List<CoordinadorDTO>>();
                tarea2.Wait();
                coords = tarea2.Result;
            }
            else
            {
                ViewBag.Error = AuxliarClienteHttp.ObtenerError(respuesta);
            }

            return View(coords);
        }

        public IActionResult Coordinador(int id)
        {
            if (HttpContext.Session.GetString("token") == null || (HttpContext.Session.GetString("rol") != "Admin")) return RedirectToAction("Login", "Usuarios");
            string token = HttpContext.Session.GetString("token");
            List<PrestamoDTO> prestamos = new List<PrestamoDTO>();
            var respuesta = AuxliarClienteHttp.EnviarSolicitud("GET", $"{URLApiPrestamos}coordinador/{id}", null, token);

            if (respuesta.IsSuccessStatusCode)
            {
                var tarea2 = respuesta.Content.ReadFromJsonAsync<List<PrestamoDTO>>();
                tarea2.Wait();
                prestamos = tarea2.Result;
            }
            else
            {
                ViewBag.Error = AuxliarClienteHttp.ObtenerError(respuesta);
            }

            return View(prestamos);
        }

        public IActionResult Auditoria(int id)
        {
            if (HttpContext.Session.GetString("token") == null || (HttpContext.Session.GetString("rol") != "Admin")) return RedirectToAction("Login", "Usuarios");
            string token = HttpContext.Session.GetString("token");
            List<AuditoriaDTO> auditorias = new List<AuditoriaDTO>();
            var respuesta = AuxliarClienteHttp.EnviarSolicitud("GET", $"{URLApiPrestamos}auditoria/{id}", null, token);

            if (respuesta.IsSuccessStatusCode)
            {
                var tarea2 = respuesta.Content.ReadFromJsonAsync<List<AuditoriaDTO>>();
                tarea2.Wait();
                auditorias = tarea2.Result;
            }
            else
            {
                ViewBag.Error = AuxliarClienteHttp.ObtenerError(respuesta);
            }

            ViewBag.Id = id;
            return View(auditorias);
        }

        public IActionResult Detalle(int id)
        {
            if (HttpContext.Session.GetString("token") == null || (HttpContext.Session.GetString("rol") != "Admin")) return RedirectToAction("Login", "Usuarios");
            string token = HttpContext.Session.GetString("token");
            PrestamoListadoDTO prestamo = new PrestamoListadoDTO();
            var respuesta = AuxliarClienteHttp.EnviarSolicitud("GET", $"{URLApiPrestamos}{id}", null, token);

            if (respuesta.IsSuccessStatusCode)
            {
                var tarea2 = respuesta.Content.ReadFromJsonAsync<PrestamoListadoDTO>();
                tarea2.Wait();
                prestamo = tarea2.Result;
            }
            else
            {
                ViewBag.Error = AuxliarClienteHttp.ObtenerError(respuesta);
            }

            ViewBag.Id = id;
            return View(prestamo);
        }

        public void CargarListasViewModel(PrestamoViewModel model)
        {
            IEnumerable<EquipoDTO> equipos = new List<EquipoDTO>();
            IEnumerable<SocioDTO> socios = new List<SocioDTO>();
            List<TelescopioDTO> listaTelescopios = new List<TelescopioDTO>();
            List<CamaraDTO> listaCamaras = new List<CamaraDTO>();
            List<OcularDTO> listaOculares = new List<OcularDTO>();
            List<MonturaDTO> listaMonturas = new List<MonturaDTO>();

            string token = HttpContext.Session.GetString("token");

            HttpResponseMessage respuesta = AuxliarClienteHttp.EnviarSolicitud("GET", URLApiEquipos, null, token);

            if (respuesta.IsSuccessStatusCode)
            {
                var tarea2 = respuesta.Content.ReadAsStringAsync();
                tarea2.Wait();
                string jsonResponse = tarea2.Result;

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                equipos = JsonSerializer.Deserialize<List<EquipoDTO>>(jsonResponse, options);

                foreach (EquipoDTO e in equipos)
                {
                    string objetoIndividualJson = JsonSerializer.Serialize(e, options);

                    if (e.TipoEquipo == "Telescopio")
                    {
                        var tele = JsonSerializer.Deserialize<TelescopioDTO>(objetoIndividualJson, options);
                        if (tele != null) listaTelescopios.Add(tele);
                    }
                    else if (e.TipoEquipo == "Camara")
                    {
                        var cam = JsonSerializer.Deserialize<CamaraDTO>(objetoIndividualJson, options);
                        if (cam != null) listaCamaras.Add(cam);

                    }
                    else if (e.TipoEquipo == "Ocular")
                    {
                        var ocu = JsonSerializer.Deserialize<OcularDTO>(objetoIndividualJson, options);
                        if (ocu != null) listaOculares.Add(ocu);
                    }
                    else if (e.TipoEquipo == "Montura")
                    {
                        var mon = JsonSerializer.Deserialize<MonturaDTO>(objetoIndividualJson, options);
                        if (mon != null) listaMonturas.Add(mon);
                    }
                }

            }

            HttpResponseMessage respuesta2 = AuxliarClienteHttp.EnviarSolicitud("GET", URLApiSocios, null, token);

            if (respuesta2.IsSuccessStatusCode)
            {
                var tarea2 = respuesta2.Content.ReadAsStringAsync();
                tarea2.Wait();
                string jsonResponse = tarea2.Result;

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                socios = JsonSerializer.Deserialize<List<SocioDTO>>(jsonResponse, options);

            }

            model.Telescopios = listaTelescopios;
            model.Camaras = listaCamaras;
            model.Oculares = listaOculares;
            model.Monturas = listaMonturas;
            model.Socios = socios;


        }
    }
}
