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
    public class TelescopiosController : ControllerBase
    {
        public IAltaTelescopio CUAltaTelescopio { get; set; }
        public IEditarTelescopio CUEditarTelescopio { get; set; }
        public IListarTelescopios CUListarTelescopios{ get; set; }

        public TelescopiosController(IAltaTelescopio cuAltaTelescopio, IEditarTelescopio cuEditarTelescopio, IListarTelescopios cUListarTelescopios)
        {
            CUAltaTelescopio = cuAltaTelescopio;
            CUEditarTelescopio = cuEditarTelescopio;
            CUListarTelescopios = cUListarTelescopios;
        }

        /// <summary>
        /// Alta de telescopio
        /// </summary>
        /// <remarks>
        /// Permite crear un nuevo telescopio.
        /// </remarks>
        /// <param name="nuevo">Objeto DTO que contiene la información del nuevo telescopio.</param>
        [ProducesResponseType(typeof(TelescopioDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        // POST api/<TelescopiosController>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult CrearTelescopio([FromBody] TelescopioDTO nuevo)
        {
            try
            {
                if (nuevo == null) return BadRequest("No se proporcionan datos para el alta");
                if (nuevo.Id != 0) return BadRequest("No se debe proporcionar id para el alta");

                CUAltaTelescopio.Ejecutar(nuevo);

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
        /// Modificación de telescopio
        /// </summary>
        /// <remarks>
        /// Permite modificar los datos de un telescopio existente.
        /// </remarks>
        /// <param name="id">Identificador del telescopio a modificar.</param>
        /// <param name="aModificar">Objeto DTO con los datos actualizados del telescopio.</param>
        [ProducesResponseType(typeof(TelescopioDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        // PUT api/<TelescopiosController>/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult Modificar(int? id, [FromBody] TelescopioDTO? aModificar)
        {
            try
            {
                if (aModificar == null) return BadRequest("No se proporcionan datos para la modificación");
                if (id == null) return BadRequest("No se proporciona el id del telescopio a modificar");
                if (aModificar.Id != id) return BadRequest("No coinciden los id del telescopio");

                CUEditarTelescopio.Ejecutar(aModificar);
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

        /// <summary>
        /// Listado de telescopios
        /// </summary>
        /// <remarks>
        /// Retorna el listado de todos los telescopios registrados en el sistema.
        /// </remarks>
        [ProducesResponseType(typeof(IEnumerable<TelescopioDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        // GET: api/<TelescopiosController>
        [Authorize(Roles = "Admin, Coordinador")]
        [HttpGet]
        public IActionResult TraerTodos()
        {
            try
            {
                IEnumerable<TelescopioDTO> telescopios = CUListarTelescopios.ObtenerListado();
                return Ok(telescopios);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error inesperado");
            }
        }

    }
}
