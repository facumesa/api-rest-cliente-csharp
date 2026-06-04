using Aplicacion.CasosDeUso;
using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Excepciones;
using Excepciones.ExcepcionesPropias;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonturasController : ControllerBase
    {
        public IAltaMontura CUAltaMontura { get; set; }
        public IEditarMontura CUEditarMontura { get; set; }

        public MonturasController(IAltaMontura cUAltaMontura, IEditarMontura cUEditarMontura)
        {
            CUAltaMontura = cUAltaMontura;
            CUEditarMontura = cUEditarMontura;
        }

        /// <summary>
        /// Alta de montura
        /// </summary>
        /// <remarks>
        /// Permite crear una nueva montura.
        /// </remarks>
        /// <param name="nuevo">Objeto DTO que contiene la información de la nueva montura.</param>
        [ProducesResponseType(typeof(MonturaDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        // POST api/<MonturasController>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult CrearMontura([FromBody] MonturaDTO nuevo)
        {
            // if (!UsuarioEsAdmin(HttpContext)) return Unauthorized();
            try
            {
                //if (!ModelState.IsValid) return BadRequest(ModelState);
                if (nuevo == null) return BadRequest("No se proporcionan datos para el alta");
                if (nuevo.Id != 0) return BadRequest("No se debe proporcionar id para el alta");

                CUAltaMontura.Ejecutar(nuevo);

                return CreatedAtRoute("ObtenerEquipoPorId", new { id = nuevo.Id }, nuevo);
            }
            catch (DatosInvalidosException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un problema y no se pudo realizar el alta.");
            }
        }

        /// <summary>
        /// Modificación de montura
        /// </summary>
        /// <remarks>
        /// Permite modificar los datos de una montura existente.
        /// </remarks>
        /// <param name="id">Identificador de la montura a modificar.</param>
        /// <param name="aModificar">Objeto DTO con los datos actualizados de la montura.</param>
        [ProducesResponseType(typeof(MonturaDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        // PUT api/<MonturasController>/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult Modificar(int? id, [FromBody] MonturaDTO? aModificar)
        {
            try
            {
                if (id == null) return BadRequest("No se proporciona el id de la montura a modificar");
                if (aModificar == null) return BadRequest("No se proporcionan datos para la modificación");
                if (aModificar.Id != id) return BadRequest("No coinciden los id de la montura");

                CUEditarMontura.Ejecutar(aModificar);
                return Ok(aModificar);
            }
            catch (DatosInvalidosException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (OperacionInvalidaException ex)
            {
                return NotFound(ex.Message);
            }
            catch
            {
                return StatusCode(500, "Ocurrió un problema y no se pudo realizar la modificación.");
            }
        }

    }
}
