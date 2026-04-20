using System;
using System.Collections.Generic;
using System.Text;
using AccesoDatos.EF;
using Negocio.Dominio;
using Negocio.InterfacesRepo;
using Negocio.ValueObjects;

namespace AccesoDatos.Repositorios
{
    public class RepositorioUsuarios : IRepositorioUsuarios
    {
        public StellarContext Contexto { get; set; }

        public RepositorioUsuarios(StellarContext contexto)
        {
            Contexto = contexto;
        }

        public void Add(Usuario nuevo)
        {
            nuevo.Validar();
            Contexto.Usuarios.Add(nuevo);
            Contexto.SaveChanges();
        }

        public IEnumerable<Usuario> FindAll()
        {
            return Contexto.Usuarios.ToList();
        }

        public Usuario FindById(int id)
        {
            return Contexto.Usuarios.Find(id);
        }

        public IEnumerable<Socio> GetSocios()
        {
            //SOLO MODO DE PRUEBA, SE REMPLAZARA CON LINQ
            List<Socio> socios = new List<Socio>();
            foreach (Usuario u in Contexto.Usuarios.ToList())
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
            //SOLO MODO DE PRUEBA, SE REMPLAZARA CON LINQ
            foreach (var usuario in Contexto.Usuarios.ToList())
            {
                if (usuario.NombreUsuario == nombreUsuario && usuario.Contrasenia.Valor == password) return usuario;
            }

            return null;
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
