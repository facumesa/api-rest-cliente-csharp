using System;
using System.Collections.Generic;
using System.Text;

namespace CasosUso.DTOs
{
    public class PrestamoListadoDTO
    {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string NombreSocio { get; set; }
        public string EmailSocio { get; set; }
        public string DescripcionTelescopio { get; set; }
        public string DescripcionMontura { get; set; }
        public string DescripcionCamara { get; set; }
        public string DescripcionOcular { get; set; }
        public string NombreCoordinador { get; set; }
    }
}
