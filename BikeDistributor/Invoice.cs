using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeDistributor
{
    /// <summary>
    /// Represents an invoice or receipt for an order.
    /// </summary>
    public class Invoice
    {
        public string TemplateFileName { get; set; }
        public Order Order { get; set; }

        public Invoice(Order order, string templateFileName)
        {
            Order = order;
            TemplateFileName = templateFileName;
        }

        /// <summary>
        /// Implements a simple template engine which loads an invoice
        /// template and uses basic replacements to render the output.
        /// </summary>
        /// <remarks>
        /// TODO: This should be replaced with more advanced template engine.
        /// TODO: Render correct currency markers based on currency associated with price.
        /// </remarks>
        public string Render()
        {
            string templateText = "";

            // TODO: Templates should be stored in a service and accessed through an API.
            // Using file system for example only.
            var filePath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\BikeDistributor\Templates\" + TemplateFileName);
            using(var file = new System.IO.StreamReader(filePath))
            {
                templateText = file.ReadToEnd();
            }

            // Extract the body and lines
            var bodyTemplate = templateText.Split(new string[] { "{{body}}" }, StringSplitOptions.None)[1];
            var lineTemplate = templateText.Split(new string[] { "{{line}}" }, StringSplitOptions.None)[1];

            // Generate lines
            var lines = new StringBuilder();
            foreach(var line in Order.Lines)
            {
                var lt = lineTemplate;

                lt = lt.Replace("{{quantity}}", line.Quantity.ToString());
                lt = lt.Replace("{{title}}", line.Title);
                lt = lt.Replace("{{extendedprice}}", line.ExtendedPrice.ToString("c"));

                lines.Append(lt);
            }

            // Generate body
            bodyTemplate = bodyTemplate.Replace("{{lines}}", lines.ToString());
            bodyTemplate = bodyTemplate.Replace("{{companyname}}", Order.Customer.CompanyName);
            bodyTemplate = bodyTemplate.Replace("{{subtotal}}", Order.SubTotal.ToString("c"));
            bodyTemplate = bodyTemplate.Replace("{{taxtotal}}", Order.TaxTotal.ToString("c"));
            bodyTemplate = bodyTemplate.Replace("{{ordertotal}}", Order.OrderTotal.ToString("c"));

            return bodyTemplate;

        }
    }
}
