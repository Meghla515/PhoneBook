using PhoneBookApi.Model;
using System;
using System.ComponentModel.DataAnnotations;

namespace PhoneBookApi
{
    public class PhoneBook : BaseEntity
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public string phonenumber { get; set; }
    }
}
