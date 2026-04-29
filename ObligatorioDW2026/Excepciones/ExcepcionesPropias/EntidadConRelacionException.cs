using System;
using System.Collections.Generic;
using System.Text;

namespace Excepciones.ExcepcionesPropias
{
    public class EntidadConRelacionException : Exception
    {
        public EntidadConRelacionException()
        {
        }

        public EntidadConRelacionException(string? message) : base(message)
        {
        }

        public EntidadConRelacionException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
