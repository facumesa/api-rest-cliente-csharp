using AccesoDatos.EF;
using Negocio.Dominio;
using Negocio.InterfacesRepo;
using Excepciones.ExcepcionesPropias;
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
            Equipo equipo = FindById(id);
            if (equipo == null) throw new OperacionInvalidaException("No existe el equipo con el id: " + id);

            Contexto.Equipos.Remove(equipo);
            Contexto.SaveChanges();
        }

        public void Update(Equipo nuevo)
        {
            nuevo.Validar();
            Contexto.Equipos.Update(nuevo);
            Contexto.SaveChanges();
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

