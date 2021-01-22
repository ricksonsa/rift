using System;
using System.Collections.Generic;
using rift.domain;
using Bogus;

namespace rift.data.FakeData
{
    public class CompanyFake : FakeData<Company>
    {
        public override Company Generate()
        {
            var fake = new Faker<Company>()
                .RuleFor(x => x.CNPJ, x => x.Random.String(13))
                .RuleFor(x => x.CompanyName, x => x.Company.CompanyName())
                .RuleFor(x => x.CreatedDate, x => x.Date.Past(2))
                .RuleFor(x => x.FantasyName, x => x.Company.CompanyName())
                .RuleFor(x => x.Address, x => new AddressFake().Generate())
                .RuleFor(x => x.Email, x => new EmailFake().Generate())
                .RuleFor(x => x.Phones, x => new PhoneFake().GenerateList(1))
                .RuleFor(x => x.Id, x => x.Random.Int(0, 5000));

            return fake.Generate();
        }

        public override IList<Company> GenerateList(int size)
        {
            var list = new List<Company>();
            for (int i = 0; i < size; i++)
            {
                list.Add(Generate());
            }
            return list;
        }
    }
}
