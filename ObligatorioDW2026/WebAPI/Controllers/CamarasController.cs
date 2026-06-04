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
    public class CamarasController : ControllerBase
    {
        public IAltaCamara CUAltaCamara { get; set; }
        public IEditarCamara CUEditarCamara { get; set; }

        public CamarasController(IAltaCamara cuAltaCamara, IEditarCamara cuEditarCamara)
        {
            CUAltaCamara = cuAltaCamara;
            CUEditarCamara = cuEditarCamara;
        }

        /// <summary>
        /// Alta de cámara
        /// </summary>
        /// <remarks>
        /// Permite crear una nueva cámara. Solo los usuarios con rol "Admin" pueden acceder a este endpoint.
        /// </remarks>
        /// <param name="nuevo">Objeto DTO que contiene la información de la nueva cámara.</param>
        /// <returns>La cámara creada.</returns>
        [ProducesResponseType(typeof(CamaraDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        // POST api/<CamarasController>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult CrearCamara([FromBody] CamaraDTO nuevo)
        {
            // if (!UsuarioEsAdmin(HttpContext)) return Unauthorized();
            try
            {
                //if (!ModelState.IsValid) return BadRequest(ModelState);
                if (nuevo == null) return BadRequest("No se proporcionan datos para el alta");
                if (nuevo.Id != 0) return BadRequest("No se debe proporcionar id para el alta");

                CUAltaCamara.Ejecutar(nuevo);

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
        /// Modificación de cámara
        /// </summary>
        /// <remarks>
        /// Permite modificar los datos de una cámara existente. Solo los usuarios con rol "Admin" pueden acceder a este endpoint.
        /// </remarks>
        /// <param name="id">Identificador de la cámara a modificar.</param>
        /// <param name="aModificar">Objeto DTO con los datos actualizados de la cámara.</param>
        /// <returns>La cámara modificada.</returns>
        [ProducesResponseType(typeof(CamaraDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        // PUT api/<CamarasController>/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult Modificar(int? id, [FromBody] CamaraDTO? aModificar)
        {
            try
            {
                if (id == null) return BadRequest("No se proporciona el id del tema a modificar");
                if (aModificar == null) return BadRequest("No se proporcionan datos para la modificación");
                if (aModificar.Id != id) return BadRequest("No coinciden los id del tema");

                CUEditarCamara.Ejecutar(aModificar);
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
