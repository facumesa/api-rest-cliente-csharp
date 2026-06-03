using Aplicacion.Mappers;
using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Negocio.InterfacesRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.CasosDeUso
{
    public class CUListarCoordinadores : IListarCoordinadores
    {
        public IRepositorioUsuarios Repo{ get; set; }
        public CUListarCoordinadores(IRepositorioUsuarios repo)
        {
            Repo = repo;
        }
        public IEnumerable<CoordinadorDTO> ObtenerListado()
        {
            return CoordinadorMapper.ToListDTO(Repo.GetCoordinadores());
        }
    }
}
