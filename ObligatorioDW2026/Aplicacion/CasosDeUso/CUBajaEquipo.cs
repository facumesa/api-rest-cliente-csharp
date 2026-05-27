using CasosUso.InterfacesCU;
using Excepciones;
using Excepciones.ExcepcionesPropias;
using Negocio.InterfacesRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.CasosDeUso
{
    public class CUBajaEquipo : IBajaEquipo
    {
        public IRepositorioEquipos RepoEquipos { get; set; }
        public IRepositorioPrestamos RepoPrestamos { get; set; }
        public CUBajaEquipo(IRepositorioEquipos repoEquipos, IRepositorioPrestamos repoPrestamos)
        {
            RepoEquipos = repoEquipos;
            RepoPrestamos = repoPrestamos;
        }

        public void Ejecutar(int id)
        {
            if (RepoEquipos.FindById(id) == null) throw new OperacionInvalidaException("El equipo a borrar no existe");

            if (RepoPrestamos.EquipoEnPrestamo(id))
            {
                throw new EntidadConRelacionException("El equipo no se puede eliminar, tiene prestamos activos asociados");
            }
            RepoEquipos.Remove(id);
        }
    }


}
