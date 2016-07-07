using Microsoft.VisualStudio.TestTools.UnitTesting;
using BikeDistributor;
using System.Collections.Generic;
using System;
using System.Text;

namespace BikeDistributor.Test
{
    [TestClass]
    public class OrderTest
    {
        private readonly static Bike Defy = new Bike("Giant", "Defy 1", new Money(1000m, Currency.USD));
        private readonly static Bike Elite = new Bike("Specialized", "Venge Elite", new Money(2000m, Currency.USD));
        private readonly static Bike DuraAce = new Bike("Specialized", "S-Works Venge Dura-Ace", new Money(5000m, Currency.USD));
        private readonly static Bike SuperSix = new Bike("Cannondale", "SuperSix Evo HM", new Money(4000m, Currency.USD));
        private readonly static Product ButtButter = new Product("Paceline Products", "Chamois Butt'r Original", new Money(8m, Currency.USD));
        
        private readonly static Customer BikeShop = new Customer("Anywhere Bike Shop");

        private ITaxEngine _taxEngine = new TaxEngine();

        #region Order Line Tests

        [TestMethod]
        public void Line_1Defy()
        {
            var order = new Order(BikeShop, _taxEngine);
            order.AddLine(new Line(Defy.Title, 1, Defy.Price.Amount));
            var invoice = new Invoice(order, "SimpleTextReceipt.txt");
            Assert.AreEqual(ResultLine1Defy, invoice.Render());
        }

        private const string ResultLine1Defy = @"Order Receipt for Anywhere Bike Shop
	1 x Giant Defy 1 = $1,000.00

Sub-Total: $1,000.00
Tax: $72.50
Total: $1,072.50";

        [TestMethod]
        public void Line_GenericProduct()
        {
            var order = new Order(BikeShop, _taxEngine);
            order.AddLine(new Line(ButtButter.Title, 1, ButtButter.Price.Amount));
            var invoice = new Invoice(order, "SimpleTextReceipt.txt");
            Assert.AreEqual(ResultLine_GenericProduct, invoice.Render());
        }

        private const string ResultLine_GenericProduct = @"Order Receipt for Anywhere Bike Shop
	1 x Chamois Butt'r Original = $8.00

Sub-Total: $8.00
Tax: $0.58
Total: $8.58";

        [TestMethod]
        public void Line_MultiLine()
        {
            var order = new Order(BikeShop, _taxEngine);
            order.AddLine(new Line(ButtButter.Title, 1, ButtButter.Price.Amount));
            order.AddLine(new Line(Defy.Title, 1, Defy.Price.Amount));
            order.AddLine(new Line(Elite.Title, 1, Elite.Price.Amount));
            order.AddLine(new Line(SuperSix.Title, 1, SuperSix.Price.Amount));
            var invoice = new Invoice(order, "SimpleTextReceipt.txt");
            Assert.AreEqual(ResultLine_MultiLine, invoice.Render());
        }

        private const string ResultLine_MultiLine = @"Order Receipt for Anywhere Bike Shop
	1 x Chamois Butt'r Original = $8.00
	1 x Giant Defy 1 = $1,000.00
	1 x Specialized Venge Elite = $2,000.00
	1 x Cannondale SuperSix Evo HM = $4,000.00

Sub-Total: $7,008.00
Tax: $508.08
Total: $7,516.08";

        #endregion

        #region Receipt Tests

        [TestMethod]
        public void Receipt_OneDefy()
        {
            var order = new Order(BikeShop, _taxEngine);
            order.AddLine(new Line(Defy.Brand + " " + Defy.Model, 1, Defy.Price.Amount));
            var invoice = new Invoice(order, "SimpleTextReceipt.txt");
            Assert.AreEqual(ResultStatementOneDefy, invoice.Render());
        }

