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
    public class EquiposController : ControllerBase
    {
        public IBajaEquipo CUBajaEquipo { get; set; }
        public IListarEquipos CUListarEquipos { get; set; }
        public IBuscarEquipo CUBuscarEquipo { get; set; }

        public EquiposController(IListarEquipos cuListarEquipos, IBuscarEquipo cuBuscarEquipo, IBajaEquipo cuBajaEquipo)
        {
            CUListarEquipos = cuListarEquipos;
            CUBuscarEquipo = cuBuscarEquipo;
            CUBajaEquipo = cuBajaEquipo;
        }

        /// <summary>
        /// Listado de equipos
        /// </summary>
        /// <remarks>
        /// Retorna el listado de todos los equipos registrados en el sistema.
        /// </remarks>
        [ProducesResponseType(typeof(IEnumerable<EquipoDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        // GET: api/<EquiposController>
        [Authorize(Roles = "Admin, Coordinador")]
        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            try
            {
                IEnumerable<EquipoDTO> equipos = CUListarEquipos.ObtenerListado();
                return Ok(equipos);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error inesperado");
            }
        }

        /// <summary>
        /// Búsqueda de equipo por Id
        /// </summary>
        /// <remarks>
        /// Retorna la información de un equipo específico según su identificador.
        /// </remarks>
        /// <param name="id">Identificador del equipo a buscar.</param>
        [ProducesResponseType(typeof(EquipoDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        // GET api/<EquiposController>/5
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}", Name = "ObtenerEquipoPorId")]
        public IActionResult Get(int id)
        {
            try
            {
                if (id <= 0) return BadRequest("El id debe ser mayor a cero.");
                EquipoDTO equipo = CUBuscarEquipo.BuscarEquipo(id);
                if (equipo == null)
                {
                    return NotFound($"El equipo con id {id} no existe.");
                }
                return Ok(equipo);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error inesperado al buscar el equipo.");
            }
        }

        /// <summary>
        /// Baja de equipo
        /// </summary>
        /// <remarks>
        /// Permite eliminar un equipo existente. Si el equipo tiene préstamos asociados, no se permite la baja.
        /// </remarks>
        /// <param name="id">Identificador del equipo a eliminar.</param>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        // DELETE api/<EquiposController>/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //if (HttpContext.Session.GetString("nombre") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");
            try
            {
                CUBajaEquipo.Ejecutar(id);
                return NoContent();
            }
            catch (OperacionInvalidaException ex)
            {
                return NotFound(ex.Message);
            }
            catch (EntidadConRelacionException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error, intente de nuevo más tarde");
            }

        }
    }
}
