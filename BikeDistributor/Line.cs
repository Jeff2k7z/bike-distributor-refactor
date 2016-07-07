namespace BikeDistributor
{
    /// <summary>
    /// Represents a single line of an order.
    /// </summary>
    public class Line
    {
        public string Title { get; set; }
        public int Quantity { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal DiscountedPrice { get; set; }
        public decimal ExtendedPrice {
            get { return Quantity * DiscountedPrice; }
        }

        public Line(string title, int quantity, decimal retailPrice)
        {
            Title = title;
            Quantity = quantity;
            RetailPrice = retailPrice;
            DiscountedPrice = retailPrice;
        }

    }
}
