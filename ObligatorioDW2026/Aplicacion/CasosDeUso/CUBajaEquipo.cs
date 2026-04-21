using CasosUso.InterfacesCU;
using Negocio.InterfacesRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.CasosDeUso
{
    public class CUBajaEquipo : IBajaEquipo
    {
        public IRepositorioEquipos Repo { get; set; }
        public CUBajaEquipo(IRepositorioEquipos repo)
        {
            Repo = repo;
        }

        public void Ejecutar(int id)
        {
            Repo.Remove(id);
        }
    }
}
