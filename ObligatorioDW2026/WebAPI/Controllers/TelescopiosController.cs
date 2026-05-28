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

        public TelescopiosController(IAltaTelescopio cuAltaTelescopio, IEditarTelescopio cuEditarTelescopio)
        {
            CUAltaTelescopio = cuAltaTelescopio;
            CUEditarTelescopio = cuEditarTelescopio;
        }

        // POST api/<TelescopiosController>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult CrearTelescopio([FromBody] TelescopioDTO nuevo)
        {
            // if (!UsuarioEsAdmin(HttpContext)) return Unauthorized();
            try
            {
                //if (!ModelState.IsValid) return BadRequest(ModelState);
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

        // PUT api/<TelescopiosController>/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult Modificar(int? id, [FromBody] TelescopioDTO? aModificar)
        {
            try
            {
                if (id == null) return BadRequest("No se proporciona el id del tema a modificar");
                if (aModificar == null) return BadRequest("No se proporcionan datos para la modificación");
                if (aModificar.Id != id) return BadRequest("No coinciden los id del tema");

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

    }
}
