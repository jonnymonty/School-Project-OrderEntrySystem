using OrderEntryEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEntryDataAccess
{
    public static class RepositoryManager
    {
        public static Dictionary<Type, IRepository> Dictionary;

        //public static Repository<Category> CategoryRepository;

        //public static Repository<Customer> CustomerRepository;

        //public static Repository<Location> LocationRepository;

        //public static Repository<Order> OrderRepository;

        //public static Repository<OrderLine> OrderLineRepository;

        //public static Repository<Product> ProductRepository;

        //public static Repository<ProductCategory> ProductCategoryRepository;

        public static OrderEntryContext Context { get; set; }

        public static void InitializeRepository()
        {
            Dictionary = new Dictionary<Type, IRepository>();
            Context = new OrderEntryContext();
            Dictionary.Add(typeof(Category), new LookupRepository<Category>(Context.Categories));
            Dictionary.Add(typeof(Customer), new LookupRepository<Customer>(Context.Customers));
            Dictionary.Add(typeof(Location), new LookupRepository<Location>(Context.Locations));
            Dictionary.Add(typeof(Order), new Repository<Order>(Context.Orders));
            Dictionary.Add(typeof(OrderLine), new Repository<OrderLine>(Context.Lines));
            Dictionary.Add(typeof(Bike), new LookupRepository<Bike>(Context.Products));
            Dictionary.Add(typeof(Brand), new LookupRepository<Brand>(Context.Brands));
            Dictionary.Add(typeof(ProductCategory), new Repository<ProductCategory>(Context.ProductCategories));
        }

        public static ILookupRepository GetLookupRepository(Type type)
        {
            IRepository result = RepositoryManager.GetRepository(type);

            return result as ILookupRepository;
        }

        public static IRepository GetRepository(Type type)
        {
            IRepository result;
            if (Dictionary.TryGetValue(type, out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
