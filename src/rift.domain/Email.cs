using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace rift.domain
{
    public class Email : BaseEntity
    {
        public string Mail { get; set; }
        [JsonIgnore]
        public IList<Person> People { get; set; }

        public Email() : base() { }
    }
}
