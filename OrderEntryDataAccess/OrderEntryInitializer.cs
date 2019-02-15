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
            };

            context.Locations.AddRange(locations);
            context.SaveChanges();

            var categories = new List<Category>
            {
                new Category { Name = "Electronics" },
                new Category { Name = "Apparel" },
                new Category { Name = "Household" }
            };

            context.Categories.AddRange(categories);
            context.SaveChanges();

            var products = new List<Product>
            {
                new Product { Name = "16GB Flash Drive", Condition = Condition.Poor, Description = "A portable flash drive adorned with the NTC logo.", Price = 15.00m, LocationId = 1 },
                new Product { Name = "Coffee Mug", Condition = Condition.Poor, Description = "A sleek mug adorned with the NTC logo.", Price = 9.50m, LocationId = 1 },
                new Product { Name = "T-Shirt", Condition = Condition.Poor, Description = "A stylish t-shirt showing off your school pride, adorned with an NTC logo.", Price = 18.50m, LocationId = 2 }
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
                new Order { CustomerId = 1, Status = OrderStatus.Processing },
                new Order { CustomerId = 2, Status = OrderStatus.Processing }
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