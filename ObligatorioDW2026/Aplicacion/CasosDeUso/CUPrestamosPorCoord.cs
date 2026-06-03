using Aplicacion.Mappers;
using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Negocio.InterfacesRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.CasosDeUso
{
    public class CUPrestamosPorCoord : IPrestamosPorCoord
    {
        public IRepositorioPrestamos Repo{ get; set; }
        public CUPrestamosPorCoord(IRepositorioPrestamos repo)
        {
            Repo = repo;
        }
        public IEnumerable<PrestamoDTO> ObtenerListado(int id)
        {
            return PrestamoMapper.ToListDTOP(Repo.PrestamosPorCoordinador(id));
        }
    }
}
