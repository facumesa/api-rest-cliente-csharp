using Aplicacion.Mappers;
using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Negocio.InterfacesRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.CasosDeUso
{
    public class CUBuscarAuditoriaPorPrestamo : IBuscarAuditoriaPorPrestamo
    {
        public IRepositorioAuditoria Repo { get; set; }
        public CUBuscarAuditoriaPorPrestamo(IRepositorioAuditoria repo)
        {
            Repo = repo;
        }
        IEnumerable<AuditoriaDTO> IBuscarAuditoriaPorPrestamo.Ejecutar(int id)
        {
            return AuditoriaMapper.ToListDTO(Repo.AuditoriasPorPrestamo(id));
        }
    }
}
