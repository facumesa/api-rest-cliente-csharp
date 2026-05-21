using CasosUso.DTOs;
using Excepciones;
using Negocio.Dominio;
using Negocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Text;

namespace Aplicacion.Mappers
{
    internal class UsuarioMapper
    {
        public static UsuarioDTO ToDto(Usuario usu)
        {
            if (usu == null) return null;

            return new UsuarioDTO
            {
                Id = usu.Id,
                NombreCompleto = usu.NombreCompleto,
                Direccion = usu.Direccion,
                Telefono = usu.Telefono,
                Email = usu.Email.Valor,
                NombreUsuario = usu.NombreUsuario,
                Contrasenia = usu.Contrasenia.Valor,
                Rol = usu.Rol
            };
        }
        public static IEnumerable<UsuarioDTO> ToListDTO(IEnumerable<Usuario> usuarios)
        {
            List<UsuarioDTO> dtos = new List<UsuarioDTO>();

            if (usuarios != null)
            {
                foreach (Usuario u in usuarios)
                {
                    dtos.Add(ToDto(u));
                }
            }

            return dtos;
        }
    }
}
