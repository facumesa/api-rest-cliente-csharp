using CasosUso.DTOs;
using Excepciones;
using Negocio.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Mappers
{
    internal class OcularMapper
    {
        public static Ocular ToOcular(OcularDTO dto)
        {
            if (dto == null) throw new DatosInvalidosException("No hay datos de Ocular");

            Ocular m = new Ocular(
                dto.Marca,
                dto.Modelo,
                dto.Cantidad,
                dto.Diametro_mm,
                dto.AnguloVision_grados
                );

            m.Id = dto.Id;

            return m;
        }

        public static OcularDTO ToDTO(Ocular ocular)
        {
            if (ocular == null) return null;

            return new OcularDTO
            {
                Id = ocular.Id,
                Marca = ocular.Marca,
                Modelo = ocular.Modelo,
                Cantidad = ocular.Cantidad,
                Diametro_mm = ocular.Diametro_mm,
                AnguloVision_grados = ocular.AnguloVision_grados
            };
        }
    }
}
