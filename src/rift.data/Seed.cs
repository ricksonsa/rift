using System;
using rift.data.FakeData;

namespace rift.data
{
    public class Seed
    {
        private readonly AcervoContext _acervoContext;

        public Seed(AcervoContext context)
        {
            _acervoContext = context;
        }

        public void SeedPeople()
        {
            var people = new PersonFake().GenerateList(10);
            _acervoContext.People.AddRange(people);
            _acervoContext.SaveChanges();
        }

        public void SeedCompanies()
        {
            var companies = new CompanyFake().GenerateList(10);
            _acervoContext.Companies.AddRange(companies);
            _acervoContext.SaveChanges();
        }
    }
}
