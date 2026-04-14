using Negocio.InterfacesRepo;
using AccesoDatos.Repositorios;
using CasosUso.InterfacesCU;
using Aplicacion.CasosDeUso;

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

            builder.Services.AddScoped<IAltaSocio, CUAltaSocio>();
            builder.Services.AddScoped<IListarSocios, CUListarSocios>();


            var app = builder.Build();

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
