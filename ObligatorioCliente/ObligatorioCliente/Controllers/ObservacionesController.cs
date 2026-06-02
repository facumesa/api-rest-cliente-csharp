using Excepciones;
using LibreriaWebMVC.Auxiliar;
using Microsoft.AspNetCore.Mvc;
using ObligatorioCliente.DTOs;
using ObligatorioCliente.Models.ViewModels;
using Presentacion.Models.ViewModels;
using System.Text.Json;

namespace ObligatorioCliente.Controllers
{
    public class ObservacionesController : Controller
    {
        public string URLApiObservaciones { get; set; }
        public string URLApiPrestamos { get; set; }
        public string URLApiObjetos { get; set; }

        public ObservacionesController(IConfiguration config, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                URLApiPrestamos = config.GetValue<string>("UrlApiPrestamosDesarrollo");
                URLApiObservaciones = config.GetValue<string>("UrlApiObservacionesDesarrollo");
                URLApiObjetos = config.GetValue<string>("UrlApiObjetosDesarrollo");

            }
            else if (env.IsProduction())
            {
                URLApiPrestamos = config.GetValue<string>("UrlApiPrestamosProduccion");
                URLApiObservaciones = config.GetValue<string>("UrlApiObservacionesProduccion");
                URLApiObjetos = config.GetValue<string>("UrlApiObjetosProduccion");

            }
        }

        public IActionResult Create()
        {
            ObservacionesViewModel model = new ObservacionesViewModel();
            CargarListas(model);
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(ObservacionesViewModel vm, string accion)
        {
            string token = HttpContext.Session.GetString("token");

            if (accion == "guardar")
            {
                try
                {
                    if (string.IsNullOrEmpty(vm.Observacion.ResultadoAdecuacion))
                    {
                        throw new Exception("Debe evaluar la observación con la IA antes de guardar.");
                    }

                    HttpResponseMessage respuesta = AuxliarClienteHttp.EnviarSolicitud("POST", URLApiObservaciones, vm.Observacion, token);

                    if (respuesta.IsSuccessStatusCode)
                    {
                        ViewBag.Exito = "Se ha guardado su observación";
                    }
                    else
                    {
                        ViewBag.Error = AuxliarClienteHttp.ObtenerError(respuesta);
                        ViewBag.IndicadorIA = vm.Observacion.ResultadoAdecuacion;
                        ViewBag.MotivoIA = vm.Observacion.MotivoAdecuacion;
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                    ViewBag.IndicadorIA = vm.Observacion.ResultadoAdecuacion;
                    ViewBag.MotivoIA = vm.Observacion.MotivoAdecuacion;
                }
            }
            else if (accion == "evaluar")
            {
                try
                {
                    ObservacionDTO aEvaluar = new ObservacionDTO
                    {
                        PrestamoId = vm.Observacion.PrestamoId,
                        ObjetoCelesteId = vm.Observacion.ObjetoCelesteId
                    };

                    HttpResponseMessage respuesta2 = AuxliarClienteHttp.EnviarSolicitud("POST", $"{URLApiObservaciones}evaluar", aEvaluar, token);

                    if (respuesta2.IsSuccessStatusCode)
                    {
                        var tarea2 = respuesta2.Content.ReadAsStringAsync();
                        tarea2.Wait();
                        string jsonResponse = tarea2.Result;

                        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                        ObservacionDTO resultadoIA = JsonSerializer.Deserialize<ObservacionDTO>(jsonResponse, options);

                        if (resultadoIA != null)
                        {
                            vm.Observacion.ResultadoAdecuacion = resultadoIA.ResultadoAdecuacion;
                            vm.Observacion.MotivoAdecuacion = resultadoIA.MotivoAdecuacion;
                        }

                        ViewBag.IndicadorIA = vm.Observacion.ResultadoAdecuacion;
                        ViewBag.MotivoIA = vm.Observacion.MotivoAdecuacion;
                    }
                    else
                    {
                        ViewBag.Error = AuxliarClienteHttp.ObtenerError(respuesta2);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                }
            }

            CargarListas(vm);
            return View(vm);
        }

        public void CargarListas(ObservacionesViewModel model)
        {
            int? idLogueado = HttpContext.Session.GetInt32("id");
            string token = HttpContext.Session.GetString("token");
            IEnumerable<PrestamoListadoDTO> prestamos = new List<PrestamoListadoDTO>();
            IEnumerable<ObjetoCelesteDTO> objetos = new List<ObjetoCelesteDTO>();

            HttpResponseMessage respuesta = AuxliarClienteHttp.EnviarSolicitud("GET", $"{URLApiPrestamos}socios-con-prestamo-vigente/{idLogueado}", null, token);

            if (respuesta.IsSuccessStatusCode)
            {
                var tarea2 = respuesta.Content.ReadAsStringAsync();
                tarea2.Wait();
                string jsonResponse = tarea2.Result;

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                prestamos = JsonSerializer.Deserialize<List<PrestamoListadoDTO>>(jsonResponse, options);
            }

            HttpResponseMessage respuesta2 = AuxliarClienteHttp.EnviarSolicitud("GET", URLApiObjetos, null, token);

            if (respuesta2.IsSuccessStatusCode)
            {
                var tarea2 = respuesta2.Content.ReadAsStringAsync();
                tarea2.Wait();
                string jsonResponse = tarea2.Result;

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                objetos = JsonSerializer.Deserialize<List<ObjetoCelesteDTO>>(jsonResponse, options);

            }

            model.Prestamos = prestamos;
            model.ObjetosCelestes = objetos;

        }


    }
}
