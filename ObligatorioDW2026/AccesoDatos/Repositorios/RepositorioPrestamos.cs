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

        public Equipo EnPrestamo(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Prestamo> FindAll()
        {
            throw new NotImplementedException();
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
    }
}
