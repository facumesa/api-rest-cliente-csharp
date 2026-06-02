using System;
using System.Collections.Generic;
using System.Text;
using Negocio.Dominio;

namespace Negocio.InterfacesRepo
{
    public interface IRepositorioUsuarios : IRepositorio<Usuario>
    {
        Usuario Login(string nombreUsuario, string password);

        IEnumerable<Socio> GetSocios();
        IEnumerable<Socio> ObtenerSociosConPrestamosActivos();
        IEnumerable<Socio> SociosConTelecopioDado(int idTelescopio);

    }
}
