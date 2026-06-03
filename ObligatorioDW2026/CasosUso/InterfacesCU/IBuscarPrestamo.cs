using CasosUso.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CasosUso.InterfacesCU
{
    public interface IBuscarPrestamo
    {
        PrestamoListadoDTO Ejecutar(int id);
    }
}
