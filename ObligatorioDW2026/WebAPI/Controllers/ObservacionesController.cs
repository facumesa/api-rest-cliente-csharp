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

        /// <summary>
        /// Evaluación de adecuación de observación
        /// </summary>
        /// <remarks>
        /// Evalúa mediante IA si el equipo asociado al préstamo es adecuado para observar o fotografiar el objeto celeste seleccionado.
        /// </remarks>
        /// <param name="request">Objeto DTO que contiene el préstamo y el objeto celeste a evaluar.</param>
        [ProducesResponseType(typeof(ObservacionDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Alta de observación
        /// </summary>
        /// <remarks>
        /// Permite registrar una nueva observación luego de haber evaluado la adecuación del equipo mediante IA.
        /// </remarks>
        /// <param name="nuevaObservacion">Objeto DTO que contiene la información de la nueva observación.</param>
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
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
