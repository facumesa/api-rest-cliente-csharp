using System;
using System.Collections.Generic;
using System.Text;
using Negocio.ValueObjects;

namespace Negocio.Dominio
{
    public class Coordinador : Usuario
    {
        protected Coordinador() { }
        public Coordinador(string nombreCompleto, string direccion, string telefono, Email email, string nombreUsuario, Password contrasenia) : base(nombreCompleto, direccion, telefono, email, nombreUsuario, contrasenia)
        {
            Rol = "Coordinador";
        }
        public override void Validar()
        {
            base.Validar();
        }
    }
}
