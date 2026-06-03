using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio.Dominio
{
    public class Auditoria
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string TipoAccion { get; set; } 
        public string Detalle { get; set; } 
        public int CoordinadorId { get; set; }
        public Coordinador Coordinador { get; set; }
        public int PrestamoId { get; set; }
        public Prestamo Prestamo { get; set; }
    }
}
