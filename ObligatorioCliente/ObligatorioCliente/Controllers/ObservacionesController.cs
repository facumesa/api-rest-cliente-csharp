using Excepciones;
using Microsoft.AspNetCore.Mvc;
using Presentacion.Models.ViewModels;
using ObligatorioCliente.DTOs;

namespace ObligatorioCliente.Controllers
{
    public class ObservacionesController : Controller
    {
        
    //    public IActionResult Create()
    //    {
    //        ObservacionesViewModel model = new ObservacionesViewModel();
    //        CargarListas(model);
    //        return View(model);
    //    }

    //    [HttpPost]
    //    public IActionResult Create(ObservacionesViewModel vm, string accion)
    //    {
    //        if (accion == "guardar")
    //        {
    //            try
    //            {
    //                if (string.IsNullOrEmpty(vm.Observacion.ResultadoAdecuacion))
    //                {
    //                    throw new Exception("Debe evaluar la observación con la IA antes de guardar.");
    //                }
    //                CUAltaObservacion.Ejecutar(vm.Observacion);
    //                ViewBag.Exito = "Se ha guardado su observación";
    //            }
    //            catch (DatosInvalidosException ex)
    //            {
    //                ViewBag.Error = ex.Message;
    //                ViewBag.IndicadorIA = vm.Observacion.ResultadoAdecuacion;
    //                ViewBag.MotivoIA = vm.Observacion.MotivoAdecuacion;

    //                CargarListas(vm);
    //                return View(vm);
    //            }
    //            catch (OperacionInvalidaException ex) 
    //            {
    //                ViewBag.Error = ex.Message;

    //                CargarListas(vm);
    //                return View(vm);
    //            }
    //            catch (Exception ex)
    //            {
    //                ViewBag.Error = "Ocurrió un problema y no fue posible crear la observación";
    //                ViewBag.IndicadorIA = vm.Observacion.ResultadoAdecuacion;
    //                ViewBag.MotivoIA = vm.Observacion.MotivoAdecuacion;

    //                CargarListas(vm);
    //                return View(vm);

    //            }

    //        } else if(accion == "evaluar")
    //        {
    //            try
    //            {
    //                var resultadoIA = CUEvaluarAdecuacion.Ejecutar(vm.Observacion.PrestamoId, vm.Observacion.ObjetoCelesteId);
    //                ViewBag.IndicadorIA = resultadoIA.ResultadoAdecuacion;
    //                ViewBag.MotivoIA = resultadoIA.MotivoAdecuacion;

    //                CargarListas(vm);
    //                return View(vm);
    //            }
    //            catch (DatosInvalidosException ex)
    //            {
    //                ViewBag.Error = ex.Message;
    //            }
    //            catch (Exception ex)
    //            {
    //                ViewBag.Error = ex.Message;
    //            }


    //        }

    //        CargarListas(vm);
    //        return View(vm);
    //    }

    //    public void CargarListas(ObservacionesViewModel model)
    //    {
    //        int? idLogueado = HttpContext.Session.GetInt32("id");
    //        IEnumerable<PrestamoListadoDTO> prestamos = CUListar.ObtenerListado(idLogueado.Value);
    //        IEnumerable<ObjetoCelesteDTO> objetos = CUListarOC.ObtenerListado();
    //        model.Prestamos = prestamos;
    //        model.ObjetosCelestes = objetos;
    //    }


    }
}
