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
    public class CUAltaCamara : IAltaCamara
    {
        public IRepositorioEquipos Repo { get; set; }

        public CUAltaCamara(IRepositorioEquipos repo)
        {
            Repo = repo;
        }

        public void Ejecutar(CamaraDTO nuevo)
        {
            Camara cam = CamaraMapper.ToCamara(nuevo);
            Repo.Add(cam);
            nuevo.Id = cam.Id;
            nuevo.TipoEquipo = "Camara";
        }
    }
}
