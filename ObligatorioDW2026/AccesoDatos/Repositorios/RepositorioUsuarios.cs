using System;
using System.Collections.Generic;
using System.Text;
using Negocio.Dominio;
using Negocio.InterfacesRepo;
using Negocio.ValueObjects;

namespace AccesoDatos.Repositorios
{
    public class RepositorioUsuarios : IRepositorioUsuarios
    {
        private static List<Usuario> usuarios = new List<Usuario>()
        {
            new Administrador(
                "Facundo Mesa",
                "Ciudad de la Costa",
                "099123456",
                new Email("admin@ort.edu.uy"),
                "admin",
                new Password("Admin123!")
            ),
            new Socio(
                "Oriana Rodriguez",
                "Montevideo",
                "098765432",
                new Email("ori@gmail.com"),
                "ori_socia",
                new Password("Socio123!")
            ),
            new Socio(
                "Juan Perez",
                "Piriápolis",
                "091111222",
                new Email("juan@perez.com"),
                "juanp",
                new Password("Juanp123!")
            )

        };

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
            foreach (var usuario in usuarios)
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
