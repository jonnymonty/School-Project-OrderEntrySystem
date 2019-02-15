using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEntryEngine
{
    public class ProductEventArgs
    {
        public ProductEventArgs(Bike product)
        {
            this.Product = product;
        }

        public Bike Product { get; private set; }
    }
}