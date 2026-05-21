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
    public class CUAltaOcular : IAltaOcular
    {
        public IRepositorioEquipos Repo{ get; set; }
        public CUAltaOcular(IRepositorioEquipos repo)
        {
            Repo = repo;
        }

        public void Ejecutar(OcularDTO nuevo)
        {
            Ocular ocu = OcularMapper.ToOcular(nuevo);
            Repo.Add(ocu);
            nuevo.Id = ocu.Id;
            nuevo.TipoEquipo = "Ocular";
        }
    }
}
