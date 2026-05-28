using Aplicacion.CasosDeUso;
using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Excepciones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SociosController : ControllerBase
    {
        public IAltaSocio CUAltaSocio { get; set; }
        public IListarSocios CUListarSocios { get; set; }

        public SociosController(IAltaSocio cuAltaSocio, IListarSocios cuListarSocios)
        {
            CUAltaSocio = cuAltaSocio;
            CUListarSocios = cuListarSocios;
        }

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
    }
}
