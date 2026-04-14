using System;
using System.Collections.Generic;
using System.Text;
using Aplicacion.Mappers;
using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Negocio.Dominio;
using Negocio.InterfacesRepo;

namespace Aplicacion.CasosDeUso
{
    public class CUListarSocios : IListarSocios
    {
        public IRepositorioUsuarios Repo { get; set; }
        public CUListarSocios(IRepositorioUsuarios repo)
        {
            Repo = repo;
        }
        public IEnumerable<SocioDTO> ObtenerListado()
        {
            IEnumerable<Socio> socios = Repo.GetSocios();
            IEnumerable<SocioDTO> dtos = SocioMapper.ToListDTO(socios);
            return dtos;
        }
    }
}
