using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Excepciones;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    public class UsuariosController : Controller
    {
        public ILoginUsuarios CULogin { get; set; }

        public UsuariosController(ILoginUsuarios cuLogin)
        {
            CULogin = cuLogin;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UsuarioDTO dto)
        {
            UsuarioDTO usuario = CULogin.Ejecutar(dto.NombreUsuario, dto.Contrasenia);
            if (usuario == null)
            {
                ViewBag.Error = "El email o la contraseña no son correctos";
            }
            else
            {
                HttpContext.Session.SetString("rol", usuario.Rol);
                HttpContext.Session.SetString("nombre", usuario.NombreUsuario);
                HttpContext.Session.SetInt32("id", usuario.Id);
                return RedirectToAction("Index", "Socios");
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
