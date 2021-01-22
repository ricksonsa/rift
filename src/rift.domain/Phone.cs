using System.Collections.Generic;
using System.Text.Json.Serialization;
using rift.domain.Enum;

namespace rift.domain
{
    public class Phone : BaseEntity
    {
        public string DDD { get; set; }
        public string Number { get; set; }

        public PhoneType Type { get; set; }

        [JsonIgnore]
        public IList<Person> People { get; set; }
        [JsonIgnore]
        public IList<Company> Companies { get; set; }

        public Phone() : base() { }
    }
}
