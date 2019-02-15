using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrderEntryEngine
{
    public class Location
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [MaxLength(100)]
        public string City { get; set; }

        [Required]
        [MaxLength(2)]
        public string State { get; set; }

        public bool IsArchived { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}