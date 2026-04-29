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
    public class CUAltaCoordinador : IAltaCoordinador
    {
        public IRepositorioUsuarios Repo { get; set; }

        public CUAltaCoordinador(IRepositorioUsuarios repo)
        {
            Repo = repo;
        }
        public void Ejecutar(CoordinadorDTO nuevo)
        {
            Repo.Add(CoordinadorMapper.ToCoordinador(nuevo));
        }
    }
}
