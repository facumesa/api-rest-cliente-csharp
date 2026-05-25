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
    public class CUListarCuerposCelestes : IListarCuerposCelestes
    {
        public IRepositorioObjetosCelestes Repo { get; set; }
        public CUListarCuerposCelestes(IRepositorioObjetosCelestes repo)
        {
            Repo = repo;
        }
        public IEnumerable<ObjetoCelesteDTO> ObtenerListado()
        {
            IEnumerable<ObjetoCeleste> objetos = Repo.FindAll();
            IEnumerable<ObjetoCelesteDTO> dtos = ObjetoCelesteMapper.ToListDTO(objetos);
            return dtos;
        }
    }
}
