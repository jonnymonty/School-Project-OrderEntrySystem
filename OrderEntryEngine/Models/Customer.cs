using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrderEntryEngine
{
    public class Customer
    {
        public Customer()
        {
            this.Orders = new List<Order>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [MaxLength(15)]
        public string Phone { get; set; }

        [Required]
        [MaxLength(255)]
        public string Email { get; set; }

        [MaxLength(150)]
        public string Address { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(2)]
        public string State { get; set; }

        public bool IsArchived { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public override string ToString()
        {
            return this.FirstName + " " + this.LastName;
        }
    }
}