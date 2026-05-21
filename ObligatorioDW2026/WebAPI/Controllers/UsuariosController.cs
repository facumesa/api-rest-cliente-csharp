using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Excepciones;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        public IListarUsuarios CUListarUsuarios { get; set; }

        public UsuariosController(IListarUsuarios cUListarUsuarios)
        {
            CUListarUsuarios = cUListarUsuarios;
        }
        // GET: api/<UsuariosController>
        [HttpGet]
        public IActionResult TraerTodos()
        {
            try
            {
                IEnumerable<UsuarioDTO> usuarios = CUListarUsuarios.ObtenerListado();
                return Ok(usuarios);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrio un error inesperado");
            }
        }

        //POST DEL LOGIN
    }
}
