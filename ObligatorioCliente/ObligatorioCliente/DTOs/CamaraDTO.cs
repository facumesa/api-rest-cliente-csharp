using System;
using System.Collections.Generic;
using System.Text;

namespace ObligatorioCliente.DTOs
{
    public class CamaraDTO : EquipoDTO
    {
        public string TipoSensor { get; set; }
        public string Resolucion { get; set; }
        public decimal TamanioPixel { get; set; }
    }
}
