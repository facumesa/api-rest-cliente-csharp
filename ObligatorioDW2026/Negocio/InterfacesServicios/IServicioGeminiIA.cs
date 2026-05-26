using Negocio.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio.InterfacesServicios
{
    public interface IServicioGeminiIA
    {
        ResultadoEvaluacionIA EvaluarAdecuacion(Telescopio telescopio, Montura montura, Camara camaraOpcional, Ocular ocularOpcional, ObjetoCeleste objetoCeleste);
    }
}
