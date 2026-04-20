using CasosUso.DTOs;
using Negocio.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Mappers
{
    internal class EquipoMapper
    {
        public static EquipoDTO ToDTO(Equipo equipo)
        {
            if (equipo == null) return null;

            return new EquipoDTO
            {
                Id = equipo.Id,
                Marca = equipo.Marca,
                Modelo = equipo.Modelo,
                Cantidad = equipo.Cantidad,
                TipoEquipo = equipo.GetType().Name
            };
        }

        public static IEnumerable<EquipoDTO> ToListDTO(IEnumerable<Equipo> equipos)
        {
            List<EquipoDTO> dtos = new List<EquipoDTO>();

            if (equipos != null)
            {
                foreach (Equipo eq in equipos)
                {
                    dtos.Add(ToDTO(eq));
                }
            }

            return dtos;
        }

    }
}
