using System;
namespace BikeDistributor
{
    public interface ITaxEngine
    {
        decimal GetTaxRate(IAddress destinationAddress);
    }
}
