using Aplicacion.Mappers;
using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Negocio.InterfacesRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.CasosDeUso
{
    public class CUEditarCamara : IEditarCamara
    {
        public IRepositorioEquipos Repo{ get; set; }
        public CUEditarCamara(IRepositorioEquipos repo)
        {
            Repo = repo;
        }

        public void Ejecutar(CamaraDTO c)
        {
            Repo.Update(CamaraMapper.ToCamara(c));
        }
    }
}
