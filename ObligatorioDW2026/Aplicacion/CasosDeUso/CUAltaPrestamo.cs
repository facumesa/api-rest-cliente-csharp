using Aplicacion.Mappers;
using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Excepciones;
using Excepciones.ExcepcionesPropias;
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
        public IRepositorioAuditoria RepoAuditoria { get; set; }
        public IRepositorioUsuarios RepoUsuarios { get; set; }
        public CUAltaPrestamo(IRepositorioPrestamos repoPrestamos, IRepositorioEquipos repoEquipos, IRepositorioAuditoria repoAuditoria, IRepositorioUsuarios repoUsuarios)
        {
            RepoPrestamos = repoPrestamos;
            RepoEquipos = repoEquipos;
            RepoAuditoria = repoAuditoria;
            RepoUsuarios = repoUsuarios;
        }

        public void Ejecutar(PrestamoDTO nuevo)
        {
            Socio socioPrestamo = (Socio)RepoUsuarios.FindById(nuevo.SocioId);
            if (socioPrestamo == null) throw new DatosInvalidosException("El socio no existe.");
            Coordinador coordPrestamo = (Coordinador)RepoUsuarios.FindById(nuevo.CoordinadorId);
            if (coordPrestamo == null) throw new DatosInvalidosException("El coordinador no existe.");


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

            if (p.Telescopio.Cantidad <= 0)
                throw new SinStockException($"Sin stock del telescopio: {p.Telescopio.Marca}, {p.Telescopio.Modelo}");

            if (p.Montura.Cantidad <= 0)
                throw new SinStockException($"Sin stock de la montura: {p.Montura.Marca}, {p.Montura.Modelo}");

            if (p.Camara != null && p.Camara.Cantidad <= 0)
                throw new SinStockException($"Sin stock de la cámara: {p.Camara.Marca},{p.Camara.Modelo}");

            if (p.Ocular != null && p.Ocular.Cantidad <= 0)
                throw new SinStockException($"Sin stock del ocular: {p.Ocular.Marca},{p.Ocular.Modelo}");

            p.Telescopio.Cantidad--;
            RepoEquipos.Update(p.Telescopio);

            p.Montura.Cantidad--;
            RepoEquipos.Update(p.Montura);

            if (p.Camara != null)
            {
                p.Camara.Cantidad--;
                RepoEquipos.Update(p.Camara);
            }

            if (p.Ocular != null)
            {
                p.Ocular.Cantidad--;
                RepoEquipos.Update(p.Ocular);
            }

            RepoPrestamos.Add(p);

            Auditoria log = new Auditoria
            {
                Fecha = DateTime.Now,
                TipoAccion = "ALTA PRÉSTAMO",
                CoordinadorId = nuevo.CoordinadorId,
                PrestamoId = p.Id,
                Detalle = $"Se registró el préstamo ID {p.Id} para el socio {socioPrestamo.NombreCompleto} | (ID: {p.SocioId})."
            };

            RepoAuditoria.Add(log);
        }
    }
    
}
