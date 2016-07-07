using System;
namespace BikeDistributor
{
    public interface IAddress
    {
        string AddressLine1 { get; set; }
        string AddressLine2 { get; set; }
        string City { get; set; }
        string Country { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string PostalCode { get; set; }
        string StateProvince { get; set; }
    }
}
