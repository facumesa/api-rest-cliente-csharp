using Aplicacion.CasosDeUso;
using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Excepciones;
using Excepciones.ExcepcionesPropias;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquiposController : ControllerBase
    {
        public IAltaCamara CUAltaCamara { get; set; }
        public IAltaTelescopio CUAltaTelescopio { get; set; }
        public IAltaMontura CUAltaMontura { get; set; }
        public IAltaOcular CUAltaOcular { get; set; }
        public IEditarTelescopio CUEditarTelescopio { get; set; }
        public IEditarCamara CUEditarCamara { get; set; }
        public IEditarMontura CUEditarMontura { get; set; }
        public IEditarOcular CUEditarOcular { get; set; }
        public IBajaEquipo CUBajaEquipo { get; set; }
        public IListarEquipos CUListarEquipos { get; set; }
        public IBuscarEquipo CUBuscarEquipo { get; set; }

        public EquiposController(IListarEquipos cuListarEquipos, IBuscarEquipo cuBuscarEquipo, IBajaEquipo cuBajaEquipo)
        {
            CUListarEquipos = cuListarEquipos;
            CUBuscarEquipo = cuBuscarEquipo;
            CUBajaEquipo = cuBajaEquipo;
        }

        // GET: api/<EquiposController>
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
                return StatusCode(500, "Ocurrio un error inesperado");
            }
        }

        // GET api/<EquiposController>/5
        [HttpGet("{id}", Name = "ObtenerEquipoPorId")]
        public IActionResult Get(int id)
        {
            try
            {
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

        // DELETE api/<EquiposController>/5
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
