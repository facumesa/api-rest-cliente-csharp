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
    public class CUListarUsuarios : IListarUsuarios
    {
        public IRepositorioUsuarios Repo { get; set; }
        public CUListarUsuarios(IRepositorioUsuarios repo)
        {
            Repo = repo;
        }
        public IEnumerable<UsuarioDTO> ObtenerListado()
        {
            IEnumerable<Usuario> usuarios = Repo.FindAll();
            IEnumerable<UsuarioDTO> dtos = UsuarioMapper.ToListDTO(usuarios);
            return dtos;
        }
    }
}
