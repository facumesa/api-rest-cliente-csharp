using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObjetosCelestesController : ControllerBase
    {
        public IListarObjetosCelestes CUListar { get; set; }
        public IRankingObjetosCelestes CURanking{ get; set; }

        public ObjetosCelestesController(IListarObjetosCelestes cUListar, IRankingObjetosCelestes cURanking)
        {
            CUListar = cUListar;
            CURanking = cURanking;
        }

        /// <summary>
        /// Listado de objetos celestes
        /// </summary>
        /// <remarks>
        /// Retorna el listado de todos los objetos celestes registrados en el sistema.
        /// </remarks>
        [ProducesResponseType(typeof(IEnumerable<ObjetoCelesteDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        // GET: api/<ObjetosCelestesController>
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var objetos = CUListar.ObtenerListado();
                return Ok(objetos);
            }
            catch
            {
                return StatusCode(500, "Ocurrió un error al obtener los objetos celestes.");
            }
        }

        /// <summary>
        /// Ranking de objetos celestes
        /// </summary>
        /// <remarks>
        /// Retorna el ranking de objetos celestes ordenado por cantidad de observaciones de mayor a menor.
        /// </remarks>
        [ProducesResponseType(typeof(IEnumerable<RankingObjetoDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        // GET: api/<ObjetosCelestesController>/ranking
        [Authorize]
        [HttpGet("ranking")]
        public IActionResult Ranking() 
        {
            try
            {
                var ranking = CURanking.Ejecutar();
                return Ok(ranking);
            }
            catch
            {
                return StatusCode(500, "Ocurrió un error al obtener los objetos celestes.");
            }

        }

    }
}
