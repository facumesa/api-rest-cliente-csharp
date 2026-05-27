using System;
using System.Collections.Generic;
using System.Text;

namespace CasosUso.DTOs
{
    public class TelescopioDTO : EquipoDTO
    {
        public int Apertura_mm { get; set; }
        public int DistanciaFocal_mm { get; set; }
        public string RelacionFocal { get; set; }
        public decimal Peso { get; set; }
    }
}
