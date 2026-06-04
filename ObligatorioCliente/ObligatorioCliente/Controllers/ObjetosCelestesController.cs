using CasosUso.DTOs;
using ObligatorioCliente.Auxiliar;
using Microsoft.AspNetCore.Mvc;
using ObligatorioCliente.DTOs;

namespace ObligatorioCliente.Controllers
{
    public class ObjetosCelestesController : Controller
    {
        public string URLApiObjetosCelestes { get; set; }
        public ObjetosCelestesController(IConfiguration config, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                URLApiObjetosCelestes = config.GetValue<string>("UrlApiObjetosDesarrollo");

            }
            else if (env.IsProduction())
            {
                URLApiObjetosCelestes = config.GetValue<string>("UrlApiObjetosProduccion");

            }
        }
        public IActionResult Ranking()
        {
            if (HttpContext.Session.GetString("token") == null) return RedirectToAction("Login", "Usuarios");
            string token = HttpContext.Session.GetString("token");
            List<RankingObjetoDTO> ranking = new List<RankingObjetoDTO>();
            var respuesta = AuxliarClienteHttp.EnviarSolicitud("GET", $"{URLApiObjetosCelestes}ranking", null, token);

            if (respuesta.IsSuccessStatusCode)
            {
                var tarea2 = respuesta.Content.ReadFromJsonAsync<List<RankingObjetoDTO>>();
                tarea2.Wait();
                ranking = tarea2.Result;
            }
            else
            {
                ViewBag.Error = AuxliarClienteHttp.ObtenerError(respuesta);
            }

            return View(ranking);
        }
    }
}
