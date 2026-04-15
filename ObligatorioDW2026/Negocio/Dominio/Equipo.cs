using System;
using System.Collections.Generic;
using System.Text;
using Negocio.InterfacesDominio;

namespace Negocio.Dominio
{
    public abstract class Equipo : IValidable
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Cantidad { get; set; }

        public Equipo()
        {
            
        }

        public void Validar()
        {
            throw new NotImplementedException();
        }
    }


}

