using System;
using System.Collections.Generic;
using System.Text;
using CasosUso.DTOs;
using Negocio.Dominio;

namespace Aplicacion.Mappers
{
    internal class UsuarioMapper
    {
        public static UsuarioDTO ToDto(Usuario usu)
        {
            if (usu == null) return null;

            return new UsuarioDTO
            {
                NombreUsuario = usu.Email.Valor,
                Contrasenia = usu.Contrasenia.Valor,
                Rol = usu.Rol
            };
        }
    }
}
