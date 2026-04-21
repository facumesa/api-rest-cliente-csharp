using Aplicacion.Mappers;
using CasosUso.DTOs;
using CasosUso.InterfacesCU;
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
            Repo.Add(MonturaMapper.ToMontura(nuevo));
        }
    }
}
