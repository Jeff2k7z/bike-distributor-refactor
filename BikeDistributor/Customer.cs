using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeDistributor
{
    /// <summary>
    /// Represents a Customer.
    /// </summary>
    public class Customer
    {
        public string CompanyName { get; set; }
        public Address ShippingAddress { get; set; }
        public Address BillingAddress { get; set; }

        public Customer(string companyName)
        {
            CompanyName = companyName;
        }
    }
}
