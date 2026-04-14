using System;
using System.Collections.Generic;
using System.Text;
using CasosUso.DTOs;

namespace CasosUso.InterfacesCU
{
    public interface IListarSocios
    {
        IEnumerable<SocioDTO> ObtenerListado();
    }
}
