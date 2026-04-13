using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio.ValueObjects
{
    public class Password
    {
        public string Valor { get; set; }

        public Password(string valor)
        {
            Valor = valor;
            Validar();
        }

        private void Validar()
        {
            if (string.IsNullOrEmpty(Valor) || Valor.Length < 8)
            {
                throw new Exception("La contraseña debe contener 8 caracteres como minimo");
            }
            bool tieneMayuscula = false;
            bool tieneMinuscula = false;
            bool tieneNumero = false;
            bool tieneEspecial = false;
            
            foreach (char c in Valor)
            {
                if (char.IsUpper(c)) tieneMayuscula = true;
                else if (char.IsLower(c)) tieneMinuscula = true;
                else if (char.IsDigit(c)) tieneNumero = true;
                else if (!char.IsLetterOrDigit(c)) tieneEspecial = true;
            }

            if (!tieneMayuscula || !tieneMinuscula || !tieneNumero || !tieneEspecial)
            {
                throw new Exception("La contraseña debe tener: mayúscula, minúscula, número y un carácter especial.");
            }
        }

    }
}
