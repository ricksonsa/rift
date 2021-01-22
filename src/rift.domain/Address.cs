using System;
namespace rift.domain
{
    public class Address : BaseEntity
    {
        public string Street { get; set; }
        public string Number { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }
        public Address() : base() { }
    }
}
