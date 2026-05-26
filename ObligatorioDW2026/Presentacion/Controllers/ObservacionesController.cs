using Aplicacion.CasosDeUso;
using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Excepciones;
using Microsoft.AspNetCore.Mvc;
using Negocio.Dominio;
using Negocio.InterfacesRepo;
using Presentacion.Models.ViewModels;

namespace Presentacion.Controllers
{
    public class ObservacionesController : Controller
    {
        public IListarPrestamosPorSocioVigentes CUListar { get; set; }
        public IListarObjetosCelestes CUListarOC { get; set; }
        public IAltaObservacion CUAltaObservacion { get; set; }
        public IEvaluarAdecuacion CUEvaluarAdecuacion { get; set; }
        public ObservacionesController(IListarPrestamosPorSocioVigentes cUListar, IListarObjetosCelestes cUListarOC, IAltaObservacion cUAltaObservacion, IEvaluarAdecuacion cUEvaluarAdecuacion)
        {
            CUListar = cUListar;
            CUListarOC = cUListarOC;
            CUAltaObservacion = cUAltaObservacion;
            CUEvaluarAdecuacion = cUEvaluarAdecuacion;
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
            if (accion == "guardar")
            {
                try
                {
                    if (string.IsNullOrEmpty(vm.Observacion.ResultadoAdecuacion))
                    {
                        throw new Exception("Debe evaluar la observación con la IA antes de guardar.");
                    }
                    CUAltaObservacion.Ejecutar(vm.Observacion);
                }
                catch (DatosInvalidosException ex)
                {
                    ViewBag.Error = ex.Message;
                    ViewBag.IndicadorIA = vm.Observacion.ResultadoAdecuacion;
                    ViewBag.MotivoIA = vm.Observacion.MotivoAdecuacion;

                    CargarListas(vm);
                    return View(vm);
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Ocurrió un problema y no fue posible crear la observación";
                    ViewBag.IndicadorIA = vm.Observacion.ResultadoAdecuacion;
                    ViewBag.MotivoIA = vm.Observacion.MotivoAdecuacion;

                    CargarListas(vm);
                    return View(vm);

                }

            } else if(accion == "evaluar")
            {
                try
                {
                    var resultadoIA = CUEvaluarAdecuacion.Ejecutar(vm.Observacion.PrestamoId, vm.Observacion.ObjetoCelesteId);
                    // Cargamos los datos en el ViewBag para que la vista los dibuje
                    ViewBag.IndicadorIA = resultadoIA.ResultadoAdecuacion; // "IDEAL", "ADECUADO", etc.
                    ViewBag.MotivoIA = resultadoIA.MotivoAdecuacion;

                    // Recargamos los combos para que la pantalla no se rompa
                    CargarListas(vm);
                    return View(vm);
                }
                catch (DatosInvalidosException ex)
                {
                    ViewBag.Error = ex.Message;
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
            IEnumerable<PrestamoListadoDTO> prestamos = CUListar.ObtenerListado(idLogueado.Value);
            IEnumerable<ObjetoCelesteDTO> objetos = CUListarOC.ObtenerListado();
            model.Prestamos = prestamos;
            model.ObjetosCelestes = objetos;
        }


    }
}
