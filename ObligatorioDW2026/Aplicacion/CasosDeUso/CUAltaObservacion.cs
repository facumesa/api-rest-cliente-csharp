using Aplicacion.Mappers;
using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Excepciones;
using Excepciones.ExcepcionesPropias;
using Negocio.Dominio;
using Negocio.InterfacesRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.CasosDeUso
{
    public class CUAltaObservacion : IAltaObservacion
    {
        public IRepositorioObservaciones RepoObs{ get; set; }
        public IRepositorioPrestamos RepoPrestamos { get; set; }
        public CUAltaObservacion(IRepositorioObservaciones repoObs, IRepositorioPrestamos repoPrestamos)
        {
            RepoObs = repoObs;
            RepoPrestamos = repoPrestamos;
        }
        public void Ejecutar(ObservacionDTO nuevo)
        {
            Observacion obs = ObservacionMapper.ToObservacion(nuevo);
            Prestamo prestamoReal = RepoPrestamos.FindById(obs.PrestamoId);
            if (prestamoReal == null)
            {
                throw new DatosInvalidosException("El préstamo especificado no existe.");
            }
            if (prestamoReal.Estado == EstadoPrestamo.DEVUELTO)
            {
                throw new OperacionInvalidaException("No se pueden registrar observaciones para un préstamo que ya fue devuelto.");
            }
            if (prestamoReal.FechaFin.Date < DateTime.Today)
            {
                throw new OperacionInvalidaException("No se pueden registrar observaciones para un préstamo atrasado.");
            }
            if (RepoObs.ExisteObservacionDuplicada(obs))
            {
                throw new OperacionInvalidaException("Ya registraste una observación para este astro, para la misma fecha, usando el mismo equipamiento.");
            }
            RepoObs.Add(obs);
            nuevo.Id = obs.Id;
        }
    }
}
