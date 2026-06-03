using CasosUso.DTOs;
using Negocio.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Mappers
{
    public static class RankingObjetoMapper
    {
        public static IEnumerable<RankingObjetoDTO> ToListDTO(IEnumerable<(ObjetoCeleste Objeto, int Cantidad)> item)
        {
            return item.Select(x => new RankingObjetoDTO
            {
                Nombre = x.Objeto.Nombre,
                Tipo = x.Objeto.Tipo,
                Cantidad = x.Cantidad
            });
        }
    }
}
