using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhoneBookPersistense.Model
{
    public class PhoneBook : BaseEntity
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string phonenumber { get; set; }
    }
}
