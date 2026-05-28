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
    public class PrestamosController : ControllerBase
    {
        public IAltaPrestamo CUAltaPrestamo { get; set; }
        public IListarPrestamos CUListarPrestamos { get; set; }
        public IListarSociosConPrestamo CUListarSociosConPrestamo { get; set; }
        public IListarPrestamosPorSocio CUListarPrestamosPorSocio { get; set; }
        public IDevolucionPrestamo CUDevolucionPrestamo { get; set; }
        public IListarPrestamosEntreFechas CUListarPrestamosEntreFechas { get; set; }
        public PrestamosController(IAltaPrestamo cUAltaPrestamo, IListarSocios cUListarSocios, IListarEquipos cUListarEquipos, IListarPrestamos cUlistarPrestamos, IListarSociosConPrestamo cUListarSociosConPrestamo, IListarPrestamosPorSocio cUListarPrestamosPorSocio, IDevolucionPrestamo cUDevolucionPrestamo, IListarPrestamosEntreFechas cUListarPrestamosEntreFechas)
        {
            CUAltaPrestamo = cUAltaPrestamo;
            CUListarPrestamos = cUlistarPrestamos;
            CUListarSociosConPrestamo = cUListarSociosConPrestamo;
            CUListarPrestamosPorSocio = cUListarPrestamosPorSocio;
            CUDevolucionPrestamo = cUDevolucionPrestamo;
            CUListarPrestamosEntreFechas = cUListarPrestamosEntreFechas;
        }

        // GET: api/<PrestamosController>
        [Authorize(Roles = "Coordinador, Admin")]
        [HttpGet]
        public IActionResult Get()
        {
            var prestamos = CUListarPrestamos.ObtenerListado();
            return Ok(prestamos);
        }

        // GET: api/prestamos/socios-con-prestamo
        [Authorize(Roles = "Coordinador, Admin")]
        [HttpGet("socios-con-prestamo")]
        public IActionResult GetSociosConPrestamo()
        {
            var socios = CUListarSociosConPrestamo.ObtenerListado();
            return Ok(socios);
        }

        // GET: api/prestamos/socio/5
        // GET: api/prestamos/socio/5?fecha=2026-05
        [Authorize(Roles = "Socio, Admin")]
        [HttpGet("socio/{id}")]
        public IActionResult GetPrestamosSocio(int id, [FromQuery] string? fecha)
        {
            try
            {
                if (!string.IsNullOrEmpty(fecha))
                {
                    string[] partes = fecha.Split('-');
                    int anio = int.Parse(partes[0]);
                    int mes = int.Parse(partes[1]);

                    var filtrados = CUListarPrestamosEntreFechas.ObtenerListado(id, mes, anio);
                    return Ok(filtrados);
                }

                var todosSocio = CUListarPrestamosPorSocio.ObtenerListado(id);
                return Ok(todosSocio);
            }
            catch (Exception)
            {
                return BadRequest(new { mensaje = "Formato de fecha o ID inválido." });
            }
        }

        // POST api/<PrestamosController>
        [Authorize(Roles = "Coordinador, Admin")]
        [HttpPost]
        public IActionResult Create([FromBody] PrestamoDTO nuevo)
        {
            if (nuevo == null) return BadRequest("Datos del préstamo no válidos.");

            try
            {
                if (nuevo == null) return BadRequest("No se proporcionan datos para el préstamo");
                if (nuevo.Id != 0) return BadRequest("No se debe proporcionar id para el préstamo");

                CUAltaPrestamo.Ejecutar(nuevo);
                return Created("api/prestamos", nuevo);
            }
            catch (DatosInvalidosException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (SinStockException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un problema y no fue posible dar de alta el prestamo");
            }
        }

        // POST: api/prestamos/devolver
        [Authorize(Roles = "Coordinador, Admin")]
        [HttpPost("devolver")]
        public IActionResult DevolverPrestamo([FromQuery] int prestamoId, [FromQuery] int coordinadorId)
        {
            try
            {
                CUDevolucionPrestamo.Ejecutar(prestamoId, coordinadorId);
                return Ok(new { mensaje = "Devolución registrada con éxito." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = "No se pudo registrar la devolución.", detalle = ex.Message });
            }
        }

    }
}
