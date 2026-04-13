using System;
using System.Collections.Generic;
using System.Text;
using Negocio.ValueObjects;

namespace Negocio.Dominio
{
    public class Socio : Usuario
    {
        public DateTime FechaRegistro { get; set; }
        public Socio(string nombreCompleto, string direccion, string telefono, Email email, string nombreUsuario, Password contrasenia) : base(nombreCompleto, direccion, telefono, email, nombreUsuario, contrasenia)
        {
            FechaRegistro = DateTime.Now;
        }

        public void Validar()
        {
            if(string.IsNullOrEmpty(NombreUsuario)) throw new Exception("El nombre de usuario no debe ser vacio");
        }
    }
}
