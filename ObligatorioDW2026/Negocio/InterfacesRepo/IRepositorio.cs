using Negocio.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio.InterfacesRepo
{
    // Add, Remove, Update, FindAll, FindbyId
    public interface IRepositorio<T>
    {
        void Add(T nuevo);
        void Remove(int id);
        void Update(T nuevo);
        T FindById(int id);
        IEnumerable<T> FindAll();
        
    }
}