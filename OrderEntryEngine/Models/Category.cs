using System.Collections.Generic;

namespace OrderEntryEngine
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsArchived { get; set; }

        public virtual ICollection<ProductCategory> ProductCategories { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}