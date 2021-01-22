using System.Collections.Generic;
using System.Linq;
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
                x => !string.IsNullOrEmpty(cnpj) && x.CNPJ.Contains(cnpj),
                x => !string.IsNullOrEmpty(fantasyName) && x.FantasyName.Contains(fantasyName),
                x => !string.IsNullOrEmpty(companyName) && x.CompanyName.Contains(companyName))
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
        public Company Create(Company company)
        {
            return _companiesRepository.SaveAsync(company).Result;
        }

        [HttpPut]
        public Company Update(Company company)
        {
            return _companiesRepository.SaveAsync(company).Result;
        }

        [HttpDelete("{id}")]
        public Company Delete(int id)
        {
            var company = _companiesRepository.FindByIdAsync(id, new string[] { "Phones", "Address", "Email" }).Result;
            return _companiesRepository.DeleteAsync(company).Result;
        }
    }
}
