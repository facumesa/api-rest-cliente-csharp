using Aplicacion.Mappers;
using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Negocio.InterfacesRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.CasosDeUso
{
    public class CUBuscarPrestamo : IBuscarPrestamo
    {
        public IRepositorioPrestamos Repo{ get; set; }
        public CUBuscarPrestamo(IRepositorioPrestamos repo)
        {
            Repo = repo;
        }
        public PrestamoListadoDTO Ejecutar(int id)
        {
            return PrestamoMapper.ToListadoDTO(Repo.FindById(id));
        }
    }
}
