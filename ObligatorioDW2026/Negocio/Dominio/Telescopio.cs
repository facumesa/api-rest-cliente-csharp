using Excepciones;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Negocio.Dominio
{
    public class Telescopio : Equipo
    {
        protected Telescopio() { }
        public int Apertura_mm { get; set; }
        public int DistanciaFocal_mm { get; set; }
        public string RelacionFocal { get; set; }
        public decimal Peso { get; set; }

        public Telescopio(string marca, string modelo, int cantidad, int apertura, int distancia, string relacion, decimal peso) : base(marca, modelo, cantidad)
        {
            Apertura_mm = apertura;
            DistanciaFocal_mm = distancia;
            RelacionFocal = relacion;
            Peso = peso;
        }
        public override void Validar()
        {
            base.Validar();
            if (Apertura_mm <= 0) throw new DatosInvalidosException("La apertura no puede ser menor o igual a 0");
            if (DistanciaFocal_mm <= 0) throw new DatosInvalidosException("La distancia focal no puede ser menor o igual a 0");
            if (string.IsNullOrEmpty(RelacionFocal)) throw new DatosInvalidosException("La relacion focal no puede ser vacía");
            if (Peso <= 0) throw new DatosInvalidosException("El peso no puede ser menor o igual a 0");

        }
    }
}
