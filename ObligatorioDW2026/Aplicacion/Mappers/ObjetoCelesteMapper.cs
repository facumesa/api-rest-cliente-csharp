using CasosUso.DTOs;
using Negocio.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Mappers
{
    internal class ObjetoCelesteMapper
    {
        public static ObjetoCelesteDTO ToDTO(ObjetoCeleste objeto)
        {
            if (objeto == null) return null;

            return new ObjetoCelesteDTO
            {
                Id = objeto.Id,
                Nombre = objeto.Nombre,
                Tipo = objeto.Tipo,
                MagnitudAparente = objeto.MagnitudAparente
            };
        }

        public static IEnumerable<ObjetoCelesteDTO> ToListDTO(IEnumerable<ObjetoCeleste> objetos)
        {
            List<ObjetoCelesteDTO> dtos = new List<ObjetoCelesteDTO>();

            if (objetos != null)
            {
                foreach (ObjetoCeleste o in objetos)
                {
                    dtos.Add(ToDTO(o));
                }
            }

            return dtos;
        }
    }
}
