
using AccesoDatos.EF;
using AccesoDatos.Repositorios;
using AccesoDatos.ServiciosExternos;
using Aplicacion.CasosDeUso;
using CasosUso.InterfacesCU;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Negocio.InterfacesRepo;
using Negocio.InterfacesServicios;
using System.Security.Claims;
using System.Text;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var claveSecreta = "Tq16r6NC.+t)I4#~nD/$Thh%1G{M;B123";

            builder.Services.AddAuthentication(aut =>
            {
                aut.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                aut.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(aut =>
            {
                aut.RequireHttpsMetadata = false;
                aut.SaveToken = true;
                aut.TokenValidationParameters = new TokenValidationParameters
                {
                    RoleClaimType = ClaimTypes.Role,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(claveSecreta)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                };
            });


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
            builder.Services.AddScoped<IListarTelescopios, CUListarTelescopios>();
            builder.Services.AddScoped<IListarSociosConTelescopio, CUListarSociosConTelescopio>();
            builder.Services.AddScoped<IRankingObjetosCelestes, CURankingObjetosCelestes>();
            builder.Services.AddScoped<IPrestamosPorCoord, CUPrestamosPorCoord>();
            builder.Services.AddScoped<IListarCoordinadores, CUListarCoordinadores>();
            builder.Services.AddScoped<IBuscarPrestamo, CUBuscarPrestamo>();
            builder.Services.AddScoped<IBuscarAuditoriaPorPrestamo, CUBuscarAuditoriaPorPrestamo>();


            string conBD = builder.Configuration.GetConnectionString("MiConexion");
            builder.Services.AddDbContext<StellarContext>(options =>
                options.UseSqlServer(conBD));

            builder.Services.AddControllers()

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

            .ConfigureApiBehaviorOptions(options =>
             {
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
