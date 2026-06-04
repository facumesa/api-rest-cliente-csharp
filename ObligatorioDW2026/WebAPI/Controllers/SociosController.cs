using Aplicacion.CasosDeUso;
using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Excepciones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Negocio.Dominio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SociosController : ControllerBase
    {
        public IAltaSocio CUAltaSocio { get; set; }
        public IListarSocios CUListarSocios { get; set; }
        public IListarSociosConTelescopio CUListarSociosTelescopio { get; set; }

        public SociosController(IAltaSocio cuAltaSocio, IListarSocios cuListarSocios, IListarSociosConTelescopio cUListarSociosTelescopio)
        {
            CUAltaSocio = cuAltaSocio;
            CUListarSocios = cuListarSocios;
            CUListarSociosTelescopio = cUListarSociosTelescopio;
        }

        /// <summary>
        /// Listado de socios
        /// </summary>
        /// <remarks>
        /// Retorna el listado de todos los socios registrados en el sistema.
        /// </remarks>
        [ProducesResponseType(typeof(IEnumerable<SocioDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        // GET: api/<SociosController>
        [HttpGet]
        public IActionResult TraerTodos()
        {
            try
            {
                IEnumerable<SocioDTO> socios = CUListarSocios.ObtenerListado();
                return Ok(socios);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrio un error inesperado");
            }
        }

        /// <summary>
        /// Alta de socio
        /// </summary>
        /// <remarks>
        /// Permite crear un nuevo socio en el sistema.
        /// </remarks>
        /// <param name="nuevo">Objeto DTO que contiene la información del nuevo socio.</param>
        [ProducesResponseType(typeof(SocioDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        // POST api/<SociosController>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult CrearSocio([FromBody] SocioDTO nuevo)
        {
            try
            {
                if (nuevo == null) return BadRequest("No se proporcionan datos para el alta");
                if (nuevo.Id != 0) return BadRequest("No se debe proporcionar id para el alta");

                CUAltaSocio.Ejecutar(nuevo);

                return StatusCode(201, nuevo);
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
        /// Listado de socios por telescopio
        /// </summary>
        /// <remarks>
        /// Retorna los socios que solicitaron en préstamo un telescopio determinado, sin repetir socios.
        /// </remarks>
        /// <param name="id">Identificador del telescopio.</param>
        [ProducesResponseType(typeof(IEnumerable<SocioDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        // GET api/<SociosController>/contelescopio
        [Authorize(Roles = "Admin, Coordinador")]
        [HttpGet("contelescopio/{id}")]
        public IActionResult SociosPorTelescopio(int id) 
        {
            try
            {
                if (id <= 0) return BadRequest("El id del telescopio debe ser mayor a cero.");
                IEnumerable<SocioDTO> socios = CUListarSociosTelescopio.ObtenerListado(id);

                if(socios == null || !socios.Any())
                return NotFound("No se encontraron socios que hayan solicitado este telescopio.");


                return Ok(socios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se pudo obtener la lista de socios: " + ex.Message);
            }
        }
    }
}
