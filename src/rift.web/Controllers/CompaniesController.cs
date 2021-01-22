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
    public class CompaniesController : ControllerBase
    {
        private readonly IRepository<Company> _companiesRepository;

        public CompaniesController(IRepository<Company> companiesRepository)
        {
            _companiesRepository = companiesRepository;
        }

        [HttpGet("{id}")]
        public async Task<Company> GetOneById(int id)
        {
            return await _companiesRepository.FindByIdAsync(id, "Phones", "Address", "Email");
        }


        [HttpGet("search")]
        public async Task<Company> Get(string cnpj, string companyName, string fantasyName)
        {
            var company = await _companiesRepository.FindByAsync(
                new string[] { "Phones", "Address", "Email" },
                x => !string.IsNullOrEmpty(cnpj) && x.CNPJ.ToLower().Contains(cnpj.ToLower()),
                x => !string.IsNullOrEmpty(fantasyName) && x.FantasyName.ToLower().Contains(fantasyName.ToLower()),
                x => !string.IsNullOrEmpty(companyName) && x.CompanyName.ToLower().Contains(companyName.ToLower()));

            return company;
        }

        [HttpGet]
        public async Task<List<Company>> Get()
        {
            return (await _companiesRepository.FindManyAsync("Phones", "Address", "Email")).ToList();
        }

        [HttpPost]
        public async Task<Company> Create(Company company)
        {
            return await _companiesRepository.SaveAsync(company);
        }

        [HttpPut]
        public async Task<Company> Update(Company company)
        {
            return await _companiesRepository.UpdateAsync(company);
        }

        [HttpDelete("{id}")]
        public async Task<Company> Delete(int id)
        {
            var company = _companiesRepository.FindByIdAsync(id, "Phones", "Address", "Email").Result;
            return await _companiesRepository.DeleteAsync(company);
        }
    }
}
