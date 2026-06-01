using Excepciones;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Negocio.Dominio
{
    public class Montura : Equipo
    {
        protected Montura() {  }
        public TipoMontura Tipo{ get; set; }
        public decimal CargaUtil_kg{ get; set; }
        public bool EsComputarizado { get; set; }

        public Montura(string marca, string modelo, int cantidad, TipoMontura tipo, decimal carga, bool esComputarizado) : base(marca, modelo, cantidad)
        {
            Tipo = tipo;
            CargaUtil_kg = carga;
            EsComputarizado = esComputarizado;
        }
        public override void Validar()
        {
            base.Validar();
            if (Tipo == null) throw new DatosInvalidosException("El tipo de montura no puede ser vacía");
            if (CargaUtil_kg <= 0) throw new DatosInvalidosException("La carga útil no puede ser menor o igual a 0");
        }
    }
}
