using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using rift.domain;
using rift.domain.Models;
using rift.interfaces.Repository;
using rift.web.Rest.Filters;
using rift.web.Rest.Utils;
using rift.Web.Extensions;
using ritf.Web.Rest.Utilities;

namespace rift.web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeopleController : ControllerBase
    {
        private const string EntityName = "person";
        private readonly IRepository<Person> _peopleRepository;

        public PeopleController(IRepository<Person> peopleRepository)
        {
            _peopleRepository = peopleRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Person>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get([FromQuery] PersonSearchModel searchModel)
        {
            var people = await _peopleRepository.FindManyAsync(
              new string[] { "Emails", "Address", "Phones" },
              searchModel.GetSearchExpressions());

            return ActionResultUtil.WrapOrNotFound(people);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Person))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetPerson(int id)
        {
            var person = await _peopleRepository.FindByIdAsync(id, "Emails", "Address", "Phones");
            return ActionResultUtil.WrapOrNotFound(person);
        }

        [HttpPost]
        [ValidateModel]
        [ProducesResponseType(200, Type = typeof(Person))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create(Person person)
        {
            var result = await _peopleRepository.SaveAsync(person);

            return CreatedAtAction(nameof(GetPerson), new { id = result.Id }, result)
               .WithHeaders(HeaderUtil.CreateEntityCreationAlert(EntityName, result.Id.ToString()));
        }

        [HttpPut]
        [ValidateModel]
        [ProducesResponseType(200, Type = typeof(Person))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(Person person)
        {
            if (!await _peopleRepository.Exists(x => x.Id == person.Id)) return NotFound();

            var result = await _peopleRepository.UpdateAsync(person);

              return Ok(result)
                    .WithHeaders(HeaderUtil.CreateEntityUpdateAlert(EntityName, result.Id.ToString()));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(Person))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            var person = await _peopleRepository.FindByIdAsync(id, "Emails", "Address", "Phones");
            if (person == null) return NotFound();
            return Ok(await _peopleRepository.DeleteAsync(person))
                    .WithHeaders(HeaderUtil.CreateEntityDeletionAlert(EntityName, person.Id.ToString()));
        }
    }
}