        private const string ResultStatementOneDefy = @"Order Receipt for Anywhere Bike Shop
	1 x Giant Defy 1 = $1,000.00

Sub-Total: $1,000.00
Tax: $72.50
Total: $1,072.50";

        [TestMethod]
        public void Receipt_OneElite()
        {
            var order = new Order(BikeShop, _taxEngine);
            order.AddLine(new Line(Elite.Brand + " " + Elite.Model, 1, Elite.Price.Amount));
            var invoice = new Invoice(order, "SimpleTextReceipt.txt");
            Assert.AreEqual(ResultStatementOneElite, invoice.Render());
        }

        private const string ResultStatementOneElite = @"Order Receipt for Anywhere Bike Shop
	1 x Specialized Venge Elite = $2,000.00

Sub-Total: $2,000.00
Tax: $145.00
Total: $2,145.00";

        [TestMethod]
        public void Receipt_OneDuraAce()
        {
            var order = new Order(BikeShop, _taxEngine);
            order.AddLine(new Line(DuraAce.Brand + " " + DuraAce.Model, 1, DuraAce.Price.Amount));
            var invoice = new Invoice(order, "SimpleTextReceipt.txt");
            Assert.AreEqual(ResultStatementOneDuraAce, invoice.Render());
        }

        private const string ResultStatementOneDuraAce = @"Order Receipt for Anywhere Bike Shop
	1 x Specialized S-Works Venge Dura-Ace = $5,000.00

Sub-Total: $5,000.00
Tax: $362.50
Total: $5,362.50";

        [TestMethod]
        public void Receipt_HTMLOneDefy()
        {
            var order = new Order(BikeShop, _taxEngine);
            order.AddLine(new Line(Defy.Brand + " " + Defy.Model, 1, Defy.Price.Amount));
            var invoice = new Invoice(order, "SimpleHTMLReceipt.html");
            Assert.AreEqual(HtmlResultStatementOneDefy, invoice.Render());
        }

        private const string HtmlResultStatementOneDefy = @"<html><body><h1>Order Receipt for Anywhere Bike Shop</h1><ul><li>1 x Giant Defy 1 = $1,000.00</li></ul><h3>Sub-Total: $1,000.00</h3><h3>Tax: $72.50</h3><h2>Total: $1,072.50</h2></body></html>";

        [TestMethod]
        public void Receipt_HTMLOneElite()
        {
            var order = new Order(BikeShop, _taxEngine);
            order.AddLine(new Line(Elite.Brand + " " + Elite.Model, 1, Elite.Price.Amount));
            var invoice = new Invoice(order, "SimpleHTMLReceipt.html");
            Assert.AreEqual(HtmlResultStatementOneElite, invoice.Render());
        }

        private const string HtmlResultStatementOneElite = @"<html><body><h1>Order Receipt for Anywhere Bike Shop</h1><ul><li>1 x Specialized Venge Elite = $2,000.00</li></ul><h3>Sub-Total: $2,000.00</h3><h3>Tax: $145.00</h3><h2>Total: $2,145.00</h2></body></html>";

        [TestMethod]
        public void Receipt_HTMLOneDuraAce()
        {
            var order = new Order(BikeShop, _taxEngine);
            order.AddLine(new Line(DuraAce.Brand + " " + DuraAce.Model, 1, DuraAce.Price.Amount));
            var invoice = new Invoice(order, "SimpleHTMLReceipt.html");
            Assert.AreEqual(HtmlResultStatementOneDuraAce, invoice.Render());
        }

        private const string HtmlResultStatementOneDuraAce = @"<html><body><h1>Order Receipt for Anywhere Bike Shop</h1><ul><li>1 x Specialized S-Works Venge Dura-Ace = $5,000.00</li></ul><h3>Sub-Total: $5,000.00</h3><h3>Tax: $362.50</h3><h2>Total: $5,362.50</h2></body></html>";

