using System;
using System.Collections.Generic;
using System.Text;
using CasosUso.DTOs;
using Excepciones;
using Negocio.Dominio;
using Negocio.ValueObjects;

namespace Aplicacion.Mappers
{
    internal class SocioMapper
    {
        public static Socio ToSocio(SocioDTO dto)
        {
            if (dto == null) throw new DatosInvalidosException("No hay datos de Socio");

            return new Socio(
            dto.NombreCompleto,
            dto.Direccion,
            dto.Telefono,
            new Email(dto.Email),
            dto.NombreUsuario,
            new Password(dto.Contrasenia));
        }

        public static SocioDTO ToDTO(Socio socio)
        {
            if (socio == null) throw new DatosInvalidosException("No hay datos de socio");

            return new SocioDTO
            {
                Id = socio.Id,
                NombreCompleto = socio.NombreCompleto,
                Direccion = socio.Direccion,
                Telefono = socio.Telefono,
                Email = socio.Email.Valor,
                NombreUsuario = socio.NombreUsuario,
                Contrasenia = socio.Contrasenia.Valor,
                FechaRegistro = socio.FechaRegistro,
                Rol = socio.Rol
            };

        }

        public static IEnumerable<SocioDTO> ToListDTO(IEnumerable<Socio> socios)
        {
            List<SocioDTO> dtos = new List<SocioDTO>();

            if (socios != null)
            {
                foreach (Socio soc in socios)
                {
                    dtos.Add(ToDTO(soc));
                }
            }

            return dtos;
        }
    }

}