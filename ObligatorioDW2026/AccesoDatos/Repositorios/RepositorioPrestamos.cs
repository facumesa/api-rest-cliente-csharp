using AccesoDatos.EF;
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
            List<Prestamo> activos = PrestamosActivos();
            foreach (Prestamo p in activos)
            {
                if (p.TelescopioId == id || p.CamaraId == id || p.MonturaId == id || p.OcularId == id)
                {
                    return true;
                }
            }
            return false;
        }

        public IEnumerable<Prestamo> FindAll()
        {
            return Contexto.Prestamos.ToList();
        }

        public Prestamo FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Prestamo nuevo)
        {
            throw new NotImplementedException();
        }

        public List<Prestamo> PrestamosActivos()
        {
            List<Prestamo> activos = new List<Prestamo>();
            foreach (Prestamo p in Contexto.Prestamos.ToList())
            {
                if (p.Estado == EstadoPrestamo.PRESTADO)
                {
                    activos.Add(p);
                }
            }
            return activos;
        }
    }
}
