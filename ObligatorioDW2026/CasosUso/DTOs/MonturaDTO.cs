using System;
using System.Collections.Generic;
using System.Text;

namespace CasosUso.DTOs
{
    public class MonturaDTO : EquipoDTO
    {
        public string Tipo { get; set; }
        public decimal CargaUtil_kg { get; set; }
        public bool EsComputarizado { get; set; }
    }
}
