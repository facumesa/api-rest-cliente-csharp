using CasosUso.DTOs;
using Excepciones;
using Negocio.Dominio;
using Negocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Mappers
{
    internal class CamaraMapper
    {
        public static Camara ToCamara(CamaraDTO dto)
        {
            if (dto == null) throw new DatosInvalidosException("No hay datos de Camara");

            Camara c = new Camara(
                dto.Marca,
                dto.Modelo,
                dto.Cantidad,
                (TipoSensor)Enum.Parse(typeof(TipoSensor), dto.TipoSensor),
                dto.Resolucion,
                dto.TamanioPixel
                );

            c.Id = dto.Id;

            return c;
        }

        public static CamaraDTO ToDTO(Camara camara)
        {
            if (camara == null) return null;

            return new CamaraDTO
            {
                Id = camara.Id,
                Marca = camara.Marca,
                Modelo = camara.Modelo,
                Cantidad = camara.Cantidad,
                TipoSensor = camara.TipoSensor.ToString(),
                Resolucion = camara.Resolucion,
                TamanioPixel = camara.TamanioPixel,
                TipoEquipo = "Camara"
            };
        }
    }
}