        [TestMethod]
        public void Receipt_FancyInvoiceOneDuraAce()
        {
            var order = new Order(BikeShop, _taxEngine);
            order.AddLine(new Line(DuraAce.Title, 1, DuraAce.Price.Amount));
            order.AddLine(new Line(SuperSix.Title, 1, SuperSix.Price.Amount));
            order.AddLine(new Line(ButtButter.Title, 2, ButtButter.Price.Amount));
            var invoice = new Invoice(order, "FancyInvoice.html");
            Assert.AreEqual("62E31D89EA1F901788BBE3E6FEC55351", GenerateHash(invoice.Render()));
        }

        #endregion

        #region Promotion Tests

        [TestMethod]
        public void Promo_1000Dollars20Units()
        {
            List<Promotion> promos = new List<Promotion>();
            promos.Add(new Promotion("test", true, 1000m, null, 20, null, .10m, null));
            promos.Add(new Promotion("test", true, 2000m, null, 10, null, .10m, null));
            promos.Add(new Promotion("test", true, 5000m, null, 5, null, .20m, null));

            var order = new Order(BikeShop, _taxEngine);
            order.Promotions = promos;
            order.AddLine(new Line(Defy.Brand + " " + Defy.Model, 20, Defy.Price.Amount));
            var invoice = new Invoice(order, "SimpleTextReceipt.txt");
            Assert.AreEqual(Result1000Dollars20Units, invoice.Render());
        }

        private const string Result1000Dollars20Units = @"Order Receipt for Anywhere Bike Shop
	20 x Giant Defy 1 = $18,000.00

Sub-Total: $18,000.00
Tax: $1,305.00
Total: $19,305.00";

        [TestMethod]
        public void Promo_2000Dollars10Units()
        {
            List<Promotion> promos = new List<Promotion>();
            promos.Add(new Promotion("test", true, 1000m, null, 20, null, .10m, null));
            promos.Add(new Promotion("test", true, 2000m, null, 10, null, .10m, null));
            promos.Add(new Promotion("test", true, 5000m, null, 5, null, .20m, null));

            var order = new Order(BikeShop, _taxEngine);
            order.Promotions = promos;
            order.AddLine(new Line(Elite.Brand + " " + Elite.Model, 10, Elite.Price.Amount));
            var invoice = new Invoice(order, "SimpleTextReceipt.txt");
            Assert.AreEqual(Result2000Dollars10Units, invoice.Render());
        }

        private const string Result2000Dollars10Units = @"Order Receipt for Anywhere Bike Shop
	10 x Specialized Venge Elite = $18,000.00

Sub-Total: $18,000.00
Tax: $1,305.00
Total: $19,305.00";

        [TestMethod]
        public void Promo_5000Dollars5Units()
        {
            List<Promotion> promos = new List<Promotion>();
            promos.Add(new Promotion("test", true, 1000m, null, 20, null, .10m, null));
            promos.Add(new Promotion("test", true, 2000m, null, 10, null, .10m, null));
            promos.Add(new Promotion("test", true, 5000m, null, 5, null, .20m, null));

            var order = new Order(BikeShop, _taxEngine);
            order.Promotions = promos;
            order.AddLine(new Line(DuraAce.Brand + " " + DuraAce.Model, 5, DuraAce.Price.Amount));
            var invoice = new Invoice(order, "SimpleTextReceipt.txt");
            Assert.AreEqual(Result5000Dollars5Units, invoice.Render());
        }

        private const string Result5000Dollars5Units = @"Order Receipt for Anywhere Bike Shop
	5 x Specialized S-Works Venge Dura-Ace = $20,000.00

Sub-Total: $20,000.00
Tax: $1,450.00
Total: $21,450.00";


        #endregion

        private string GenerateHash(string text)
        {
            string hash;

            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                hash = BitConverter.ToString(
                    md5.ComputeHash(Encoding.UTF8.GetBytes(text))
                ).Replace("-", String.Empty);
            }

            return hash;
        }
    }
}


