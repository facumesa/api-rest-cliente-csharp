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
    public class CUBuscarEquipo : IBuscarEquipo
    {
        public IRepositorioEquipos Repo { get; set; }
        public CUBuscarEquipo(IRepositorioEquipos repo)
        {
            Repo = repo;
        }
        public EquipoDTO BuscarEquipo(int id)
        {
            Equipo equipo = Repo.FindById(id);
            //Preguntar herencia de DTOS
            if (equipo is Camara c) return CamaraMapper.ToDTO(c);
            //if (equipo is Telescopio t) return TelescopioMapper.ToDTO(t);
            //if (equipo is Ocular o) return OcularMapper.ToDTO(o);
            //if (equipo is Montura m) return MonturaMapper.ToDTO(m);

            return EquipoMapper.ToDTO(equipo);
        }
    }
}
