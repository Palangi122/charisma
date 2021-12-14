using DomainModel.Others;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel.Customers.dto
{
    public class CustomerDto
    {
        public Guid CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Address> Addresses { get; set; }
    }
}
