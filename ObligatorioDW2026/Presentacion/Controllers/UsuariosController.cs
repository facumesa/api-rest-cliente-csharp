using Aplicacion.CasosDeUso;
using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Excepciones;
using Humanizer;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    public class UsuariosController : Controller
    {
        public ILoginUsuarios CULogin { get; set; }
        public IAltaSocio CUAltaSocio { get; set; }
        public IListarSocios CUListarSocios { get; set; }
        public IAltaAdministrador CUAltaAdmin { get; set; }
        public IAltaCoordinador CUAltaCoord { get; set; }
        public IListarUsuarios CUListarUsuarios{ get; set; }

        public UsuariosController(ILoginUsuarios cuLogin, IAltaSocio cuAltaSocio, IListarSocios cuListarSocios, IAltaAdministrador cUAltaAdmin, IAltaCoordinador cUAltaCoord, IListarUsuarios cUListarUsuarios)
        {
            CULogin = cuLogin;
            CUAltaSocio = cuAltaSocio;
            CUListarSocios = cuListarSocios;
            CUAltaAdmin = cUAltaAdmin;
            CUAltaCoord = cUAltaCoord;
            CUListarUsuarios = cUListarUsuarios;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("nombre") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");
            return View(CUListarUsuarios.ObtenerListado());
        }
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("nombre") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");
            return View();
        }

        public IActionResult CrearSocio()
        {
            if (HttpContext.Session.GetString("nombre") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");
            return View();
        }
        [HttpPost]
        public IActionResult CrearSocio(SocioDTO dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CUAltaSocio.Ejecutar(dto);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DatosInvalidosException ex)
            {
                ViewBag.Error = ex.Message;
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View();
        }
        public IActionResult CrearCoordinador()
        {
            if (HttpContext.Session.GetString("nombre") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");
            return View();
        }
        [HttpPost]
        public IActionResult CrearCoordinador(CoordinadorDTO dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CUAltaCoord.Ejecutar(dto);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DatosInvalidosException ex)
            {
                ViewBag.Error = ex.Message;
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View();
        }

        public IActionResult CrearAdministrador()
        {
            if (HttpContext.Session.GetString("nombre") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");
            return View();
        }
        [HttpPost]
        public IActionResult CrearAdministrador(AdministradorDTO dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CUAltaAdmin.Ejecutar(dto);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DatosInvalidosException ex)
            {
                ViewBag.Error = ex.Message;
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View();
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
                return RedirectToAction("Index", "Home");
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
