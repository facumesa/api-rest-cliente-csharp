using Aplicacion.Mappers;
using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Negocio.InterfacesRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.CasosDeUso
{
    public class CUListarTelescopios : IListarTelescopios
    {
        public IRepositorioEquipos Repo{ get; set; }
        public CUListarTelescopios(IRepositorioEquipos repo)
        {
            Repo = repo;
        }
        public IEnumerable<TelescopioDTO> ObtenerListado()
        {
            return TelescopioMapper.ToListDTO(Repo.ObtenerTelescopios());

        }
    }
}
