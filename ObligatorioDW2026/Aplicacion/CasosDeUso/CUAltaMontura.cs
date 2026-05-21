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
    public class CUAltaMontura : IAltaMontura
    {
        public IRepositorioEquipos Repo { get; set; }

        public CUAltaMontura(IRepositorioEquipos repo)
        {
            Repo = repo;
        }

        public void Ejecutar(MonturaDTO nuevo)
        {
            Montura mon = MonturaMapper.ToMontura(nuevo);
            Repo.Add(mon);
            nuevo.Id = mon.Id;
            nuevo.TipoEquipo = "Montura";
        }
    }
}
