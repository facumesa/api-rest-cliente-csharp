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

        public ObjetosCelestesController(IListarObjetosCelestes cUListar)
        {
            CUListar = cUListar;
        }

        // GET: api/<ObjetosCelestesController>
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            var objetos = CUListar.ObtenerListado();
            return Ok(objetos);
        }

    }
}
