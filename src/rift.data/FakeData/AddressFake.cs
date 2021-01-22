using System;
using System.Collections.Generic;
using Bogus;
using rift.domain;
namespace rift.data.FakeData
{
    public class AddressFake : FakeData<Address>
    {
        public override Address Generate()
        {
            var faker = new Faker<Address>()
                 .RuleFor(x => x.Id, x => x.Random.Int(0, 5000))
                .RuleFor(x => x.Number, x => x.Phone.PhoneNumber("###"))
                .RuleFor(x => x.PostalCode, x => x.Address.CountryCode())
                .RuleFor(x => x.State, x => x.Address.State())
                .RuleFor(x => x.Street, x => x.Address.StreetName())
                .RuleFor(x => x.Region, x => x.Address.StreetName());

            return faker.Generate();
        }

        public override IList<Address> GenerateList(int size)
        {
            var list = new List<Address>();
            for (int i = 0; i < size; i++)
            {
                list.Add(Generate());
            }
            return list;
        }
    }
}
