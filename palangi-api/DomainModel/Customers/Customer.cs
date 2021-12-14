using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel.Customers
{
  public  class Customer
    {
        public Guid CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Addresses { get; set; }                                                       
    }

}
