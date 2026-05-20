using Aplicacion.Mappers;
using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Negocio.Dominio;
using Negocio.InterfacesRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.CasosDeUso
{
    public class CUListarPrestamosEntreFechas : IListarPrestamosEntreFechas
    {
        public IRepositorioPrestamos Repo{ get; set; }
        public CUListarPrestamosEntreFechas(IRepositorioPrestamos repo)
        {
            Repo = repo;
        }
        public IEnumerable<PrestamoListadoDTO> ObtenerListado(int id, int mes, int año)
        {
            IEnumerable<PrestamoListadoDTO> prestamos = PrestamoMapper.ToListDTO(Repo.ObtenerPrestamosPorFechas(id, mes, año));

            foreach (PrestamoListadoDTO p in prestamos)
            {
                if (p.Estado == EstadoPrestamo.PRESTADO.ToString() && p.FechaFin < DateTime.Now )
                {
                    p.Estado = "ATRASADO";
                }
            }
            return prestamos;
        }
    }
}
