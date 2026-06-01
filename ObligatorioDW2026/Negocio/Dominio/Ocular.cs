using Excepciones;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Negocio.Dominio
{
    public class Ocular : Equipo
    {
        protected Ocular() { }
        public int Diametro_mm { get; set; }
        public int AnguloVision_grados { get; set; }

        public Ocular(string marca, string modelo, int cantidad, int diametro, int angulo) : base(marca, modelo, cantidad)
        {
            Diametro_mm = diametro;
            AnguloVision_grados = angulo;
        }
        public override void Validar()
        {
            base.Validar();
            if (Diametro_mm <= 0) throw new DatosInvalidosException("El diámetro no puede ser menor o igual a 0");
            if (AnguloVision_grados <= 0) throw new DatosInvalidosException("El ángulo de visión no puede ser menor o igual a 0");
        }
    }
}
