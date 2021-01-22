using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace rift.domain
{
    public class Person : BaseEntity
    {
        [Required]
        public string CPF { get; set; }
        [Required]
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Document { get; set; }
        public IList<Email> Emails { get; set; }
        public IList<Phone> Phones { get; set; }
        public Address Address { get; set; }

        public Person() : base() { }
    }
}
