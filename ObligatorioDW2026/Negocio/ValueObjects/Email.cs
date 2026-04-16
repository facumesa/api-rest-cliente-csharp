using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Negocio.ValueObjects
{
    [ComplexType]
    public class Email
    {
        public string Valor { get; set; }

        public Email(string valor)
        {
            Valor = valor;
            Validar();
        }

        private void Validar()
        {
            if(string.IsNullOrEmpty(Valor) || !Valor.Contains('@')) throw new Exception("El email debe contener una arroba (@).");
        }
    }
}
