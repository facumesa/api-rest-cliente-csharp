using CasosUso.DTOs;
using Excepciones;
using Negocio.Dominio;
using Negocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Mappers
{
    internal class AdministradorMapper
    {
        public static Administrador ToAdministrador(AdministradorDTO dto)
        {
            if (dto == null) throw new DatosInvalidosException("No hay datos de Administrador");

            return new Administrador(
            dto.NombreCompleto,
            dto.Direccion,
            dto.Telefono,
            new Email(dto.Email),
            dto.NombreUsuario,
            new Password(dto.Contrasenia));
        }

        public static AdministradorDTO ToDTO(Administrador administrador)
        {
            if (administrador == null) throw new DatosInvalidosException("No hay datos de Administrador");

            return new AdministradorDTO
            {
                Id = administrador.Id,
                NombreCompleto = administrador.NombreCompleto,
                Direccion = administrador.Direccion,
                Telefono = administrador.Telefono,
                Email = administrador.Email.Valor,
                NombreUsuario = administrador.NombreUsuario,
                Contrasenia = administrador.Contrasenia.Valor,
                Rol = administrador.Rol
            };

        }

        public static IEnumerable<AdministradorDTO> ToListDTO(IEnumerable<Administrador> administradores)
        {
            List<AdministradorDTO> dtos = new List<AdministradorDTO>();

            if (administradores != null)
            {
                foreach (Administrador ad in administradores)
                {
                    dtos.Add(ToDTO(ad));
                }
            }

            return dtos;
        }
    }
}

