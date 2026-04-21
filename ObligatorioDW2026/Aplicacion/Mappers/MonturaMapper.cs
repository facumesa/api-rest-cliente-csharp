using CasosUso.DTOs;
using Excepciones;
using Negocio.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Mappers
{
    internal class MonturaMapper
    {
        public static Montura ToMontura(MonturaDTO dto)
        {
            if (dto == null) throw new DatosInvalidosException("No hay datos de Montura");

            Montura m = new Montura(
                dto.Marca,
                dto.Modelo,
                dto.Cantidad,
                (TipoMontura)Enum.Parse(typeof(TipoMontura), dto.Tipo),
                dto.CargaUtil_kg,
                dto.EsComputarizado
                );

            m.Id = dto.Id;

            return m;
        }

        public static MonturaDTO ToDTO(Montura montura)
        {
            if (montura == null) return null;

            return new MonturaDTO
            {
                Id = montura.Id,
                Marca = montura.Marca,
                Modelo = montura.Modelo,
                Cantidad = montura.Cantidad,
                Tipo = montura.Tipo.ToString(),
                CargaUtil_kg = montura.CargaUtil_kg,
                EsComputarizado = montura.EsComputarizado
            };
        }
    }
}
