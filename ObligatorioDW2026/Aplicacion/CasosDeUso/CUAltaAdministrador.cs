using Aplicacion.Mappers;
using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Negocio.InterfacesRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.CasosDeUso
{
    public class CUAltaAdministrador : IAltaAdministrador
    {
        public IRepositorioUsuarios Repo { get; set; }

        public CUAltaAdministrador(IRepositorioUsuarios repo)
        {
            Repo = repo;
        }
        public void Ejecutar(AdministradorDTO nuevo)
        {
            Repo.Add(AdministradorMapper.ToAdministrador(nuevo));
        }
    }
}
