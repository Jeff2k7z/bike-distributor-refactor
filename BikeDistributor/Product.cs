using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeDistributor
{
    /// <summary>
    /// Represents a generic product.
    /// </summary>
    public class Product
    {
        
        public string Brand { get; private set; }
        public Money Price { get; set; }
        public virtual string Title { get; set; }

        public Product(string brand, Money price)
        {
            Brand = brand;
            Price = price;
        }

        public Product(string brand, string title, Money price) : this(brand, price)
        {
            Title = title;
        }
    }
}
