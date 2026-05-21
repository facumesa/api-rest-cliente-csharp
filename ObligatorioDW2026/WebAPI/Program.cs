
using AccesoDatos.EF;
using AccesoDatos.Repositorios;
using Aplicacion.CasosDeUso;
using CasosUso.InterfacesCU;
using Microsoft.EntityFrameworkCore;
using Negocio.InterfacesRepo;

namespace WebAPI
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

            string conBD = builder.Configuration.GetConnectionString("MiConexion");
            builder.Services.AddDbContext<StellarContext>(options =>
                options.UseSqlServer(conBD));

            builder.Services.AddControllers()
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            .ConfigureApiBehaviorOptions(options =>
             {
                 // ¡ESTA LÍNEA ES LA MAGIA! 
                 // Le dice a la API: "No generes respuestas automáticas de error 400 por los campos,
                 // dejá que la petición entre al controlador que yo lo manejo con mis excepciones."
                 options.SuppressModelStateInvalidFilter = true;
             });

            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
