using Aplicacion.Mappers;
using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Negocio.Dominio;
using Negocio.InterfacesRepo;
using Negocio.InterfacesServicios;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.CasosDeUso
{
    public class CUEvaluarAdecuacion : IEvaluarAdecuacion
    {
        public IRepositorioPrestamos RepoPrestamos{ get; set; }
        public IServicioGeminiIA ServicioIA { get; set; }
        public IRepositorioObjetosCelestes RepoObjetosCelestes { get; set; }

        // Inyección por constructor de las interfaces
        public CUEvaluarAdecuacion(IServicioGeminiIA servicioIA, IRepositorioPrestamos repoPrestamos, IRepositorioObjetosCelestes repoOC)
        {
            RepoPrestamos = repoPrestamos;
            ServicioIA = servicioIA;
            RepoObjetosCelestes = repoOC;
        }

        public ObservacionDTO Ejecutar(int prestamoId, int objetoId)
        {
            Prestamo prestamo = RepoPrestamos.FindById(prestamoId) ?? throw new Exception("El préstamo seleccionado no existe.");
            ObjetoCeleste oc = RepoObjetosCelestes.FindById(objetoId) ?? throw new Exception("El objeto celeste seleccionado no existe.");

            Telescopio telescopio = prestamo.Telescopio;
            Montura montura = prestamo.Montura;
            Camara camara = prestamo.Camara;
            Ocular ocular = prestamo.Ocular;

            ResultadoEvaluacionIA resultadoIA = ServicioIA.EvaluarAdecuacion(telescopio, montura, camara, ocular, oc);

            ObservacionDTO nuevaObservacion = new ObservacionDTO
            {
                PrestamoId = prestamoId,
                ResultadoAdecuacion = resultadoIA.Indicador?.ToUpper().Trim(),
                MotivoAdecuacion = resultadoIA.Detalle
            };

            return nuevaObservacion;
        }
    }
}
