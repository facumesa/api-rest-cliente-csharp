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

        public Equipo(string marca, string modelo, int cantidad)
        {
            Marca = marca;
            Modelo = modelo;
            Cantidad = cantidad;
        }

        public void Validar()
        {
            if(string.IsNullOrEmpty(Marca)) throw new Exception("La marca no puede ser vacía");
            if(string.IsNullOrEmpty(Marca)) throw new Exception("La marca no puede ser vacía");
            if(Cantidad < 0) throw new Exception("La cantidad no puede ser menor a 0");
        }
    }


}

