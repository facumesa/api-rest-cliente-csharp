using CasosUso.DTOs;
using Excepciones;
using Negocio.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Mappers
{
    internal class ObservacionMapper
    {
        public static ObservacionDTO ToDTO(Observacion objeto)
        {
            if (objeto == null) return null;

            return new ObservacionDTO
            {
                Id = objeto.Id,
                FechaObservacion = objeto.FechaObservacion,
                PrestamoId = objeto.PrestamoId,
                ObjetoCelesteId = objeto.ObjetoCelesteId,
                ResultadoAdecuacion = objeto.ResultadoAdecuacion,
                MotivoAdecuacion = objeto.MotivoAdecuacion

            };
        }

        public static Observacion ToObservacion(ObservacionDTO dto) {

            if (dto == null) throw new DatosInvalidosException("No hay datos de Prestamo");

            Observacion o = new Observacion();

            o.Id = dto.Id;
            o.FechaObservacion = dto.FechaObservacion;
            o.PrestamoId = dto.PrestamoId;
            o.ObjetoCelesteId = dto.ObjetoCelesteId;
            o.ResultadoAdecuacion = dto.ResultadoAdecuacion;
            o.MotivoAdecuacion = dto.MotivoAdecuacion;
           
            return o;
        }
    }
}
