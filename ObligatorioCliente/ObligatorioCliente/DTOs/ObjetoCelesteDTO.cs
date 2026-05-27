using System;
using System.Collections.Generic;
using System.Text;

namespace ObligatorioCliente.DTOs
{
    public class ObjetoCelesteDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public decimal MagnitudAparente { get; set; }
    }
}
