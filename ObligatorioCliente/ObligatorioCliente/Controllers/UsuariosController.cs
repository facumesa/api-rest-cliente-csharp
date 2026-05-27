using Excepciones;
using LibreriaWebMVC.Auxiliar;
using Microsoft.AspNetCore.Mvc;
using ObligatorioCliente.DTOs;

namespace Presentacion.Controllers
{
    public class UsuariosController : Controller
    {
        public string URLApiUsuarios { get; set; }

        public UsuariosController(IConfiguration config, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                URLApiUsuarios = config.GetValue<string>("UrlApiUsuariosDesarrollo");
            }
            else if (env.IsProduction())
            {
                URLApiUsuarios = config.GetValue<string>("URLApiUsuariosProduccion");
            }
        }

        public IActionResult Index()
        {
            List<UsuarioDTO> usus = new List<UsuarioDTO>();
            var respuesta = AuxliarClienteHttp.EnviarSolicitud("GET", URLApiUsuarios);

            if (respuesta.IsSuccessStatusCode)
            {
                var tarea2 = respuesta.Content.ReadFromJsonAsync<List<UsuarioDTO>>();
                tarea2.Wait();
                usus = tarea2.Result;
            }
            else
            {
                ViewBag.Error = AuxliarClienteHttp.ObtenerError(respuesta);
            }

            return View(usus);
        }
        //public IActionResult Create()
        //{
        //    if (HttpContext.Session.GetString("nombre") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");
        //    return View();
        //}

        //public IActionResult CrearSocio()
        //{
        //    if (HttpContext.Session.GetString("nombre") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult CrearSocio(SocioDTO dto)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            CUAltaSocio.Ejecutar(dto);
        //            return RedirectToAction(nameof(Index));
        //        }
        //    }
        //    catch (DatosInvalidosException ex)
        //    {
        //        ViewBag.Error = ex.Message;
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Error = ex.Message;
        //    }

        //    return View();
        //}
        //public IActionResult CrearCoordinador()
        //{
        //    if (HttpContext.Session.GetString("nombre") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult CrearCoordinador(CoordinadorDTO dto)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            CUAltaCoord.Ejecutar(dto);
        //            return RedirectToAction(nameof(Index));
        //        }
        //    }
        //    catch (DatosInvalidosException ex)
        //    {
        //        ViewBag.Error = ex.Message;
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Error = ex.Message;
        //    }

        //    return View();
        //}

        //public IActionResult CrearAdministrador()
        //{
        //    if (HttpContext.Session.GetString("nombre") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult CrearAdministrador(AdministradorDTO dto)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            CUAltaAdmin.Ejecutar(dto);
        //            return RedirectToAction(nameof(Index));
        //        }
        //    }
        //    catch (DatosInvalidosException ex)
        //    {
        //        ViewBag.Error = ex.Message;
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Error = ex.Message;
        //    }

        //    return View();
        //}

        //public IActionResult Login()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult Login(UsuarioDTO dto)
        //{
        //    UsuarioDTO usuario = CULogin.Ejecutar(dto.NombreUsuario, dto.Contrasenia);
        //    if (usuario == null)
        //    {
        //        ViewBag.Error = "El email o la contraseña no son correctos";
        //    }
        //    else
        //    {
        //        HttpContext.Session.SetString("rol", usuario.Rol);
        //        HttpContext.Session.SetString("nombre", usuario.NombreUsuario);
        //        HttpContext.Session.SetInt32("id", usuario.Id);
        //        return RedirectToAction("Index", "Home");
        //    }
        //    return View();
        //}

        //public IActionResult Logout()
        //{
        //    HttpContext.Session.Clear();
        //    return RedirectToAction("Index", "Home");
        //}
    }
}
