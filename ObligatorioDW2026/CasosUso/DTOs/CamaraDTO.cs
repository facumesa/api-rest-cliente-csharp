using System;
using System.Collections.Generic;
using System.Text;

namespace CasosUso.DTOs
{
    public class CamaraDTO : EquipoDTO
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Cantidad { get; set; }
        public string TipoSensor { get; set; }
        public string Resolucion { get; set; }
        public decimal TamanioPixel { get; set; }
    }
}
