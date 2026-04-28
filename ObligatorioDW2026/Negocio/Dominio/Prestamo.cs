using Negocio.InterfacesDominio;
using System;
using System.Collections.Generic;
using System.Text;
using Excepciones.ExcepcionesPropias;
using Excepciones;

namespace Negocio.Dominio
{
    public class Prestamo : IValidable
    {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public EstadoPrestamo Estado { get; set; }
        public int SocioId { get; set; }
        public Socio Socio { get; set; }
        public int CoordinadorId { get; set; }
        public Coordinador Coordinador{ get; set; } 
        public int MonturaId { get; set; }
        public Montura Montura { get; set; } 
        public int TelescopioId { get; set; }
        public Telescopio Telescopio { get; set; }
        public int? CamaraId { get; set; }
        public Camara? Camara { get; set; }
        public int? OcularId { get; set; }
        public Ocular? Ocular { get; set; }

        public void Validar()
        {
            if (FechaFin < FechaInicio)
                throw new DatosInvalidosException("La fecha de fin no puede ser previa al inicio.");

            if (Telescopio == null || Montura == null)
                throw new DatosInvalidosException("El telescopio y la montura son obligatorios.");

            if (Camara == null && Ocular == null)
                throw new DatosInvalidosException("Debe solicitar al menos una cámara o un ocular.");

            if (Montura.CargaUtil_kg < Telescopio.Peso)
                throw new DatosInvalidosException("La montura no soporta el peso del telescopio.");

            if (Camara != null && Montura.Tipo == TipoMontura.Alt_Azimutal)
                throw new DatosInvalidosException("Para astrofotografía se requiere montura Ecuatorial o Híbrida.");
        }
    }
 }

