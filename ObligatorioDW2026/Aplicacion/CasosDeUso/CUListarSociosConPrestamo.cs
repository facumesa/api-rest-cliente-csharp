using Aplicacion.Mappers;
using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Negocio.InterfacesRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.CasosDeUso
{
    public class CUListarSociosConPrestamo : IListarSociosConPrestamo
    {
        public IRepositorioUsuarios Repo{ get; set; }
        public CUListarSociosConPrestamo(IRepositorioUsuarios repo)
        {
            Repo = repo;
        }
        public IEnumerable<SocioDTO> ObtenerListado()
        {
            return SocioMapper.ToListDTO(Repo.ObtenerSociosConPrestamosActivos());
        }
    }
}
