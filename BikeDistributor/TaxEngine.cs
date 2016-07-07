using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeDistributor
{
    /// <summary>
    /// Represents a simple tax engine.
    /// </summary>
    public class TaxEngine : BikeDistributor.ITaxEngine
    {
        public TaxEngine()
        {
        }

        /// <summary>
        /// Calculates tax rate based on shipping destination
        /// </summary>
        public decimal GetTaxRate(IAddress destinationAddress)
        {
            // TODO: Integrate with 3rd party tax engine
            return .0725m;
        }
    }
}
