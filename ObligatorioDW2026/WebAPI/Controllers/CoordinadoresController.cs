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
    public class CoordinadoresController : ControllerBase
    {
        public IAltaCoordinador CUAltaCoord { get; set; }
        public IListarCoordinadores CUListarCoordinadores { get; set; }

        public CoordinadoresController(IAltaCoordinador cUAltaCoord, IListarCoordinadores cUListarCoordinadores)
        {
            CUAltaCoord = cUAltaCoord;
            CUListarCoordinadores = cUListarCoordinadores;
        }

        // POST api/<CoordinadoresController>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult CrearCoord([FromBody] CoordinadorDTO nuevo)
        {
            // if (!UsuarioEsAdmin(HttpContext)) return Unauthorized();
            try
            {
                //if (!ModelState.IsValid) return BadRequest(ModelState);
                if (nuevo == null) return BadRequest("No se proporcionan datos para el alta");
                if (nuevo.Id != 0) return BadRequest("No se debe proporcionar id para el alta");

                CUAltaCoord.Ejecutar(nuevo);

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

        // GET: api/coordinadores/
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetCoordinadores()
        {
            var coords = CUListarCoordinadores.ObtenerListado();
            return Ok(coords);
        }

    }
}
