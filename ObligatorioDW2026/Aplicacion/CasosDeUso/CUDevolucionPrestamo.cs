using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Excepciones;
using Negocio.Dominio;
using Negocio.InterfacesRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.CasosDeUso
{
    public class CUDevolucionPrestamo : IDevolucionPrestamo
    {
        public IRepositorioPrestamos RepoPrestamos { get; set; }
        public IRepositorioEquipos RepoEquipos { get; set; }
        public IRepositorioAuditoria RepoAuditoria { get; set; }
        public IRepositorioUsuarios RepoUsuarios { get; set; }
        public CUDevolucionPrestamo(IRepositorioPrestamos repoPrestamos, IRepositorioEquipos repoEquipos, IRepositorioAuditoria repoAuditoria, IRepositorioUsuarios repoUsuarios)
        {
            RepoPrestamos = repoPrestamos;
            RepoEquipos = repoEquipos;
            RepoAuditoria = repoAuditoria;
            RepoUsuarios = repoUsuarios;
        }
        public void Ejecutar(int id, int coordId)
        {
            Prestamo p = RepoPrestamos.FindById(id);
            Coordinador coordPrestamo = (Coordinador)RepoUsuarios.FindById(coordId);
            if (p == null) throw new Exception("El préstamo no existe");
            if (p.Estado == EstadoPrestamo.DEVUELTO) throw new Exception("Este préstamo ya fue devuelto anteriormente");
            if (coordPrestamo == null) throw new DatosInvalidosException("El coordinador no existe.");

            p.Estado = EstadoPrestamo.DEVUELTO;
            p.FechaFin = DateTime.Now;

            Telescopio t = (Telescopio)RepoEquipos.FindById(p.TelescopioId);
            Montura m = (Montura)RepoEquipos.FindById(p.MonturaId);

            if (t != null) { t.Cantidad++; RepoEquipos.Update(t); }
            if (m != null) { m.Cantidad++; RepoEquipos.Update(m); }

            if (p.CamaraId.HasValue)
            {
                Camara c = (Camara)RepoEquipos.FindById(p.CamaraId.Value);
                if (c != null) { c.Cantidad++; RepoEquipos.Update(c); }

            }

            if (p.OcularId.HasValue)
            {
                Ocular o = (Ocular)RepoEquipos.FindById(p.OcularId.Value);
                if (o != null) { o.Cantidad++; RepoEquipos.Update(o); }
            }

            RepoPrestamos.Update(p);

            Auditoria log = new Auditoria()
            {
                Fecha = DateTime.Now,
                TipoAccion = "DEVOLUCION PRESTAMO",
                CoordinadorId = coordId,
                PrestamoId = p.Id,
                Detalle = $"Devolución registrada para el préstamo ID {p.Id}. El socio {p.Socio.NombreCompleto} devolvió los equipos"
            };

            RepoAuditoria.Add(log);
        }
    }
}
