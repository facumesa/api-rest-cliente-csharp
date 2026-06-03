using Aplicacion.Mappers;
using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Negocio.InterfacesRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.CasosDeUso
{
    public class CURankingObjetosCelestes : IRankingObjetosCelestes
    {
        public IRepositorioObservaciones Repo{ get; set; }
        public CURankingObjetosCelestes(IRepositorioObservaciones repo)
        {
            Repo = repo;
        }
        public IEnumerable<RankingObjetoDTO> Ejecutar()
        {
            var ranking = Repo.ObtenerRankingObjetos();

            return RankingObjetoMapper.ToListDTO(ranking);
        }
    }
}
