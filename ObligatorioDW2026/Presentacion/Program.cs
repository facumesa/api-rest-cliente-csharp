using Negocio.InterfacesRepo;
using AccesoDatos.Repositorios;
using CasosUso.InterfacesCU;
using Aplicacion.CasosDeUso;
using AccesoDatos.EF;

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

            builder.Services.AddScoped<IAltaSocio, CUAltaSocio>();
            builder.Services.AddScoped<IListarSocios, CUListarSocios>();
            builder.Services.AddScoped<ILoginUsuarios, CULoginUsuarios>();
            builder.Services.AddScoped<IAltaCamara, CUAltaCamara>();
            builder.Services.AddScoped<IListarEquipos, CUListarEquipos>();
            builder.Services.AddScoped<IBuscarEquipo, CUBuscarEquipo>();

            builder.Services.AddDbContext<StellarContext>();

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
