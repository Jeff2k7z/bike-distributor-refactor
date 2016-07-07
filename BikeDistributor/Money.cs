using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeDistributor
{
    /// <summary>
    /// Represents a
    /// </summary>
    public class Money
    {
        public decimal Amount { get; private set; }
        public Currency Currency { get; private set; }

        public Money(decimal amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }
    }

    /// <summary>
    /// Enumeration of ISO 4217 currency codes
    /// </summary>
    public enum Currency 
    {
        USD,
        CAD,
        EUR,
        AUD

        // TODO: Add remaining codes
    }
}
