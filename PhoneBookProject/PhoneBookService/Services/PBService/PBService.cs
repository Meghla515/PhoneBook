using PhoneBookPersistense.Model;
using PhoneBookPersistense.Repository.PhoneBookRepository;
using PhoneBookService.DataTransfer.Mapper;
using PhoneBookService.DataTransfer.Model;
using PhoneBookService.Services.PBService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhoneBookService.Services.PhoneBookService
{
    public class PBService : IPBService
    {
        private IPhoneBookRepository repo;

        public PBService(IPhoneBookRepository repo)
        {
            this.repo = repo;
        }

        public IEnumerable<PhoneBookDTO> GetEntries()
        {
            return repo.FindAll().Select(x => x.ToDTO());
        }

        public PhoneBookDTO GetEntryById(int entryId)
        {
            var book = repo.FindByID(entryId);
            if (book == null)
            {
                throw new Exception("not found");
            }
            return repo.FindByID(entryId).ToDTO();
        }

        public void RemoveEntry(int entryId)
        {
            var book = repo.FindByID(entryId);
            if (book == null)
            {
                throw new Exception("not found");
            }
            repo.Remove(entryId);
        }

        public PhoneBookDTO SaveEntry(PhoneBookDTO dto)
        {
            PhoneBook phoneBook = dto.ToEntity();
            if (phoneBook.id != 0)
            {
                var book = repo.FindByID(phoneBook.id);
                if (book != null)
                {
                    repo.Update(phoneBook);
                    return phoneBook.ToDTO();
                }
            }
            var entity = repo.Add(phoneBook);

            return entity.ToDTO();
        }

        public void UpdateEntry(PhoneBookDTO dto)
        {
            var phoneBook = repo.FindByID(dto.id);
            if (phoneBook == null)
            {
                throw new Exception("not found");
            }
            dto.ToEntity(phoneBook);
            repo.Update(phoneBook);
        }
    }
}
