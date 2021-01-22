using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace rift.domain
{
    public class Company : BaseEntity
    {
        [Required]
        public string CNPJ { get; set; }
        public string FantasyName { get; set; }
        [Required]
        public string CompanyName { get; set; }
        public IList<Phone> Phones { get; set; }
        public Email Email { get; set; }
        public Address Address { get; set; }

        public Company() : base() { }
    }
}
