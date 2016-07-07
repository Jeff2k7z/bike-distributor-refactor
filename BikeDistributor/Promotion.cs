using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeDistributor
{
    /// <summary>
    /// Represents a sales promotion.
    /// Can be either a percentage discount or dollar discount.
    /// Can apply to individual lines or the entire order.
    /// Can be defined by range of price and/or range of quantity.
    /// </summary>
    public class Promotion
    {
        public string PromotionName { get; set; }
        public bool IsLineLevel { get; set; }
        public decimal? MinimumValue { get; set; }
        public decimal? MaximumValue { get; set; }
        public int? MiniumumQuantity { get; set; }
        public int? MaximumQuantity { get; set; }
        public decimal? PercentageDiscount { get; set; }
        public decimal? DollarDiscount { get; set; }

        public Promotion(string promotionName, bool isLineLevel, decimal? minimumValue, decimal? maximumValue, int? minimumQuantity, int? maximumQantity, decimal? percentageDiscount, decimal? dollarDiscount)
        {
            PromotionName = promotionName;
            IsLineLevel = isLineLevel;
            MinimumValue = minimumValue;
            MaximumValue = maximumValue;
            MiniumumQuantity = minimumQuantity;
            MaximumQuantity = maximumQantity;
            PercentageDiscount = percentageDiscount;
            DollarDiscount = dollarDiscount;
        }

    }
}
