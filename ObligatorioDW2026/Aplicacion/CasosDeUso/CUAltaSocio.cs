using System;
using System.Collections.Generic;
using System.Text;
using Negocio.Dominio;
using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Negocio.InterfacesRepo;
using Aplicacion.Mappers;

namespace Aplicacion.CasosDeUso
{
    public class CUAltaSocio : IAltaSocio
    {
        public IRepositorioUsuarios Repo { get; set; }

        public CUAltaSocio(IRepositorioUsuarios repo)
        {
            Repo = repo;
        }
        public void Ejecutar(SocioDTO nuevo)
        {
            Repo.Add(SocioMapper.ToSocio(nuevo));
        }
    }
}
