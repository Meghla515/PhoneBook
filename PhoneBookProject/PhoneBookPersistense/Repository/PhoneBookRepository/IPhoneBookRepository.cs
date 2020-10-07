using PhoneBookPersistense.Model;
using PhoneBookPersistense.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBookPersistense.Repository.PhoneBookRepository
{
    public interface IPhoneBookRepository : IRepository<PhoneBook>
    {
    }
}
