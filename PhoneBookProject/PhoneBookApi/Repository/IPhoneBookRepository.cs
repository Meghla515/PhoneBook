using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBookApi.Repository
{
    public interface IPhoneBookRepository
    {
        PhoneBook Save(PhoneBook phonebook);
        List<PhoneBook> Gets();
        PhoneBook Get();
        string Delete(int pbid);
    }
}
