using CasosUso.DTOs;
using Excepciones;
using Negocio.Dominio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Mappers
{
    internal class PrestamoMapper
    {
        public static Prestamo ToPrestamo(PrestamoDTO dto)
        {
            if (dto == null) throw new DatosInvalidosException("No hay datos de Prestamo");

            Prestamo p = new Prestamo();
            
            p.Id = dto.Id;
            p.FechaInicio = dto.FechaInicio;
            p.FechaFin = dto.FechaFin;
            p.Estado = EstadoPrestamo.PRESTADO;
            p.SocioId = dto.SocioId;
            p.CoordinadorId = dto.CoordinadorId;
            p.MonturaId = dto.MonturaId;
            p.TelescopioId = dto.TelescopioId;
            p.CamaraId = dto.CamaraId;
            p.OcularId = dto.OcularId;
            p.FechaPrestamo = DateTime.Now;

            return p;
        
        }

        public static PrestamoDTO ToDTO(Prestamo prestamo)
        {
            if (prestamo == null) return null;

            return new PrestamoDTO
            {
                Id = prestamo.Id,
                FechaInicio = prestamo.FechaInicio,
                FechaFin = prestamo.FechaFin,
                Estado = prestamo.Estado.ToString(),
                SocioId = prestamo.SocioId,
                CoordinadorId = prestamo.CoordinadorId,
                MonturaId = prestamo.MonturaId,
                TelescopioId = prestamo.TelescopioId,
                CamaraId = prestamo.CamaraId,
                OcularId = prestamo.OcularId,
                FechaPrestamo = prestamo.FechaPrestamo
            };
        }

        public static PrestamoListadoDTO ToListadoDTO(Prestamo p)
        {
            return new PrestamoListadoDTO
            {
                Id = p.Id,
                FechaInicio = p.FechaInicio,
                FechaFin = p.FechaFin,
                NombreSocio = p.Socio != null ? $"{p.Socio.NombreCompleto}" : "Desconocido",
                EmailSocio = p.Socio != null ? $"{p.Socio.Email.Valor}" : "Desconocido",
                DescripcionTelescopio = p.Telescopio != null ? $"{p.Telescopio.Marca} {p.Telescopio.Modelo}" : "Ninguno",
                DescripcionMontura = p.Montura != null ? $"{p.Montura.Marca} | {p.Montura.Modelo}" : "Ninguno",
                DescripcionCamara = p.Camara != null ? $"{p.Camara.Marca} | {p.Camara.Modelo}" : "Sin cámara seleccionada",
                DescripcionOcular = p.Ocular != null ? $"{p.Ocular.Marca} | {p.Ocular.Diametro_mm}mm" : "Sin ocular seleccionado",
                NombreCoordinador = p.Coordinador != null ? p.Coordinador.NombreCompleto : "Sistema",
                Estado = p.Estado != null ? $"{p.Estado.ToString()}" : "Sin estado"
            };
        }

        public static IEnumerable<PrestamoListadoDTO> ToListDTO(IEnumerable<Prestamo> lista)
        {
            if (lista == null) return new List<PrestamoListadoDTO>();

            return lista.Select(p => ToListadoDTO(p)).ToList();
        }

    }
}
