using System;
using System.Collections.Generic;
using System.Text;
using Negocio.ValueObjects;

namespace Negocio.Dominio
{
    public class Administrador : Usuario
    {
        protected Administrador() { }
        public Administrador(string nombreCompleto, string direccion, string telefono, Email email, string nombreUsuario, Password contrasenia) : base(nombreCompleto, direccion, telefono, email, nombreUsuario, contrasenia)
        {
            Rol = "Admin";
        }

        public override void Validar()
        {
            base.Validar();
        }
    }
}
