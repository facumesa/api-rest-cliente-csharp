using Aplicacion.Mappers;
using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Negocio.InterfacesRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.CasosDeUso
{
    public class CUListarSociosConTelescopio : IListarSociosConTelescopio
    {
        public IRepositorioUsuarios Repo{ get; set; }
        public CUListarSociosConTelescopio(IRepositorioUsuarios repo)
        {
            Repo = repo;
        }
        public IEnumerable<SocioDTO> ObtenerListado(int id)
        {
            return SocioMapper.ToListDTO(Repo.SociosConTelecopioDado(id));
        }
    }
}
