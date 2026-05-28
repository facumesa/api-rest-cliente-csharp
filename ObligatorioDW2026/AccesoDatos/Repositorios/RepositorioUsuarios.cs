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
            return Contexto.Socios.ToList();
        }

        public Usuario Login(string nombreUsuario, string password)
        {
            return Contexto.Usuarios
                .FirstOrDefault(u => u.NombreUsuario == nombreUsuario && u.Contrasenia.Valor == password);
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Usuario nuevo)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Socio> ObtenerSociosConPrestamosActivos()
        {
            return Contexto.Prestamos
                               .Where(p => p.Estado == EstadoPrestamo.PRESTADO)
                               .Select(p => p.Socio)
                               .Distinct()
                               .ToList();
        }
    }
}
