using System.Collections.Generic;
using Bogus;

namespace rift.data.FakeData
{
    public class PersonFake : FakeData<domain.Person>
    {
        public override domain.Person Generate()
        {
            var faker = new Faker<domain.Person>()
                .RuleFor(x => x.Emails, x => new EmailFake().GenerateList(2))
                .RuleFor(x => x.Document, x => x.Random.String2(10))
                .RuleFor(x => x.CPF, x => x.Random.String2(11))
                .RuleFor(x => x.Id, x =>  x.Random.Int(0, 5000))
                .RuleFor(x => x.Name, x => x.Person.FullName)
                .RuleFor(x => x.Address, x => new AddressFake().Generate())
                .RuleFor(x => x.BirthDate, x => x.Person.DateOfBirth);

                return faker.Generate();
        }

        public override IList<domain.Person> GenerateList(int size)
        {
            var list = new List<domain.Person>();
            for (int i = 0; i < size; i++)
            {
                list.Add(Generate());
            }
            return list;
        }
    }
}
