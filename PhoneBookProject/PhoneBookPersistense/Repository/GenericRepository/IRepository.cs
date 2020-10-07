using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBookPersistense.Repository.GenericRepository
{
    public interface IRepository<T> where T : class
    {
        T Add(T item);
        void Remove(int id);
        T Update(T item);
        T FindByID(int id);
        IEnumerable<T> FindAll();
    }
}
