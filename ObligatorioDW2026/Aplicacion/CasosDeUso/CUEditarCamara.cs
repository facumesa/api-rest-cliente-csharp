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
    public class CUEditarCamara : IEditarCamara
    {
        public IRepositorioEquipos Repo{ get; set; }
        public CUEditarCamara(IRepositorioEquipos repo)
        {
            Repo = repo;
        }

        public void Ejecutar(CamaraDTO c)
        {
            Camara cam = CamaraMapper.ToCamara(c);
            Repo.Update(cam);
            c.TipoEquipo = "Camara";
        }
    }
}
