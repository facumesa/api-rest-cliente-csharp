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
    public class CUEditarOcular : IEditarOcular
    {
        public IRepositorioEquipos Repo { get; set; }
        public CUEditarOcular(IRepositorioEquipos repo)
        {
            Repo = repo;
        }

        public void Ejecutar(OcularDTO o)
        {
            Ocular ocu = OcularMapper.ToOcular(o);
            Repo.Update(ocu);
            o.TipoEquipo = "Ocular";
        }
    }
}
