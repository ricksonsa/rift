using System;
using System.Collections.Generic;
using Bogus;
using rift.domain;

namespace rift.data.FakeData
{
    public class EmailFake : FakeData<Email>
    {
        public override Email Generate()
        {
            var fake = new Faker<Email>()
                 .RuleFor(x => x.Id, x => x.Random.Int(0, 5000))
                .RuleFor(x => x.Mail, x => x.Person.Email);

            return fake.Generate();
        }

        public override IList<Email> GenerateList(int size)
        {
            var list = new List<Email>();
            for (int i = 0; i < size; i++)
            {
                list.Add(Generate());
            }
            return list;
        }
    }
}
