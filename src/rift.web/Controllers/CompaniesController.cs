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
    public class CompaniesController : ControllerBase
    {
        private readonly IRepository<Company> _companiesRepository;

        public CompaniesController(IRepository<Company> companiesRepository)
        {
            _companiesRepository = companiesRepository;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Company))]
        public async Task<IActionResult> GetOneById(int id)
        {
            return Ok(await _companiesRepository.FindByIdAsync(id, "Phones", "Address", "Email"));
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Company>))]
        public async Task<IActionResult> Get([FromQuery] CompanySearchModel search)
        {
            var companies = await _companiesRepository.FindManyAsync(
                new string[] { "Phones", "Address", "Email" },
                search.GetSearchExpressions());

            return Ok(companies);
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Company))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create(Company company)
        {
            return Ok(await _companiesRepository.SaveAsync(company));
        }

        [HttpPut]
        [ProducesResponseType(200, Type = typeof(Company))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Update(Company company)
        {
            return Ok(await _companiesRepository.UpdateAsync(company));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(Company))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            var company = _companiesRepository.FindByIdAsync(id, "Phones", "Address", "Email").Result;
            if (company == null) return NotFound();
            return Ok(await _companiesRepository.DeleteAsync(company));
        }
    }
}
