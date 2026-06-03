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

        // GET: api/<ObjetosCelestesController>
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            var objetos = CUListar.ObtenerListado();
            return Ok(objetos);
        }

        // GET: api/<ObjetosCelestesController>/ranking
        [Authorize]
        [HttpGet("ranking")]
        public IActionResult Ranking() 
        {
            var ranking = CURanking.Ejecutar();
            return Ok(ranking);
        }

    }
}
