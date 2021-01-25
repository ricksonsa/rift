using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using rift.domain;
using rift.domain.Models;
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

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Person>))]
        public async Task<IActionResult> Get([FromQuery] PersonSearchModel searchModel)
        {
            var people = await _peopleRepository.FindManyAsync(
              new string[] { "Emails", "Address", "Phones" },
              searchModel.GetSearchExpressions());

            return Ok(people);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Person))]
        public async Task<IActionResult> GetOne(int id)
        {
            return Ok(await _peopleRepository.FindByIdAsync(id, "Emails", "Address", "Phones"));
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Person))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create(Person person)
        {
            return Ok(await _peopleRepository.SaveAsync(person));
        }

        [HttpPut]
        [ProducesResponseType(200, Type = typeof(Person))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Update(Person person)
        {
            return Ok(await _peopleRepository.UpdateAsync(person));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(Person))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            var person = await _peopleRepository.FindByIdAsync(id, "Emails", "Address", "Phones");
            if (person == null) return NotFound();
            return Ok(await _peopleRepository.DeleteAsync(person));
        }
    }
}
