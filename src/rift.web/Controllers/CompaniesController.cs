using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        public Company GetOneById(int id)
        {
            return _companiesRepository.FindByIdAsync(id, "Phones", "Address", "Email").Result;
        }


        [HttpGet("search")]
        public Company Get(string cnpj, string companyName, string fantasyName)
        {
            var company = _companiesRepository.FindByAsync(
                new string[] { "Phones", "Address", "Email" },
                x => !string.IsNullOrEmpty(cnpj) && x.CNPJ.ToLower().Contains(cnpj.ToLower()),
                x => !string.IsNullOrEmpty(fantasyName) && x.FantasyName.ToLower().Contains(fantasyName.ToLower()),
                x => !string.IsNullOrEmpty(companyName) && x.CompanyName.ToLower().Contains(companyName.ToLower()))
                .Result;

            return company;
        }

        [HttpGet]
        public List<Company> Get()
        {
            var companies = _companiesRepository.FindManyAsync("Phones", "Address", "Email").Result.AsQueryable();
            return companies.ToList();
        }

        [HttpPost]
        public async Task<Company> Create(Company company)
        {
            return await _companiesRepository.SaveAsync(company);
        }

        [HttpPut]
        public Company Update(Company company)
        {
            return _companiesRepository.UpdateAsync(company).Result;
        }

        [HttpDelete("{id}")]
        public Company Delete(int id)
        {
            var company = _companiesRepository.FindByIdAsync(id, new string[] { "Phones", "Address", "Email" }).Result;
            return _companiesRepository.DeleteAsync(company).Result;
        }
    }
}
