using CasosUso.DTOs;
using Negocio.Dominio;

namespace Presentacion.Models.ViewModels
{
    public class PrestamoViewModel
    {
        public PrestamoDTO Prestamo { get; set; }

        public IEnumerable<SocioDTO>? Socios { get; set; }
        public IEnumerable<OcularDTO>? Oculares { get; set; }
        public IEnumerable<TelescopioDTO>? Telescopios { get; set; }
        public IEnumerable<MonturaDTO>? Monturas { get; set; }
        public IEnumerable<CamaraDTO>? Camaras { get; set; }
        
    }
}
