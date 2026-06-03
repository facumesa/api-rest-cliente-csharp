using AccesoDatos.EF;
using Negocio.Dominio;
using Negocio.InterfacesRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccesoDatos.Repositorios
{
    public class RepositorioAuditoria : IRepositorioAuditoria
    {
        public StellarContext Contexto { get; set; }
        public RepositorioAuditoria(StellarContext contexto)
        {
            Contexto = contexto;
        }
        public void Add(Auditoria auditoria)
        {
            Contexto.Add(auditoria);
            Contexto.SaveChanges();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Auditoria nuevo)
        {
            throw new NotImplementedException();
        }

        public Auditoria FindById(int id)
        {
            return Contexto.Auditorias.Find(id);
        }

        public IEnumerable<Auditoria> FindAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Auditoria> AuditoriasPorPrestamo(int id)
        {
            return Contexto.Auditorias
                            .Where(a => a.PrestamoId == id)
                            .ToList();

        }
    }
}
