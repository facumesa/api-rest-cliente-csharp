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
    }
}
