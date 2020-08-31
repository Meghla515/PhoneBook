using PhoneBookApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBookApi.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        PhoneBook Add(T item);
        void Remove(int id);
        PhoneBook Update(T item);
        T FindByID(int id);
        IEnumerable<T> FindAll();
    }
}
