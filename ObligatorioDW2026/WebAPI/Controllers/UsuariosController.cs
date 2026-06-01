using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Excepciones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using WebAPI.JWT;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        public IListarUsuarios CUListarUsuarios { get; set; }
        public ILoginUsuarios CULogin{ get; set; }

        public UsuariosController(IListarUsuarios cUListarUsuarios, ILoginUsuarios cULogin)
        {
            CUListarUsuarios = cUListarUsuarios;
            CULogin = cULogin;
        }
        // GET: api/<UsuariosController>
        [Authorize(Roles = "Admin")]
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
        [HttpPost] // PARA LOGIN SE PERMITE VERBO POST AUNQUE NO SEA UN ALTA
        public IActionResult Login([FromBody] UsuarioDTO? dto)
        {
            try
            {
                if (dto == null) return BadRequest("No se provee información para el login");

                UsuarioDTO usu = CULogin.Ejecutar(dto.NombreUsuario, dto.Contrasenia);

                if (usu == null) return Unauthorized("Credenciales inválidas");

                string token = ManejadorJWT.GenerarToken(usu);

                return Ok(new { usu.Rol, usu.Id, Token = token }); //PARA LOGIN SE PERMITE STATUS CODE 200 DE ÉXITO
            }
            catch
            {
                return StatusCode(500, "Ocurrió un problema, reintente más tarde");
            }
        }
    }
}
