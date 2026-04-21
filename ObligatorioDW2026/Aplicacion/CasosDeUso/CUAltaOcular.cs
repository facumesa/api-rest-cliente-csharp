using Aplicacion.Mappers;
using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Negocio.InterfacesRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.CasosDeUso
{
    public class CUAltaOcular : IAltaOcular
    {
        public IRepositorioEquipos Repo{ get; set; }
        public CUAltaOcular(IRepositorioEquipos repo)
        {
            Repo = repo;
        }

        public void Ejecutar(OcularDTO nuevo)
        {
            Repo.Add(OcularMapper.ToOcular(nuevo));
        }
    }
}
