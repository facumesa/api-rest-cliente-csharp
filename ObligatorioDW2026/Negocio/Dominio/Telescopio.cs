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
        }
    }
}
