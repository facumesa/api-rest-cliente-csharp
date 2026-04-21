using CasosUso.DTOs;
using Excepciones;
using Negocio.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Mappers
{
    internal class TelescopioMapper
    {
        public static Telescopio ToTelescopio(TelescopioDTO dto)
        {
            if (dto == null) throw new DatosInvalidosException("No hay datos de Telescopio");

            Telescopio t = new Telescopio(
                dto.Marca,
                dto.Modelo,
                dto.Cantidad,
                dto.Apertura_mm,
                dto.DistanciaFocal_mm,
                dto.RelacionFocal,
                dto.Peso
                );

            t.Id = dto.Id;

            return t;
        }

        public static TelescopioDTO ToDTO(Telescopio telescopio)
        {
            if (telescopio == null) return null;

            return new TelescopioDTO
            {
                Id = telescopio.Id,
                Marca = telescopio.Marca,
                Modelo = telescopio.Modelo,
                Cantidad = telescopio.Cantidad,
                Apertura_mm = telescopio.Apertura_mm,
                DistanciaFocal_mm = telescopio.DistanciaFocal_mm,
                RelacionFocal = telescopio.RelacionFocal,
                Peso = telescopio.Peso,
                TipoEquipo = "Telescopio"
            };
        }
    }
}
