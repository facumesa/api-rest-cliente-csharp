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

        IEnumerable<Prestamo> ObtenerActivosPorSocio(int socioId);

        IEnumerable<Prestamo> ObtenerPrestamosPorFechas(int socioId, int mes, int año);

    }
}
