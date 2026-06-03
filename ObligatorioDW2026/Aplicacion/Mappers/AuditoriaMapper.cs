using CasosUso.DTOs;
using Negocio.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Mappers
{
    public class AuditoriaMapper
    {
        public static AuditoriaDTO ToDTO(Auditoria auditoria)
        {
            if (auditoria == null) return null;

            return new AuditoriaDTO
            {
                Id = auditoria.Id,
                Fecha = auditoria.Fecha,
                TipoAccion = auditoria.TipoAccion,
                Detalle = auditoria.Detalle,
                CoordinadorId = auditoria.CoordinadorId,
                PrestamoId = auditoria.PrestamoId

            };
        }

        public static IEnumerable<AuditoriaDTO> ToListDTO(IEnumerable<Auditoria> auditorias)
        {
            List<AuditoriaDTO> dtos = new List<AuditoriaDTO>();

            if (auditorias != null)
            {
                foreach (Auditoria au in auditorias)
                {
                    dtos.Add(ToDTO(au));
                }
            }
            return dtos;
        }
    }
}
