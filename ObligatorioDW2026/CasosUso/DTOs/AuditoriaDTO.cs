using System;
using System.Collections.Generic;
using System.Text;

namespace CasosUso.DTOs
{
    public class AuditoriaDTO
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoAccion { get; set; }
        public string Detalle { get; set; }
        public int CoordinadorId { get; set; }
        public int PrestamoId { get; set; }
    }
}
