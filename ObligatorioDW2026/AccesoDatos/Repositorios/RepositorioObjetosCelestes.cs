using AccesoDatos.EF;
using Negocio.Dominio;
using Negocio.InterfacesRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccesoDatos.Repositorios
{
    public class RepositorioObjetosCelestes : IRepositorioObjetosCelestes
    {
        public StellarContext Contexto { get; set; }

        public RepositorioObjetosCelestes(StellarContext contexto)
        {
            Contexto = contexto;
        }

        public void Add(ObjetoCeleste nuevo)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ObjetoCeleste> FindAll()
        {
            return Contexto.ObjetosCelestes.ToList();
        }

        public ObjetoCeleste FindById(int id)
        {
            return Contexto.ObjetosCelestes.Find(id);
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(ObjetoCeleste nuevo)
        {
            throw new NotImplementedException();
        }
    }
}
