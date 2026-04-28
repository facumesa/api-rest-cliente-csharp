using CasosUso.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CasosUso.InterfacesCU
{
    public interface IAltaPrestamo
    {
        void Ejecutar(PrestamoDTO nuevo);
    }
}
