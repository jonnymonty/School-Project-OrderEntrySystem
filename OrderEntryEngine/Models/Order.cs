using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderEntryEngine
{
    public class Order
    {
        private decimal shippingAmount;

        private decimal productTotal;

        private decimal taxTotal;

        public Order()
        {
            this.Lines = new List<OrderLine>();
        }

        public void Post()
        {
            if (this.Status == OrderStatus.Processing)
            {
                this.Status = OrderStatus.Shipped;
                foreach (OrderLine l in this.Lines)
                {
                    l.Post();
                    l.CalculateTax();
                }
                this.CalculateTotals();
            }
        }

        public decimal ShippingAmount
        {
            get
            {
                return this.shippingAmount;
            }
            set
            {
                this.shippingAmount = Math.Round(value, 2);
            }
        }

        public decimal ProductTotal
        {
            get
            {
                return this.productTotal;
            }
            set
            {
                this.productTotal = Math.Round(value, 2);
            }
        }

        public decimal TaxTotal
        {
            get
            {
                return this.taxTotal;
            }
            set
            {
                this.taxTotal = Math.Round(value, 2);
            }
        }

        public decimal Total
        {
            get
            {
                return Math.Round(this.shippingAmount + this.productTotal + taxTotal, 2);
            }
        }

        public int Id { get; set; }

        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<OrderLine> Lines { get; set; }

        public OrderStatus Status { get; set; }

        public bool IsArchived { get; set; }

        public void CalculateTotals()
        {
            this.ProductTotal = this.Lines.Where(l => !l.IsArchived).Sum(l => l.ExtendedProductAmount);
            this.TaxTotal = this.Lines.Where(l => !l.IsArchived).Sum(l => l.ExtendedTaxAmount);
        }
    }
}