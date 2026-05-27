using Excepciones;
using Negocio.InterfacesDominio;
using Negocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace Negocio.Dominio
{
    public abstract class Usuario : IValidable
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public Email Email { get; set; }
        public string NombreUsuario { get; set; }
        public Password Contrasenia { get; set; }
        public string Rol { get; protected set; }

        protected Usuario() { }

        public Usuario(string nombreCompleto, string direccion, string telefono, Email email, string nombreUsuario, Password contrasenia)
        {
            NombreCompleto = nombreCompleto;
            Direccion = direccion;
            Telefono = telefono;
            Email = email;
            NombreUsuario = nombreUsuario;
            Contrasenia = contrasenia;
        }
         
        public virtual void Validar()
        {
            if(string.IsNullOrEmpty(NombreUsuario)) throw new DatosInvalidosException("El nombre de usuario no debe ser vacio");
            if (string.IsNullOrEmpty(NombreCompleto)) throw new DatosInvalidosException("El nombre no puede estar vacío.");
            if (string.IsNullOrEmpty(Direccion)) throw new DatosInvalidosException("La dirección es requerida.");
        }
    }
}
