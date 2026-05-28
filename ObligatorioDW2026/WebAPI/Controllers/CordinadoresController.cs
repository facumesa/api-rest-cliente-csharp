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
    public class CordinadoresController : ControllerBase
    {
        public IAltaCoordinador CUAltaCoord { get; set; }

        public CordinadoresController(IAltaCoordinador cUAltaCoord)
        {
            CUAltaCoord = cUAltaCoord;
        }

        // POST api/<CordinadoresController>
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

    }
}
