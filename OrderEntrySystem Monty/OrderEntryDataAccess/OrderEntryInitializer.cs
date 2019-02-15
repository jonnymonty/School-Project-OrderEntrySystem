using System.Collections.Generic;
using System.Data.Entity;
using OrderEntryEngine;

namespace OrderEntryDataAccess
{
    public class OrderEntryInitializer : DropCreateDatabaseAlways<OrderEntryContext>
    {
        protected override void Seed(OrderEntryContext context)
        {
            var locations = new List<Location>
            {
                new Location { Name = "NTC Bookstore", City = "Wausau", State = "WI", Description = "The school bookstore." },
                new Location { Name = "Warehouse", City = "Wausau", State = "WI", Description = "The school bookstore's storage warehouse." },
                new Location { Name = "Processing room", City = "Wausau", State = "WI", Description = "The room for processing placed orders." }
            };

            context.Locations.AddRange(locations);
            context.SaveChanges();

            var brands = new List<Brand>
            {
                new Brand { Name = "Cycleurope" },
                new Brand { Name = "Derby Cycle" },
                new Brand { Name = "Dorel Industries" }
            };

            context.Brands.AddRange(brands);
            context.SaveChanges();

            var categories = new List<Category>
            {
                new Category { Name = "Electronics" },
                new Category { Name = "Apparel" },
                new Category { Name = "Household" }
            };

            context.Categories.AddRange(categories);
            context.SaveChanges();

            var products = new List<Bike>
            {
                new Bike { Name = "Big Wheels", Condition = Condition.Poor, Description = "A bike for a kid.", Price = 50.00m, LocationId = 1, Quantity = 5, BrandId = 2 },
                new Bike { Name = "Model 21x", Condition = Condition.Excellent, Description = "yadda yadda.", Price = 250.00m, LocationId = 1, Quantity = 5, BrandId = 1 },
                new Bike { Name = "Model 50y", Condition = Condition.Poor, Description = "yadda yadda.", Price = 250.00m, LocationId = 2, Quantity = 5, BrandId = 1 }
            };

            context.Products.AddRange(products);
            context.SaveChanges();

            var productCategories = new List<ProductCategory>
            {
                new ProductCategory { ProductId = 1, CategoryId = 1 },
                new ProductCategory { ProductId = 2, CategoryId = 3 },
                new ProductCategory { ProductId = 3, CategoryId = 2 }
            };

            context.ProductCategories.AddRange(productCategories);
            context.SaveChanges();

            var customers = new List<Customer>
            {
                new Customer { FirstName = "Billy", LastName = "Buyer", Phone = "555-555-1234", Email = "billybuyer@example.com", Address = "111 Red Road", City = "Wausau", State = "WI" },
                new Customer { FirstName = "Sally", LastName = "Ride", Phone = "555-555-9876", Email = "astronautlady@nasa.gov", Address = "222 Orange Street", City = "Wausau", State = "WI" }
            };

            context.Customers.AddRange(customers);
            context.SaveChanges();

            var orders = new List<Order>
            {
                new Order { CustomerId = 1, Status = OrderStatus.Pending },
                new Order { CustomerId = 2, Status = OrderStatus.Pending }
            };

            context.Orders.AddRange(orders);
            context.SaveChanges();

            var lines = new List<OrderLine>
            {
                new OrderLine { OrderId = 1, ProductId = 1, Quantity = 2 },
                new OrderLine { OrderId = 2, ProductId = 2, Quantity = 1 },
                new OrderLine { OrderId = 2, ProductId = 3, Quantity = 1 }
            };

            context.Lines.AddRange(lines);
            context.SaveChanges();
        }
    }
}