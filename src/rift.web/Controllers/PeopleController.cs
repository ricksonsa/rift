using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using rift.domain;
using rift.interfaces.Repository;

namespace rift.web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeopleController : ControllerBase
    {
        private readonly IRepository<Person> _peopleRepository;

        public PeopleController(IRepository<Person> peopleRepository)
        {
            _peopleRepository = peopleRepository;
        }

        [HttpGet("search")]
        public Person Get(string cpf, string name, string document)
        {
            var person = _peopleRepository.FindByAsync(
              new string[] { "Emails", "Address" },
              x => !string.IsNullOrEmpty(cpf) && x.CPF.Contains(cpf),
              x => !string.IsNullOrEmpty(name) && x.Name.Contains(name),
              x => !string.IsNullOrEmpty(document) && x.Document.Contains(document))
              .Result;

            return person;
        }

        [HttpGet("{id}")]
        public Person GetOne(int id)
        {
            return _peopleRepository.FindByIdAsync(id, "Emails", "Address").Result;
        }

        [HttpGet()]
        public List<Person> GetPeople()
        {
            return _peopleRepository.FindManyAsync("Emails", "Address").Result.ToList();
        }

        [HttpPost]
        public Person Create(Person person)
        {
            return _peopleRepository.SaveAsync(person).Result;
        }

        [HttpPut]
        public Person Update(Person person)
        {
            return _peopleRepository.SaveAsync(person).Result;
        }

        [HttpDelete("{id}")]
        public Person Delete(int id)
        {
            var person = _peopleRepository.FindByIdAsync(id, "Emails", "Address").Result;
            return _peopleRepository.DeleteAsync(person).Result;
        }
    }
}
