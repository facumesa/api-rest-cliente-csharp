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
            builder.Services.AddScoped<ILoginUsuarios, CULoginUsuarios>();

            builder.Services.AddSession();

            builder.Services.AddDistributedMemoryCache(); // Necesario para que la sesión tenga donde guardarse
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20); // Cuánto tiempo dura el login antes de expirar
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
