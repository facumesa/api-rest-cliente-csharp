using Negocio.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio.InterfacesRepo
{
    public interface IRepositorioAuditoria
    {
        void Add(Auditoria auditoria);
    }
}
