using Aplicacion.Mappers;
using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Negocio.InterfacesRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.CasosDeUso
{
    public class CUEditarMontura : IEditarMontura
    {
        public IRepositorioEquipos Repo { get; set; }
        public CUEditarMontura(IRepositorioEquipos repo)
        {
            Repo = repo;
        }

        public void Ejecutar(MonturaDTO m)
        {
            Repo.Update(MonturaMapper.ToMontura(m));
        }
    }
}
