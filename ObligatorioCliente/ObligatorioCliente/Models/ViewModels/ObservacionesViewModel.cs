using ObligatorioCliente.DTOs;

namespace ObligatorioCliente.Models.ViewModels
{
    public class ObservacionesViewModel
    {
        public PrestamoDTO Prestamo{ get; set; }
        public ObjetoCelesteDTO ObjetoCeleste { get; set; }
        public ObservacionDTO Observacion { get; set; }
        public IEnumerable<PrestamoListadoDTO>? Prestamos { get; set; }
        public IEnumerable<ObjetoCelesteDTO>? ObjetosCelestes { get; set; }


    }
}
