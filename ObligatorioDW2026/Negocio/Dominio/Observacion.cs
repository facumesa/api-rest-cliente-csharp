using Excepciones;
using Negocio.InterfacesDominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio.Dominio
{
    public class Observacion : IValidable
    {
        public int Id { get; set; }
        public DateTime FechaObservacion { get; set; }
        public int PrestamoId { get; set; }
        public Prestamo Prestamo { get; set; }
        public int ObjetoCelesteId { get; set; }
        public ObjetoCeleste ObjetoCeleste { get; set; }
        public string ResultadoAdecuacion { get; set; }
        public string MotivoAdecuacion { get; set; }

        public void Validar()
        {
            if (FechaObservacion == DateTime.MinValue || FechaObservacion < DateTime.Now)
                throw new DatosInvalidosException("La fecha de observación es inválida.");

            if (string.IsNullOrEmpty(ResultadoAdecuacion))
                throw new DatosInvalidosException("Debe evaluar la adecuación antes de registrar el alta.");

            if (ResultadoAdecuacion != "IDEAL" &&
                ResultadoAdecuacion != "ADECUADO" &&
                ResultadoAdecuacion != "NO RECOMENDABLE")
            {
                throw new Exception("El indicador de adecuación no es válido.");
            }
        }
    }
}
