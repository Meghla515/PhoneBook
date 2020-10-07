using Microsoft.Extensions.Configuration;
using PhoneBookPersistense.Model;
using PhoneBookPersistense.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBookPersistense.Repository.PhoneBookRepository
{
    public class PhoneBookRepository : Repository<PhoneBook>, IPhoneBookRepository
    {
        public PhoneBookRepository(IConfiguration configuration) : base(configuration)
        {

        }
    }
}
