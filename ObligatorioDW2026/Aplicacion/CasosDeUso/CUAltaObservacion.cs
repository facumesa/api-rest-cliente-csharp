using Aplicacion.Mappers;
using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Negocio.Dominio;
using Negocio.InterfacesRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.CasosDeUso
{
    public class CUAltaObservacion : IAltaObservacion
    {
        public IRepositorioObservaciones Repo{ get; set; }
        public CUAltaObservacion(IRepositorioObservaciones repo)
        {
            Repo = repo;
        }
        public void Ejecutar(ObservacionDTO nuevo)
        {
            Observacion obs = ObservacionMapper.ToObservacion(nuevo);
            Repo.Add(obs);
            nuevo.Id = obs.Id;
        }
    }
}
