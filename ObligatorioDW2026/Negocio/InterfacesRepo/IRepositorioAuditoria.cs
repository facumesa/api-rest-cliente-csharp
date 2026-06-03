using Negocio.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio.InterfacesRepo
{
    public interface IRepositorioAuditoria : IRepositorio<Auditoria>
    {
        IEnumerable<Auditoria> AuditoriasPorPrestamo(int id);
    }
}
