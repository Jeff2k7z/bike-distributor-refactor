namespace BikeDistributor
{
    /// <summary>
    /// Represents a bicycle.
    /// </summary>
    public class Bike : Product
    {

        public string Model { get; private set; }
        public override string Title {get { return Brand + " " + Model; } }

        public Bike(string brand, string model, Money price) : base(brand, price)
        {
            Model = model;
        }
    }
}
