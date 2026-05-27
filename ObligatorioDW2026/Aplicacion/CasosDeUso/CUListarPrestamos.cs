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
    public class CUListarPrestamos : IListarPrestamos
    {
        public IRepositorioPrestamos Repo { get; set; }
        public CUListarPrestamos(IRepositorioPrestamos repo) 
        {
            Repo = repo;
        }

        public IEnumerable<PrestamoListadoDTO> ObtenerListado()
        {
            return PrestamoMapper.ToListDTO(Repo.FindAll());
        }
    }
}
