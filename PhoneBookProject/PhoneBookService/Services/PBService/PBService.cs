using Newtonsoft.Json;
using PhoneBookPersistense.Model;
using PhoneBookPersistense.Repository.PhoneBookRepository;
using PhoneBookService.DataTransfer.Mapper;
using PhoneBookService.DataTransfer.Model;
using PhoneBookService.Services.MessageService;
using PhoneBookService.Services.PBService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace PhoneBookService.Services.PhoneBookService
{
    public class PBService : IPBService
    {
        private IPhoneBookRepository repo;
        private IProducer producer;

        public PBService(IPhoneBookRepository repo, IProducer producer)
        {
            this.repo = repo;
            this.producer = producer;
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

            KafkaMessage message = new KafkaMessage("phonebookDeleted", JsonConvert.SerializeObject(book.ToDTO()));
            producer.PublishAsync("phonebook-incoming", message);
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

            KafkaMessage message = new KafkaMessage("phonebookCreated", JsonConvert.SerializeObject(entity.ToDTO()));
            producer.PublishAsync("phonebook-incoming", message);

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

            KafkaMessage message = new KafkaMessage("phonebookUpdated", JsonConvert.SerializeObject(phoneBook.ToDTO()));
            producer.PublishAsync("phonebook-incoming", message);
        }
    }
}
