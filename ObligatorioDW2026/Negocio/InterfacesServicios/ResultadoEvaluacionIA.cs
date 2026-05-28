namespace Negocio.InterfacesServicios
{
    public class ResultadoEvaluacionIA
    {
        public string Indicador { get; set; } // "IDEAL", "ADECUADO" o "NO RECOMENDABLE"
        public string Detalle { get; set; }    // Máximo 300 caracteres

    }
}