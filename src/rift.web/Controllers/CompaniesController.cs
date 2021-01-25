using System.Collections.Generic;
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
    public class CompaniesController : ControllerBase
    {
        private const string EntityName = "company";
        private readonly IRepository<Company> _companiesRepository;

        public CompaniesController(IRepository<Company> companiesRepository)
        {
            _companiesRepository = companiesRepository;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Company))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCompany(int id)
        {
            var company = await _companiesRepository.FindByIdAsync(id, "Phones", "Address", "Email");
            return ActionResultUtil.WrapOrNotFound(company);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Company>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get([FromQuery] CompanySearchModel search)
        {
            var companies = await _companiesRepository.FindManyAsync(
                new string[] { "Phones", "Address", "Email" },
                search.GetSearchExpressions());

            return ActionResultUtil.WrapOrNotFound(companies);
        }

        [HttpPost]
        [ValidateModel]
        [ProducesResponseType(201, Type = typeof(Company))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create(Company company)
        {
            var result = await _companiesRepository.SaveAsync(company);
            return CreatedAtAction(nameof(GetCompany), new { id = result.Id }, result)
               .WithHeaders(HeaderUtil.CreateEntityCreationAlert(EntityName, result.Id.ToString()));
        }

        [HttpPut]
        [ValidateModel]
        [ProducesResponseType(200, Type = typeof(Company))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(Company company)
        {
            if (!await _companiesRepository.Exists(x => x.Id == company.Id)) return NotFound();

            var result = await _companiesRepository.UpdateAsync(company);
            return Ok(result)
                    .WithHeaders(HeaderUtil.CreateEntityUpdateAlert(EntityName, result.Id.ToString()));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(Company))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            var company = _companiesRepository.FindByIdAsync(id, "Phones", "Address", "Email").Result;
            if (company == null) return NotFound();
            var result = await _companiesRepository.UpdateAsync(company);
            return Ok(await _companiesRepository.DeleteAsync(company))
                    .WithHeaders(HeaderUtil.CreateEntityDeletionAlert(EntityName, result.Id.ToString()));
        }
    }
}
