using CasosUso.DTOs;
using Excepciones;
using Negocio.Dominio;
using Negocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Mappers
{
    public class CoordinadorMapper
    {
        public static Coordinador ToCoordinador(CoordinadorDTO dto)
        {
            if (dto == null) throw new DatosInvalidosException("No hay datos de Coordinador");

            return new Coordinador(
            dto.NombreCompleto,
            dto.Direccion,
            dto.Telefono,
            new Email(dto.Email),
            dto.NombreUsuario,
            new Password(dto.Contrasenia));
        }

        public static CoordinadorDTO ToDTO(Coordinador coordinador)
        {
            if (coordinador == null) throw new DatosInvalidosException("No hay datos de Coordinador");

            return new CoordinadorDTO
            {
                Id = coordinador.Id,
                NombreCompleto = coordinador.NombreCompleto,
                Direccion = coordinador.Direccion,
                Telefono = coordinador.Telefono,
                Email = coordinador.Email.Valor,
                NombreUsuario = coordinador.NombreUsuario,
                Contrasenia = coordinador.Contrasenia.Valor,
                Rol = coordinador.Rol
            };

        }

        public static IEnumerable<CoordinadorDTO> ToListDTO(IEnumerable<Coordinador> coordinadores)
        {
            List<CoordinadorDTO> dtos = new List<CoordinadorDTO>();

            if (coordinadores != null)
            {
                foreach (Coordinador cor in coordinadores)
                {
                    dtos.Add(ToDTO(cor));
                }
            }

            return dtos;
        }
    }
}
