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
    public class CUEditarTelescopio : IEditarTelescopio
    {
        public IRepositorioEquipos Repo { get; set; }
        public CUEditarTelescopio(IRepositorioEquipos repo)
        {
            Repo = repo;
        }

        public void Ejecutar(TelescopioDTO t)
        {
            Telescopio tel = TelescopioMapper.ToTelescopio(t);
            Repo.Update(tel);
            t.TipoEquipo = "Telescopio";
        }
    }
}
