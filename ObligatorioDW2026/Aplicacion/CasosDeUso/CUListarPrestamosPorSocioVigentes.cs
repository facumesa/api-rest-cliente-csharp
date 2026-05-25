using Aplicacion.Mappers;
using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Negocio.InterfacesRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.CasosDeUso
{
    public class CUListarPrestamosPorSocioVigentes : IListarPrestamosPorSocioVigentes
    {
        public IRepositorioPrestamos Repo { get; set; }
        public CUListarPrestamosPorSocioVigentes(IRepositorioPrestamos repo)
        {
            Repo = repo;
        }
        public IEnumerable<PrestamoListadoDTO> ObtenerListado(int id)
        {
            IEnumerable<PrestamoListadoDTO> prestamos = PrestamoMapper.ToListDTO(Repo.ObtenerVigentesYNoDevueltosPorSocio(id));
            return prestamos;
        }
    }
}
