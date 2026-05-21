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
    public class CUAltaTelescopio : IAltaTelescopio
    {
        public IRepositorioEquipos Repo { get; set; }
        public CUAltaTelescopio(IRepositorioEquipos repo)
        {
            Repo = repo;
        }

        public void Ejecutar(TelescopioDTO nuevo)
        {
            Telescopio tel = TelescopioMapper.ToTelescopio(nuevo);
            Repo.Add(tel);
            nuevo.Id = tel.Id;
            nuevo.TipoEquipo = "Telescopio";
        }
    }
}
