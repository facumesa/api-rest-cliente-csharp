using System;
using System.Collections.Generic;
using System.Text;
using Negocio.Dominio;
using Negocio.InterfacesRepo;

namespace AccesoDatos.Repositorios
{
    internal class RepositorioUsuarios : IRepositoriosUsuarios
    {
        private static List<Socio> socios = new List<Socio>();
        public void AddSocio(Socio obj)
        {
            obj.Validar();
            socios.Add(obj);
        }

    }
}
