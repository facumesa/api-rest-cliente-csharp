using Excepciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
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
            if (string.IsNullOrWhiteSpace(Valor)) throw new DatosInvalidosException("El email es obligatorio.");
            if (string.IsNullOrEmpty(Valor) || !Valor.Contains('@')) throw new DatosInvalidosException("El email debe contener una arroba (@).");
        }
    }
}
