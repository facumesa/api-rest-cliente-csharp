using System;
using System.Collections.Generic;
using System.Text;
using Aplicacion.Mappers;
using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Negocio.Dominio;
using Negocio.InterfacesRepo;

namespace Aplicacion.CasosDeUso
{
    public class CULoginUsuarios : ILoginUsuarios
    {
        public IRepositorioUsuarios Repo { get; set; }

        public CULoginUsuarios(IRepositorioUsuarios repo)
        {
            Repo = repo;
        }

        public UsuarioDTO Ejecutar(string nombreUsuario, string password)
        {
            Usuario u = Repo.Login(nombreUsuario, password);
            UsuarioDTO dto = UsuarioMapper.ToDto(u);
            return dto;  
        }
    }
}
