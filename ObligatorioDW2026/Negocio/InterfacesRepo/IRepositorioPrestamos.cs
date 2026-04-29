using Negocio.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio.InterfacesRepo
{
    public interface IRepositorioPrestamos : IRepositorio<Prestamo>
    {
        bool EquipoEnPrestamo(int id);

        List<Prestamo> PrestamosActivos();
    }
}
