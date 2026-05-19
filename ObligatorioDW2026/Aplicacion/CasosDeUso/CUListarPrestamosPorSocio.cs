using Aplicacion.Mappers;
using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Negocio.InterfacesRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.CasosDeUso
{
    public class CUListarPrestamosPorSocio : IListarPrestamosPorSocio
    {
        public IRepositorioPrestamos Repo { get; set; }
        public CUListarPrestamosPorSocio(IRepositorioPrestamos repo)
        {
            Repo = repo;
        }
        public IEnumerable<PrestamoListadoDTO> ObtenerListado(int id)
        {
            return PrestamoMapper.ToListDTO(Repo.ObtenerActivosPorSocio(id));
        }
    }
}
