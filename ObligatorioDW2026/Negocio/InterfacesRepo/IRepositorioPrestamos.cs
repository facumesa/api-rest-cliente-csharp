using Negocio.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio.InterfacesRepo
{
    public interface IRepositorioPrestamos : IRepositorio<Prestamo>
    {
        Equipo EnPrestamo(int id);
    }
}
