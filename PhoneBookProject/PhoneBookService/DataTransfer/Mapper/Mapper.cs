using PhoneBookPersistense.Model;
using PhoneBookService.DataTransfer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBookService.DataTransfer.Mapper
{
    public static class Mapper
    {
        public static PhoneBook ToEntity(this PhoneBookDTO dto)
        {
            return new PhoneBook()
            {
                id = dto.id,
                username = dto.username,
                phonenumber = dto.phonenumber
            };
        }

        public static void ToEntity(this PhoneBookDTO dto, PhoneBook pb)
        {
            pb.username = dto.username;
            pb.phonenumber = dto.phonenumber;
        }

        public static PhoneBookDTO ToDTO(this PhoneBook pb)
        {
            return new PhoneBookDTO()
            {
                id = pb.id,
                username = pb.username,
                phonenumber = pb.phonenumber
            };
        }
    }
}
