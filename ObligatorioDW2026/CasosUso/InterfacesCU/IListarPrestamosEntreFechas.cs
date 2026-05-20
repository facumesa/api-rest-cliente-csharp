using CasosUso.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CasosUso.InterfacesCU
{
    public interface IListarPrestamosEntreFechas
    {
        IEnumerable<PrestamoListadoDTO> ObtenerListado(int id, int mes, int año);
    }
}
