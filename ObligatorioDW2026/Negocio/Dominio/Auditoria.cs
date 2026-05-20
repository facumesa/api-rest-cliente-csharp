using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio.Dominio
{
    //MODIFICAR PARA RF11
    public class Auditoria
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string TipoAccion { get; set; } 
        public string Detalle { get; set; } 
        public int CoordinadorId { get; set; }
        public Coordinador Coordinador { get; set; }
    }
}
