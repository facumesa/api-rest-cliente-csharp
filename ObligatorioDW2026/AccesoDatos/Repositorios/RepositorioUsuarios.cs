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
        //Cambiar repo a BD y probar
        private static int ultId = 0;
        private static List<Usuario> usuarios = new List<Usuario>();
        public RepositorioUsuarios()
        {
            if (usuarios.Count == 0)
            {
                this.PrecargarUsuarios(); 
            }
        }
        public void Add(Usuario nuevo)
        {
            nuevo.Validar();
            nuevo.Id = ultId++;
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

        public void PrecargarUsuarios()
        {
            Administrador admin = new Administrador(
                    "Facundo Mesa",
                    "Ciudad de la Costa",
                    "099123456",
                    new Email("admin@ort.edu.uy"),
                    "admin",
                    new Password("Admin123!")
                );
            Socio socio = new Socio(
                    "Oriana Rodriguez",
                    "Montevideo",
                    "098765432",
                    new Email("ori@gmail.com"),
                    "ori_socia",
                    new Password("Socio123!")
                );
            Socio socio2 = new Socio(
                    "Juan Perez",
                    "Piriápolis",
                    "091111222",
                    new Email("juan@perez.com"),
                    "juanp",
                    new Password("Juanp123!")
                );
            this.Add(admin);
            this.Add(socio);
            this.Add(socio2);
        }
    }
}
