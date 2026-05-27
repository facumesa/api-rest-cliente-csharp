using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio.Dominio
{
    public class Camara : Equipo
    {
        protected Camara() { }
        public TipoSensor TipoSensor { get; set; }
        public string Resolucion { get; set; }
        public decimal TamanioPixel { get; set; }
        public Camara(string marca, string modelo, int cantidad, TipoSensor tipoSensor, string resolucion, decimal tamanioPixel) : base(marca, modelo, cantidad)
        {
            TipoSensor = tipoSensor;
            Resolucion = resolucion;
            TamanioPixel = tamanioPixel;
        }
        public override void Validar()
        {
            base.Validar();
            if (TipoSensor == null) throw new Exception("El tipo de sensor no puede ser vacío");
            if (string.IsNullOrEmpty(Resolucion)) throw new Exception("La resolución no puede ser vacía");
            if (TamanioPixel <= 0) throw new Exception("El tamaño del pixel no puede ser menor o igual a 0");
        }
    }
}   
