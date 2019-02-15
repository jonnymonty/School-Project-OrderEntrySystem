using OrderEntryEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrderEntryEngine
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int LocationId { get; set; }

        public virtual Location Location { get; set; }

        public Condition Condition { get; set; }

        public virtual ICollection<ProductCategory> ProductCategories { get; set; }

        public virtual ICollection<OrderLine> Orders { get; set; }

        public bool IsArchived { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}