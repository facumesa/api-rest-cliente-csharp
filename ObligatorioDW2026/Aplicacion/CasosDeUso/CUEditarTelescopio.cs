using Aplicacion.Mappers;
using CasosUso.DTOs;
using CasosUso.InterfacesCU;
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
            Repo.Update(TelescopioMapper.ToTelescopio(t));
        }
    }
}
