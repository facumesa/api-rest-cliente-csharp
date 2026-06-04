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

        /// <summary>
        /// Listado de préstamos
        /// </summary>
        /// <remarks>
        /// Retorna el listado de todos los préstamos registrados en el sistema.
        /// </remarks>
        [ProducesResponseType(typeof(IEnumerable<PrestamoDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        // GET: api/<PrestamosController>
        [Authorize(Roles = "Coordinador, Admin")]
        [HttpGet]
        public IActionResult Get()
        {
            var prestamos = CUListarPrestamos.ObtenerListado();
            return Ok(prestamos);
        }

        /// <summary>
        /// Listado de préstamos vigentes por socio
        /// </summary>
        /// <remarks>
        /// Retorna los préstamos vigentes y no devueltos de un socio determinado.
        /// </remarks>
        /// <param name="id">Identificador del socio.</param>
        [ProducesResponseType(typeof(IEnumerable<PrestamoDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        // GET: api/prestamos/prestamos-vigentes-por-socio/5
        [Authorize]
        [HttpGet("prestamos-vigentes-por-socio/{id}")]
        public IActionResult PrestamosVigentesPorSocio(int id) 
        {
            var prestamos = CUPrestamosVigentes.ObtenerListado(id);
            return Ok(prestamos);
        }

        /// <summary>
        /// Listado de socios con préstamos activos
        /// </summary>
        /// <remarks>
        /// Retorna los socios que tienen préstamos en estado EN PRÉSTAMO.
        /// </remarks>
        [ProducesResponseType(typeof(IEnumerable<SocioDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        // GET: api/prestamos/socios-con-prestamo
        [Authorize(Roles = "Coordinador, Admin")]
        [HttpGet("socios-con-prestamo")]
        public IActionResult GetSociosConPrestamo()
        {
            var socios = CUListarSociosConPrestamo.ObtenerListado();
            return Ok(socios);
        }

        /// <summary>
        /// Listado de préstamos por socio
        /// </summary>
        /// <remarks>
        /// Retorna los préstamos de un socio. Opcionalmente permite filtrar por mes y año usando el parámetro fecha con formato yyyy-MM.
        /// </remarks>
        /// <param name="id">Identificador del socio.</param>
        /// <param name="fecha">Fecha opcional en formato yyyy-MM para filtrar los préstamos.</param>
        [ProducesResponseType(typeof(IEnumerable<PrestamoDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Alta de préstamo
        /// </summary>
        /// <remarks>
        /// Permite registrar un nuevo préstamo de equipos para un socio.
        /// </remarks>
        /// <param name="nuevo">Objeto DTO que contiene la información del nuevo préstamo.</param>
        [ProducesResponseType(typeof(PrestamoDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Devolución de préstamo
        /// </summary>
        /// <remarks>
        /// Registra la devolución de un préstamo y actualiza la disponibilidad de los equipos asociados.
        /// </remarks>
        /// <param name="prestamoId">Identificador del préstamo a devolver.</param>
        /// <param name="coordinadorId">Identificador del coordinador que registra la devolución.</param>
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Listado de préstamos por coordinador
        /// </summary>
        /// <remarks>
        /// Retorna los préstamos asociados a un coordinador determinado.
        /// </remarks>
        /// <param name="id">Identificador del coordinador.</param>
        [ProducesResponseType(typeof(IEnumerable<PrestamoDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        // GET: api/prestamos/coordinador/4
        [Authorize(Roles = "Admin")]
        [HttpGet("coordinador/{id}")]
        public IActionResult PrestamosPorCoord(int id)
        {
            try
            {
                var prestamos = CUPrestamosPorCoord.ObtenerListado(id);
                return Ok(prestamos);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un problema al retornar los préstamos.");
            }

        }

        /// <summary>
        /// Auditorías de préstamo
        /// </summary>
        /// <remarks>
        /// Retorna la información de auditoría asociada a un préstamo determinado.
        /// </remarks>
        /// <param name="id">Identificador del préstamo.</param>
        [ProducesResponseType(typeof(IEnumerable<AuditoriaDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        // GET: api/prestamos/auditoria/7
        [Authorize(Roles = "Admin")]
        [HttpGet("auditoria/{id}")]
        public IActionResult AuditoriasPorPrestamo(int id)
        {
            try
            {
                var auditorias = CUAuditoriasPrestamo.Ejecutar(id);
                return Ok(auditorias);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un problema al retornar las auditorías.");
            }

        }

        /// <summary>
        /// Búsqueda de préstamo por Id
        /// </summary>
        /// <remarks>
        /// Retorna la información de un préstamo específico según su identificador.
        /// </remarks>
        /// <param name="id">Identificador del préstamo.</param>
        [ProducesResponseType(typeof(PrestamoDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        // GET: api/prestamos/7
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public IActionResult GetPrestamo(int id)
        {
            try
            {
                var prestamo = CUBuscarPrestamo.Ejecutar(id);
                if (prestamo == null) return NotFound($"El préstamo con id {id} no existe.");
                return Ok(prestamo);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un problema al retornar el préstamo.");
            }

        }
    }
}
