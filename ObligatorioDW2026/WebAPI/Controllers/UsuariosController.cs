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

        /// <summary>
        /// Listado de usuarios
        /// </summary>
        /// <remarks>
        /// Retorna el listado de todos los usuarios registrados en el sistema.
        /// </remarks>
        [ProducesResponseType(typeof(IEnumerable<UsuarioDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
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
                return StatusCode(500, "Ocurrió un error inesperado");
            }
        }

        /// <summary>
        /// Inicio de sesión
        /// </summary>
        /// <remarks>
        /// Valida las credenciales del usuario y retorna un token JWT para acceder a los endpoints protegidos.
        /// </remarks>
        /// <param name="dto">Objeto DTO con nombre de usuario y contraseña.</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        //POST DEL LOGIN
        [HttpPost]
        public IActionResult Login([FromBody] UsuarioDTO? dto)
        {
            try
            {
                if (dto == null) return BadRequest("No se provee información para el login");

                UsuarioDTO usu = CULogin.Ejecutar(dto.NombreUsuario, dto.Contrasenia);

                if (usu == null) return Unauthorized("Credenciales inválidas");

                string token = ManejadorJWT.GenerarToken(usu);

                return Ok(new { usu.Rol, usu.Id, Token = token });
            }
            catch
            {
                return StatusCode(500, "Ocurrió un problema, reintente más tarde");
            }
        }
    }
}
