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
    public class CUAltaPrestamo : IAltaPrestamo
    {
        public IRepositorioPrestamos RepoPrestamos{ get; set; }
        public IRepositorioEquipos RepoEquipos { get; set; }
        public CUAltaPrestamo(IRepositorioPrestamos repoPrestamos, IRepositorioEquipos repoEquipos)
        {
            RepoPrestamos = repoPrestamos;
            RepoEquipos = repoEquipos;
        }

        public void Ejecutar(PrestamoDTO nuevo)
        {
            Prestamo p = PrestamoMapper.ToPrestamo(nuevo);

            p.Telescopio = (Telescopio)RepoEquipos.FindById(p.TelescopioId);
            p.Montura = (Montura)RepoEquipos.FindById(p.MonturaId);

            if (p.CamaraId != null)
            {
                p.Camara = (Camara)RepoEquipos.FindById(p.CamaraId.Value);
            }
            if (p.OcularId != null)
            {
                p.Ocular = (Ocular)RepoEquipos.FindById(p.OcularId.Value);
            }

            p.Validar();

            RepoPrestamos.Add(p);
        }
    }
}
