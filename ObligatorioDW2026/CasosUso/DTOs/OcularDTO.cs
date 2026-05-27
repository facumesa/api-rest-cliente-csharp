using System;
using System.Collections.Generic;
using System.Text;

namespace CasosUso.DTOs
{
    public class OcularDTO : EquipoDTO
    {
        public int Diametro_mm { get; set; }
        public int AnguloVision_grados { get; set; }

    }
}
