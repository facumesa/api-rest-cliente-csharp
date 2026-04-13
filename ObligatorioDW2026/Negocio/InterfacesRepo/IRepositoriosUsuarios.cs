using System;
using System.Collections.Generic;
using System.Text;
using Negocio.Dominio;

namespace Negocio.InterfacesRepo
{
    public interface IRepositoriosUsuarios
    {
        void AddSocio(Socio obj);
    }
}
