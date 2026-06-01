using Excepciones;
using LibreriaWebMVC.Auxiliar;
using Microsoft.AspNetCore.Mvc;
using ObligatorioCliente.DTOs;

namespace Presentacion.Controllers
{
    public class UsuariosController : Controller
    {
        public string URLApiUsuarios { get; set; }
        public string URLApiSocios { get; set; }
        public string URLApiAdministradores { get; set; }
        public string URLApiCoordinadores { get; set; }

        public UsuariosController(IConfiguration config, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                URLApiUsuarios = config.GetValue<string>("UrlApiUsuariosDesarrollo");
                URLApiSocios = config.GetValue<string>("URLApiSociosDesarrollo");
                URLApiAdministradores = config.GetValue<string>("URLApiAdministradoresDesarrollo");
                URLApiCoordinadores = config.GetValue<string>("URLApiCoordinadoresDesarrollo");
            }
            else if (env.IsProduction())
            {
                URLApiUsuarios = config.GetValue<string>("URLApiUsuariosProduccion");
            }
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("token") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");
            string token = HttpContext.Session.GetString("token");
            List<UsuarioDTO> usus = new List<UsuarioDTO>();
            var respuesta = AuxliarClienteHttp.EnviarSolicitud("GET", URLApiUsuarios, null, token);

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
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("token") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");
            return View();
        }

        public IActionResult CrearSocio()
        {
            if (HttpContext.Session.GetString("token") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");
            return View();
        }
        [HttpPost]
        public IActionResult CrearSocio(SocioDTO dto)
        {
            if (ModelState.IsValid) 
            {
                try
                {
                    string token = HttpContext.Session.GetString("token");
                    var respuesta = AuxliarClienteHttp.EnviarSolicitud("POST", URLApiSocios, dto, token);

                    if (respuesta.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else 
                    {
                        ViewBag.Error = AuxliarClienteHttp.ObtenerError(respuesta);
                    }

                }
                catch (Exception ex) { 
                
                    ViewBag.Error = "Ocurrió un problema inesperado";
                }
            }

            return View(dto);
        }

        public IActionResult CrearCoordinador()
        {
            if (HttpContext.Session.GetString("token") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");
            return View();
        }

        [HttpPost]
        public IActionResult CrearCoordinador(CoordinadorDTO dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string token = HttpContext.Session.GetString("token");
                    var respuesta = AuxliarClienteHttp.EnviarSolicitud("POST", URLApiCoordinadores, dto, token);

                    if (respuesta.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.Error = AuxliarClienteHttp.ObtenerError(respuesta);
                    }

                }
                catch (Exception ex)
                {

                    ViewBag.Error = "Ocurrió un problema inesperado";
                }
            }

            return View(dto);
        }

        public IActionResult CrearAdministrador()
        {
            if (HttpContext.Session.GetString("token") == null || HttpContext.Session.GetString("rol") != "Admin") return RedirectToAction("Login", "Usuarios");
            return View();
        }

        [HttpPost]
        public IActionResult CrearAdministrador(AdministradorDTO dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string token = HttpContext.Session.GetString("token");
                    var respuesta = AuxliarClienteHttp.EnviarSolicitud("POST", URLApiAdministradores, dto, token);

                    if (respuesta.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.Error = AuxliarClienteHttp.ObtenerError(respuesta);
                    }

                }
                catch (Exception ex)
                {

                    ViewBag.Error = "Ocurrió un problema inesperado";
                }
            }

            return View(dto);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UsuarioDTO dto)
        {
            try
            {
                var respuesta = AuxliarClienteHttp.EnviarSolicitud("POST", URLApiUsuarios, dto);

                if (respuesta.IsSuccessStatusCode)
                {
                    var tarea2 = respuesta.Content.ReadFromJsonAsync<UsuarioDTO>();
                    tarea2.Wait();

                    UsuarioDTO usu = tarea2.Result;
                    HttpContext.Session.SetString("rol", usu.Rol);
                    HttpContext.Session.SetString("token", usu.Token);
                    HttpContext.Session.SetInt32("id", usu.Id);
                    return RedirectToAction("Index", "Home");
                }
                else 
                {
                    ViewBag.Error = AuxliarClienteHttp.ObtenerError(respuesta);
                }
            }
            catch
            {
                ViewBag.Error = "Ocurrió un error. Intente de nuevo más tarde";
            }
            return View(dto);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
