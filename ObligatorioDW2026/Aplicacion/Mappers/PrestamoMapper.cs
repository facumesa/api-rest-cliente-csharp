using CasosUso.DTOs;
using Excepciones;
using Negocio.Dominio;
using System;
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
    }
}
