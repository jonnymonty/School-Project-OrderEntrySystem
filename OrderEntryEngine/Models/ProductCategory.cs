using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEntryEngine
{
    [Serializable]
    public class ProductCategory : IEntity
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public virtual Bike Product { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public bool IsArchived { get; set; }
    }
}