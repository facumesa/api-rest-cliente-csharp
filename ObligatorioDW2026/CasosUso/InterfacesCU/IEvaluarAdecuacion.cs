using CasosUso.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CasosUso.InterfacesCU
{
    public interface IEvaluarAdecuacion
    {
        ObservacionDTO Ejecutar(int prestamoId, int objetoId);
    }
}
