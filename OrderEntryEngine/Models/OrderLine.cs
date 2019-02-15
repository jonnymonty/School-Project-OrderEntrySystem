using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace OrderEntryEngine
{
    public class OrderLine
    {
        private decimal productAmount;

        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        public int ProductId { get; set; }

        public decimal ProductAmount
        {
            get
            {
                return this.productAmount;
            }
            set
            {
                this.productAmount = Math.Round(value, 2);
            }
        }

        public decimal TaxPerProduct { get; set; }

        public decimal ExtendedProductAmount
        {
            get
            {
                return Math.Round(this.ProductAmount * this.Quantity, 2);
            }
        }

        public decimal ExtendedTaxAmount
        {
            get
            {
                return Math.Round(this.TaxPerProduct * this.Quantity, 2);
            }
        }

        public void Post()
        {
            this.productAmount = this.Product.Price;
            this.Product.Quantity -= this.Quantity;
        }

        public virtual Product Product { get; set; }

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        public bool IsArchived { get; set; }

        public void CalculateTax()
        {
            decimal taxRate = decimal.Parse(ConfigurationManager.AppSettings["TaxRate"]);
            this.TaxPerProduct = Math.Round(this.ProductAmount * taxRate, 2);
        }
    }
}