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
    public class AdministradoresController : ControllerBase
    {
        public IAltaAdministrador CUAltaAdmin { get; set; }

        public AdministradoresController(IAltaAdministrador cUAltaAdmin)
        {
            CUAltaAdmin = cUAltaAdmin;
        }

        /// <summary>
        /// Alta de administrador
        /// </summary>
        /// <remarks>Permite crear un nuevo administrador. Solo los usuarios con rol "Admin" pueden acceder a este endpoint.</remarks>
        /// <param name="nuevo">Objeto DTO que contiene la información del nuevo administrador.</param>
        [ProducesResponseType(typeof(AdministradorDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        // POST api/<AdministradoresController>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult CrearAdmin([FromBody] AdministradorDTO nuevo)
        {
            // if (!UsuarioEsAdmin(HttpContext)) return Unauthorized();
            try
            {
                //if (!ModelState.IsValid) return BadRequest(ModelState);
                if (nuevo == null) return BadRequest("No se proporcionan datos para el alta");
                if (nuevo.Id != 0) return BadRequest("No se debe proporcionar id para el alta");

                CUAltaAdmin.Ejecutar(nuevo);

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
