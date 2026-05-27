using AccesoDatos.EF;
using AccesoDatos.Repositorios;
using AccesoDatos.ServiciosExternos;
using Aplicacion.CasosDeUso;
using CasosUso.InterfacesCU;
using Microsoft.EntityFrameworkCore;
using Negocio.InterfacesRepo;
using Negocio.InterfacesServicios;

namespace Presentacion
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IRepositorioUsuarios, RepositorioUsuarios>();
            builder.Services.AddScoped<IRepositorioEquipos, RepositorioEquipos>();
            builder.Services.AddScoped<IRepositorioPrestamos, RepositorioPrestamos>();
            builder.Services.AddScoped<IRepositorioAuditoria, RepositorioAuditoria>();
            builder.Services.AddScoped<IRepositorioObjetosCelestes, RepositorioObjetosCelestes>();
            builder.Services.AddScoped<IRepositorioObservaciones, RepositorioObservaciones>();

            builder.Services.AddScoped<IAltaSocio, CUAltaSocio>();
            builder.Services.AddScoped<IAltaAdministrador, CUAltaAdministrador>();
            builder.Services.AddScoped<IAltaCoordinador, CUAltaCoordinador>();
            builder.Services.AddScoped<IListarSocios, CUListarSocios>();
            builder.Services.AddScoped<IListarUsuarios, CUListarUsuarios>();
            builder.Services.AddScoped<ILoginUsuarios, CULoginUsuarios>();
            builder.Services.AddScoped<IAltaCamara, CUAltaCamara>();
            builder.Services.AddScoped<IListarEquipos, CUListarEquipos>();
            builder.Services.AddScoped<IListarPrestamos, CUListarPrestamos>();
            builder.Services.AddScoped<IListarSociosConPrestamo, CUListarSociosConPrestamo>();
            builder.Services.AddScoped<IListarPrestamosPorSocio, CUListarPrestamosPorSocio>();
            builder.Services.AddScoped<IDevolucionPrestamo, CUDevolucionPrestamo>();
            builder.Services.AddScoped<IBuscarEquipo, CUBuscarEquipo>();
            builder.Services.AddScoped<IAltaTelescopio, CUAltaTelescopio>();
            builder.Services.AddScoped<IAltaMontura, CUAltaMontura>();
            builder.Services.AddScoped<IAltaOcular, CUAltaOcular>();
            builder.Services.AddScoped<IBajaEquipo, CUBajaEquipo>();
            builder.Services.AddScoped<IEditarCamara, CUEditarCamara>();
            builder.Services.AddScoped<IEditarMontura, CUEditarMontura>();
            builder.Services.AddScoped<IEditarOcular, CUEditarOcular>();
            builder.Services.AddScoped<IEditarTelescopio, CUEditarTelescopio>();
            builder.Services.AddScoped<IAltaPrestamo, CUAltaPrestamo>();
            builder.Services.AddScoped<IListarPrestamosEntreFechas, CUListarPrestamosEntreFechas>();
            builder.Services.AddScoped<IListarPrestamosPorSocioVigentes, CUListarPrestamosPorSocioVigentes>();
            builder.Services.AddScoped<IListarObjetosCelestes, CUListarObjetosCelestes>();
            builder.Services.AddScoped<IAltaObservacion, CUAltaObservacion>();
            builder.Services.AddHttpClient<IServicioGeminiIA, ServicioGeminiIA>();
            builder.Services.AddScoped<IEvaluarAdecuacion, CUEvaluarAdecuacion>();


            string conBD = builder.Configuration.GetConnectionString("MiConexion");
            builder.Services.AddDbContext<StellarContext>(options =>
                options.UseSqlServer(conBD));

            builder.Services.AddSession();

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            app.UseSession();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
