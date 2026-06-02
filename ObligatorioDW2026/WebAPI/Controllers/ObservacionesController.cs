using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Excepciones;
using Excepciones.ExcepcionesPropias;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Negocio.Dominio;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ObservacionesController : ControllerBase
    {
        public IAltaObservacion CUAltaObservacion{ get; set; }
        public IEvaluarAdecuacion CUEvaluar { get; set; }

        public ObservacionesController(IAltaObservacion cUAltaObservacion, IEvaluarAdecuacion cUEvaluar)
        {
            CUAltaObservacion = cUAltaObservacion;
            CUEvaluar = cUEvaluar;
        }

        // POST: api/observaciones/evaluar
        [Authorize(Roles = "Socio, Admin")]
        [HttpPost("evaluar")]
        public IActionResult Evaluar([FromBody] ObservacionDTO request)
        {
            try
            {
                if (request == null) return BadRequest("Datos insuficientes para evaluar.");

                var resultadoIA = CUEvaluar.Ejecutar(request.PrestamoId, request.ObjetoCelesteId);

                return Ok(resultadoIA);
            }
            catch (DatosInvalidosException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al conectar con la IA: " + ex.Message);
            }
        }

        // POST: api/observaciones/
        [Authorize(Roles = "Socio, Admin")]
        [HttpPost]
        public IActionResult Guardar([FromBody] ObservacionDTO nuevaObservacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (nuevaObservacion == null) return BadRequest("No se proporcionan datos para la observación");
                if (nuevaObservacion.Id != 0) return BadRequest("No se debe proporcionar id para la observación");

                if (string.IsNullOrEmpty(nuevaObservacion.ResultadoAdecuacion))
                {
                    return BadRequest("Debe evaluar la observación con la IA antes de guardar.");
                }

                CUAltaObservacion.Ejecutar(nuevaObservacion);

                return Created("", "Se ha guardado su observación con éxito.");
            }
            catch (DatosInvalidosException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (OperacionInvalidaException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un problema y no fue posible crear la observación.");
            }
        }
    }
}
