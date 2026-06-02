using AccesoDatos.EF;
using Microsoft.EntityFrameworkCore;
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

        public RepositorioObservaciones(StellarContext contexto)
        {
            Contexto = contexto;
        }
        public void Add(Observacion nuevo)
        {
            nuevo.Validar();
            Contexto.Observaciones.Add(nuevo);
            Contexto.SaveChanges();
        }

        public IEnumerable<Observacion> FindAll()
        {
            return Contexto.Observaciones.ToList();
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

        public bool ExisteObservacionDuplicada(Observacion obs)
        {
            if (obs == null) return false;

            DateTime fechaFiltro = obs.FechaObservacion.Date;
            int? objetoCelesteId = obs.ObjetoCelesteId;
            int? prestamoId = obs.PrestamoId;

            return Contexto.Observaciones.Any(o =>
                o.FechaObservacion.Date == fechaFiltro &&
                o.ObjetoCelesteId == objetoCelesteId &&
                o.PrestamoId == prestamoId
            );
        }

        public IEnumerable<ObjetoCeleste> RankingObjetosMasObservados()
        {
            throw new NotImplementedException();
        }
    }
}
