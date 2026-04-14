using System;
using System.Collections.Generic;
using System.Text;
using Negocio.Dominio;
using Negocio.InterfacesRepo;

namespace AccesoDatos.Repositorios
{
    public class RepositorioUsuarios : IRepositorioUsuarios
    {
        private static List<Usuario> usuarios = new List<Usuario>();

        public void Add(Usuario nuevo)
        {
            nuevo.Validar();
            usuarios.Add(nuevo);
        }

        public IEnumerable<Usuario> FindAll()
        {
            return usuarios;
        }

        public Usuario FindById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Socio> GetSocios()
        {
            List<Socio> socios = new List<Socio>();
            foreach (Usuario u in usuarios)
            {
                if (u is Socio s)
                {
                    socios.Add(s);
                }
            }
            return socios;
        }

        public Usuario Login(string nombreUsuario, string password)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Usuario nuevo)
        {
            throw new NotImplementedException();
        }
    }
}
