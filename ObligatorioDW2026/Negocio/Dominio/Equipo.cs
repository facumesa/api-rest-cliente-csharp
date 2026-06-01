using System;
using System.Collections.Generic;
using System.Text;
using Excepciones;
using Negocio.InterfacesDominio;

namespace Negocio.Dominio
{
    public abstract class Equipo : IValidable
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Cantidad { get; set; }

        protected Equipo() { }

        public Equipo(string marca, string modelo, int cantidad)
        {
            Marca = marca;
            Modelo = modelo;
            Cantidad = cantidad;
        }

        public virtual void Validar()
        {
            if (string.IsNullOrEmpty(Marca)) throw new DatosInvalidosException("La marca no puede ser vacía");
            if (string.IsNullOrEmpty(Modelo)) throw new DatosInvalidosException("El modelo no puede ser vacío");
            if (Cantidad < 0) throw new DatosInvalidosException("La cantidad no puede ser menor a 0");
        }
    }


}

