using Aplicacion.Mappers;
using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Negocio.Dominio;
using Negocio.InterfacesRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.CasosDeUso
{
    public class CUListarEquipos : IListarEquipos
    {
        public IRepositorioEquipos Repo { get; set; }

        public CUListarEquipos(IRepositorioEquipos repo)
        {
            Repo = repo;
        }

        public IEnumerable<EquipoDTO> ObtenerListado()
        {
            IEnumerable<Equipo> equipos = Repo.FindAll();
            IEnumerable<EquipoDTO> dtos = EquipoMapper.ToListDTO(equipos);
            return dtos;
        }
    }
}
