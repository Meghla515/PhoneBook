using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhoneBookPersistense.Model
{
    public class BaseEntity
    {
        [Key]
        public int id { get; set; }
    }
}
