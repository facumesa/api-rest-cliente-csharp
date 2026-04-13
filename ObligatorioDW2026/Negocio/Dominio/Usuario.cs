using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using Negocio.ValueObjects;

namespace Negocio.Dominio
{
    public abstract class Usuario
    {
        private static int ultNum = 1;
        public int Id { get; private set; }
        public string NombreCompleto { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public Email Email { get; set; }
        public string NombreUsuario { get; set; }
        public Password Contrasenia { get; set; }

        public Usuario(string nombreCompleto, string direccion, string telefono, Email email, string nombreUsuario, Password contrasenia)
        {
            Id = ultNum++;
            NombreCompleto = nombreCompleto;
            Direccion = direccion;
            Telefono = telefono;
            Email = email;
            NombreUsuario = nombreUsuario;
            Contrasenia = contrasenia;
        }
        
    }
}
