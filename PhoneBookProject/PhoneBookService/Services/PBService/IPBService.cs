using PhoneBookService.DataTransfer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBookService.Services.PBService
{
    public interface IPBService
    {
        PhoneBookDTO SaveEntry(PhoneBookDTO dto);
        void RemoveEntry(int entryId);
        IEnumerable<PhoneBookDTO> GetEntries();
        PhoneBookDTO GetEntryById(int entryId);
        void UpdateEntry(PhoneBookDTO dto);
    }
}
