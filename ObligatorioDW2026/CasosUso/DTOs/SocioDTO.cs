using System;
using System.Collections.Generic;
using System.Text;

namespace CasosUso.DTOs
{
    public class SocioDTO
    {
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; } 
        public string NombreUsuario { get; set; }
        public string Password { get; set; } 
        public DateTime FechaRegistro { get; set; }
    }
}
