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
        public IListarPrestamosPorSocioVigentes CUPrestamosVigentes { get; set; }
        public IPrestamosPorCoord CUPrestamosPorCoord { get; set; }
        public IBuscarAuditoriaPorPrestamo CUAuditoriasPrestamo { get; set; }
        public IBuscarPrestamo CUBuscarPrestamo { get; set; }
        public PrestamosController(IAltaPrestamo cUAltaPrestamo, IListarSocios cUListarSocios, IListarEquipos cUListarEquipos, IListarPrestamos cUlistarPrestamos, IListarSociosConPrestamo cUListarSociosConPrestamo, IListarPrestamosPorSocio cUListarPrestamosPorSocio, IDevolucionPrestamo cUDevolucionPrestamo, IListarPrestamosEntreFechas cUListarPrestamosEntreFechas, IListarPrestamosPorSocioVigentes cUPrestamosVigentes, IPrestamosPorCoord cUPrestamosPorCoord, IBuscarAuditoriaPorPrestamo cUAuditoriasPrestamo, IBuscarPrestamo cUBuscarPrestamo)
        {
            CUAltaPrestamo = cUAltaPrestamo;
            CUListarPrestamos = cUlistarPrestamos;
            CUListarSociosConPrestamo = cUListarSociosConPrestamo;
            CUListarPrestamosPorSocio = cUListarPrestamosPorSocio;
            CUDevolucionPrestamo = cUDevolucionPrestamo;
            CUListarPrestamosEntreFechas = cUListarPrestamosEntreFechas;
            CUPrestamosVigentes = cUPrestamosVigentes;
            CUPrestamosPorCoord = cUPrestamosPorCoord;
            CUAuditoriasPrestamo = cUAuditoriasPrestamo;
            CUBuscarPrestamo = cUBuscarPrestamo;
        }

        // GET: api/<PrestamosController>
        [Authorize(Roles = "Coordinador, Admin")]
        [HttpGet]
        public IActionResult Get()
        {
            var prestamos = CUListarPrestamos.ObtenerListado();
            return Ok(prestamos);
        }

        // GET: api/prestamos/socios-con-prestamo-vigente
        [Authorize]
        [HttpGet("socios-con-prestamo-vigente/{id}")]
        public IActionResult GetSociosConPrestamosVigentes(int id) 
        {
            var prestamos = CUPrestamosVigentes.ObtenerListado(id);
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
        [Authorize]
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
                return BadRequest("Formato de fecha o ID inválido.");
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
                return Ok("Devolución registrada con éxito.");
            }
            catch (Exception ex)
            {
                return BadRequest("No se pudo registrar la devolución: " + ex.Message);
            }
        }

        // GET: api/prestamos/coordinador/4
        [Authorize(Roles = "Admin")]
        [HttpGet("coordinador/{id}")]
        public IActionResult PrestamosPorCoord(int id)
        {
            var prestamos = CUPrestamosPorCoord.ObtenerListado(id);
            return Ok(prestamos);
        }

        // GET: api/prestamos/auditoria/7
        [Authorize(Roles = "Admin")]
        [HttpGet("auditoria/{id}")]
        public IActionResult AuditoriasPorPrestamo(int id)
        {
            var auditorias = CUAuditoriasPrestamo.Ejecutar(id);
            return Ok(auditorias);
        }

        // GET: api/prestamos/7
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public IActionResult GetPrestamo(int id)
        {
            var prestamo = CUBuscarPrestamo.Ejecutar(id);
            return Ok(prestamo);
        }
    }
}
