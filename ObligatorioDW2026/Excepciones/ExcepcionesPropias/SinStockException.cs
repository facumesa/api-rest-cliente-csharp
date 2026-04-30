using System;
using System.Collections.Generic;
using System.Text;

namespace Excepciones.ExcepcionesPropias
{
    public class SinStockException : Exception
    {
        public SinStockException()
        {
        }

        public SinStockException(string? message) : base(message)
        {
        }

        public SinStockException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
