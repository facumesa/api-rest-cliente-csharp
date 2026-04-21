using System;
using System.Collections.Generic;
using System.Text;

namespace CasosUso.DTOs
{
    public class EquipoDTO
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Cantidad { get; set; }
        public string? TipoEquipo { get; set; } 
    }
}
