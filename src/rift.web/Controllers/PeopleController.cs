using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<Person> Get(string cpf, string name, string document)
        {
            var person = await _peopleRepository.FindByAsync(
              new string[] { "Emails", "Address", "Phones" },
              x => !string.IsNullOrEmpty(cpf) && x.CPF.ToLower().Contains(cpf.ToLower()),
              x => !string.IsNullOrEmpty(name) && x.Name.ToLower().Contains(name.ToLower()),
              x => !string.IsNullOrEmpty(document) && x.Document.ToLower().Contains(document.ToLower()));

            return person;
        }

        [HttpGet("{id}")]
        public async Task<Person> GetOne(int id)
        {
            return await _peopleRepository.FindByIdAsync(id, "Emails", "Address", "Phones");
        }

        [HttpGet()]
        public async Task<List<Person>> GetPeople()
        {
            return (await _peopleRepository.FindManyAsync("Emails", "Address", "Phones")).ToList();
        }

        [HttpPost]
        public async Task<Person> Create(Person person)
        {
            return await _peopleRepository.SaveAsync(person);
        }

        [HttpPut]
        public async Task<Person> Update(Person person)
        {
            return await _peopleRepository.UpdateAsync(person);
        }

        [HttpDelete("{id}")]
        public async Task<Person> Delete(int id)
        {
            var person = await _peopleRepository.FindByIdAsync(id, "Emails", "Address", "Phones");
            return await _peopleRepository.DeleteAsync(person);
        }
    }
}
