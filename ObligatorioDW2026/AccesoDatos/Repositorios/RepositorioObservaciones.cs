using AccesoDatos.EF;
using Negocio.Dominio;
using Negocio.InterfacesRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccesoDatos.Repositorios
{
    public class RepositorioObservaciones : IRepositorioObservaciones
    {
        public StellarContext Contexto { get; set; }

        public RepositorioEquipos(StellarContext contexto)
        {
            Contexto = contexto;
        }
        public void Add(Observacion nuevo)
        {
            nuevo.Validar();
            Contexto.Observacion.Add(nuevo);
            Contexto.SaveChanges();
        }

        public IEnumerable<Observacion> FindAll()
        {
            Contexto.Observaciones.ToList();
        }

        public Observacion FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Observacion nuevo)
        {
            throw new NotImplementedException();
        }
    }
}
