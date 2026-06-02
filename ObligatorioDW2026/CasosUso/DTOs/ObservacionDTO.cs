using System;
using System.Collections.Generic;
using System.Text;

namespace CasosUso.DTOs
{
    public class ObservacionDTO
    {
        public int Id { get; set; }
        public DateTime FechaObservacion { get; set; }
        public int PrestamoId { get; set; }
        public int ObjetoCelesteId { get; set; }
        public string ResultadoAdecuacion { get; set; }
        public string MotivoAdecuacion { get; set; }
        public string? TipoObservacion { get; set; }
    }
}
