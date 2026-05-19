using AccesoDatos.EF;
using Microsoft.EntityFrameworkCore;
using Negocio.Dominio;
using Negocio.InterfacesRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccesoDatos.Repositorios
{
    public class RepositorioPrestamos : IRepositorioPrestamos
    {
        public StellarContext Contexto { get; set; }

        public RepositorioPrestamos(StellarContext contexto)
        {
            Contexto = contexto;
        }
        public void Add(Prestamo nuevo)
        {
            nuevo.Validar();
            Contexto.Add(nuevo);
            Contexto.SaveChanges();
        }

        public bool EquipoEnPrestamo(int id)
        {
            return PrestamosActivos().Any(p => p.TelescopioId == id ||
                                       p.CamaraId == id ||
                                       p.MonturaId == id ||
                                       p.OcularId == id);
        }

        public IEnumerable<Prestamo> FindAll()
        {
            return Contexto.Prestamos
                    .Include(p => p.Socio)
                    .Include(p => p.Telescopio)
                    .Include(p => p.Montura)
                    .Include(p => p.Camara)
                    .Include(p => p.Ocular)
                    .Include(p => p.Coordinador)
                    .ToList();
        }

        public Prestamo FindById(int id)
        {
            return Contexto.Prestamos
                   .Include(p => p.Socio)
                   .FirstOrDefault(p => p.Id == id);
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Prestamo nuevo)
        {
            nuevo.Validar();
            Contexto.Prestamos.Update(nuevo);
            Contexto.SaveChanges();
        }

        public List<Prestamo> PrestamosActivos()
        {
            return Contexto.Prestamos
                            .Where(p => p.Estado == EstadoPrestamo.PRESTADO)
                            .ToList();
        }

        public IEnumerable<Prestamo> ObtenerActivosPorSocio(int socioId)
        {
            return Contexto.Prestamos
                   .Where(p => p.SocioId == socioId && p.Estado == EstadoPrestamo.PRESTADO)
                    .Include(p => p.Socio)
                    .Include(p => p.Telescopio)
                    .Include(p => p.Montura)
                    .Include(p => p.Camara)
                    .Include(p => p.Ocular)
                    .Include(p => p.Coordinador)
                   .ToList();
        }
    }
}
