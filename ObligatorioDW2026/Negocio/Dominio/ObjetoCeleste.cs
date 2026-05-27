using Excepciones;
using Negocio.InterfacesDominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio.Dominio
{
    public class ObjetoCeleste : IValidable
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public decimal MagnitudAparente { get; set; }

        public void Validar()
        {
            if (string.IsNullOrEmpty(Nombre)) throw new DatosInvalidosException("El nombre del objeto celeste no puede estar vacío.");
            if (string.IsNullOrEmpty(Tipo)) throw new DatosInvalidosException("El tipo del objeto celeste no puede estar vacío.");
        }
    }
}
