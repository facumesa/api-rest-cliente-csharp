using System;
using System.Collections.Generic;
using System.Text;
using CasosUso.DTOs;

namespace CasosUso.InterfacesCU
{
    public interface ILoginUsuarios
    {
        UsuarioDTO Ejecutar(string nombreUsuario, string password);
    }
}
