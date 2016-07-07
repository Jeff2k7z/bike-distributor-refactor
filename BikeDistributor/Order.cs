using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BikeDistributor
{
    /// <summary>
    /// Represents a Sales Order.
    /// </summary>
    public class Order
    {
        private ITaxEngine _taxEngine;
        private IList<Line> _lines = new List<Line>();
        public Customer Customer { get; private set; }
        public IAddress ShippingAddress { get; set; }
        public IList<Line> Lines { get { return _lines; } }
        public IList<Promotion> Promotions { get; set; }

        public decimal MerchandiseTotal { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxTotal { get; set; }
        public decimal OrderTotal { get; set; }

        /// <summary>
        /// Represents a Sales Order.
        /// </summary>
        /// <param name="customer">The customer this order belongs to.</param>
        /// <param name="taxEngine">The tax engine to be used to calculate tax rates.</param>
        public Order(Customer customer, ITaxEngine taxEngine)
        {
            Customer = customer;
            _taxEngine = taxEngine;
        }

        public void AddLine(Line line)
        {
            _lines.Add(line);
            CalculateOrderTotals();
        }

        public void CalculateOrderTotals()
        {
            // Calculate line level discounts
            foreach(var line in _lines)
            {
                if (Promotions != null)
                {
                    // Only look at line level promotions
                    foreach (var promo in Promotions.Where(p => p.IsLineLevel == true))
                    {
                        // Match on both price and quantity ranges
                        if (line.RetailPrice >= (promo.MinimumValue == null ? decimal.MinValue : promo.MinimumValue) && line.RetailPrice <= (promo.MaximumValue == null ? decimal.MaxValue : promo.MaximumValue))
                        {
                            if (line.Quantity >= (promo.MiniumumQuantity == null ? int.MinValue : promo.MiniumumQuantity) && line.Quantity <= (promo.MaximumQuantity == null ? int.MaxValue : promo.MiniumumQuantity))
                            {
                                if (promo.PercentageDiscount != null)
                                {
                                    line.DiscountedPrice = line.DiscountedPrice - (line.DiscountedPrice * promo.PercentageDiscount.Value);
                                }
                                else if (promo.DollarDiscount != null)
                                {
                                    line.DiscountedPrice -= promo.DollarDiscount.Value;
                                }
                            }
                        }
                    }
                }
            }

            // Re-calculate the SubTotal
            SubTotal = _lines.Sum(p => p.ExtendedPrice);

            // We'll use the pre-discounted line total to determine if this order
            // qualifies for promotions
            var preDiscountLineTotal = _lines.Sum(p => p.Quantity * p.RetailPrice);

            // Calculate order level discounts
            if (Promotions != null)
            {
                // Only evaluate order level promotions
                foreach (var promo in Promotions.Where(p => p.IsLineLevel == false))
                {
                    // If the order qualifies for this promotion...
                    if (preDiscountLineTotal >= (promo.MinimumValue == null ? decimal.MinValue : promo.MinimumValue) && preDiscountLineTotal <= (promo.MaximumValue == null ? decimal.MaxValue : promo.MaximumValue))
                    {
                        if (promo.PercentageDiscount != null)
                        {
                            SubTotal *= promo.PercentageDiscount.Value;
                        }
                        else if (promo.DollarDiscount != null)
                        {
                            SubTotal -= promo.DollarDiscount.Value;
                        }
                    }
                }
            }

            // Calculate Tax
            TaxTotal = SubTotal * _taxEngine.GetTaxRate(ShippingAddress);

            // Calculate Order Total
            OrderTotal = SubTotal + TaxTotal;
        }


    }
}
