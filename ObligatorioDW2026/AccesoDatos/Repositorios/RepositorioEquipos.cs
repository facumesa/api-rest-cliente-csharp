using AccesoDatos.EF;
using Negocio.Dominio;
using Negocio.InterfacesRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccesoDatos.Repositorios
{
    public class RepositorioEquipos : IRepositorioEquipos
    {
        public StellarContext Contexto { get; set; }

        public RepositorioEquipos(StellarContext contexto)
        {
            Contexto = contexto;
        }

        public void Add(Equipo nuevo)
        {
            nuevo.Validar();
            Contexto.Equipos.Add(nuevo);
            Contexto.SaveChanges();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Equipo nuevo)
        {
            throw new NotImplementedException();
        }

        public Equipo FindById(int id)
        {

            return Contexto.Equipos.Find(id); ;

        }

        public IEnumerable<Equipo> FindAll()
        {
            return Contexto.Equipos.ToList();
        }
    }
}
