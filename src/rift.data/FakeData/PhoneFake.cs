using System.Collections.Generic;
using Bogus;
using rift.domain;
using rift.domain.Enum;

namespace rift.data.FakeData
{
    public class PhoneFake : FakeData<Phone>
    {
        public override Phone Generate()
        {
            var faker = new Faker<Phone>()
                 .RuleFor(x => x.Id, x => x.Random.Int(0, 5000))
                .RuleFor(x => x.DDD, x => x.Random.Int(3).ToString())
                .RuleFor(x => x.Number, x => x.Phone.PhoneNumber("#########"))
                .RuleFor(x => x.Type, x => x.PickRandom<PhoneType>());

            return faker.Generate();
        }

        public override IList<Phone> GenerateList(int size)
        {
            var list = new List<Phone>();
            for (int i = 0; i < size; i++)
            {
                list.Add(Generate());
            }
            return list;
        }
    }
}
