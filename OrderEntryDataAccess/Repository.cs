using System;
using System.Collections.Generic;
using System.Linq;
using OrderEntryEngine;
using System.Data.Entity;

namespace OrderEntryDataAccess
{
    public class Repository<T> : IRepository where T : class, IEntity
    {
        private DbSet<T> dbSet;

        public event EventHandler<EntityEventArgs<T>> EntityAdded;

        //public event EventHandler<EntityEventArgs<Customer>> CustomerAdded;

        //public event EventHandler<EntityEventArgs<Location>> LocationAdded;

        //public event EventHandler<EntityEventArgs<Category>> CategoryAdded;

        //public event EventHandler<EntityEventArgs<Order>> OrderAdded;

        //public event EventHandler<EntityEventArgs<OrderLine>> OrderLineAdded;

        public event EventHandler<EntityEventArgs<T>> EntityRemoved;

        //public event EventHandler<EntityEventArgs<Location>> LocationRemoved;

        //public event EventHandler<EntityEventArgs<Order>> OrderRemoved;

        //public event EventHandler<EntityEventArgs<Customer>> CustomerRemoved;

        //public event EventHandler<EntityEventArgs<Category>> CategoryRemoved;

        //public event EventHandler<EntityEventArgs<OrderLine>> OrderLineRemoved;

        public Repository(DbSet<T> dbSet)
        {
            this.dbSet = dbSet;
        }

        public void AddEntity(T product)
        {
            if (!this.ContainsEntity(product))
            {
                this.dbSet.Add(product);

                if (this.EntityAdded != null)
                {
                    this.EntityAdded(this, new EntityEventArgs<T>(product));
                }
            }
        }

        public bool ContainsEntity(T product)
        {
            return this.GetEntity(product.Id) != null;
        }

        public T GetEntity(int id)
        {
            return this.dbSet.Find(id);
        }

        public List<T> GetEntities()
        {
            return this.dbSet.Where(p => !p.IsArchived).ToList();
        }

        //public void AddCustomer(Customer customer)
        //{
        //    if (!this.ContainsCustomer(customer))
        //    {
        //        this.db.Customers.Add(customer);

        //        if (this.CustomerAdded != null)
        //        {
        //            this.CustomerAdded(this, new EntityEventArgs<Customer>(customer));
        //        }
        //    }
        //}

        //public bool ContainsCustomer(Customer customer)
        //{
        //    return this.GetCustomer(customer.Id) != null;
        //}

        //public Customer GetCustomer(int id)
        //{
        //    return this.db.Customers.Find(id);
        //}

        //public List<Customer> GetCustomers()
        //{
        //    return this.db.Customers.Where(c => !c.IsArchived).ToList();
        //}

        //public void AddLocation(Location location)
        //{
        //    if (!this.ContainsLocation(location))
        //    {
        //        this.db.Locations.Add(location);

        //        if (this.LocationAdded != null)
        //        {
        //            this.LocationAdded(this, new EntityEventArgs<Location>(location));
        //        }
        //    }
        //}

        //public bool ContainsLocation(Location location)
        //{
        //    return this.GetLocation(location.Id) != null;
        //}

        //public Location GetLocation(int id)
        //{
        //    return this.db.Locations.Find(id);
        //}

        //public List<Location> GetLocations()
        //{
        //    return this.db.Locations.Where(l => !l.IsArchived).ToList();
        //}

        //public void AddCategory(Category category)
        //{
        //    if (!this.ContainsCategory(category))
        //    {
        //        this.db.Categories.Add(category);

        //        if (this.CategoryAdded != null)
        //        {
        //            this.CategoryAdded(this, new EntityEventArgs<Category>(category));
        //        }
        //    }
        //}

        //public bool ContainsCategory(Category category)
        //{
        //    return this.GetCategory(category.Id) != null;
        //}

        //public Category GetCategory(int id)
        //{
        //    return this.db.Categories.Find(id);
        //}

        //public List<Category> GetCategories()
        //{
        //    return this.db.Categories.Where(c => !c.IsArchived).ToList();
        //}

        //public void AddOrder(Order order)
        //{
        //    if (!this.ContainsOrder(order))
        //    {
        //        this.db.Orders.Add(order);

        //        if (this.OrderAdded != null)
        //        {
        //            this.OrderAdded(this, new EntityEventArgs<Order>(order));
        //        }
        //    }
        //}

        //public bool ContainsOrder(Order order)
        //{
        //    return this.GetOrder(order.Id) != null;
        //}

        //public Order GetOrder(int id)
        //{
        //    return this.db.Orders.Find(id);
        //}

        //public List<Order> GetOrders()
        //{
        //    return this.db.Orders.Where(o => !o.IsArchived).ToList();
        //}

        //public void AddLine(OrderLine line)
        //{
        //    if (!this.ContainsLine(line))
        //    {
        //        this.db.Lines.Add(line);

        //        if (this.OrderLineAdded != null)
        //        {
        //            this.OrderLineAdded(this, new EntityEventArgs<OrderLine>(line));
        //        }
        //    }
        //}

        //public bool ContainsLine(OrderLine line)
        //{
        //    return this.GetLine(line.Id) != null;
        //}

        //public OrderLine GetLine(int id)
        //{
        //    return this.db.Lines.Find(id);
        //}

        //public List<OrderLine> GetLines()
        //{
        //    return this.db.Lines.Where(l => !l.IsArchived).ToList();
        //}

        //public void AddProductCategory(ProductCategory pc)
        //{
        //    if (!this.ContainsProductCategory(pc))
        //    {
        //        this.db.ProductCategories.Add(pc);

        //        if (this.CategoryAdded != null)
        //        {
        //            this.CategoryAdded(this, new EntityEventArgs<Category>(pc.Category));
        //        }
        //    }
        //}

        //public bool ContainsProductCategory(ProductCategory pc)
        //{
        //    return this.GetProductCategory(pc.Id) != null;
        //}

        //public ProductCategory GetProductCategory(int id)
        //{
        //    return this.db.ProductCategories.Find(id);
        //}

        //public List<ProductCategory> GetProductCategories()
        //{
        //    return this.db.ProductCategories.Where(pc => !pc.IsArchived).ToList();
        //}

        //public void RemoveCustomer(Customer customer)
        //{
        //    if (customer == null)
        //    {
        //        throw new ArgumentNullException("customer");
        //    }

        //    customer.IsArchived = true;

        //    if (this.CustomerRemoved != null)
        //    {
        //        this.CustomerRemoved(this, new EntityEventArgs<Customer>(customer));
        //    }
        //}

        public void RemoveEntity(T product)
        {
            if (product == null)
            {
                throw new ArgumentNullException("product");
            }

            product.IsArchived = true;

            if (this.EntityRemoved != null)
            {
                this.EntityRemoved(this, new EntityEventArgs<T>(product));
            }
        }

        //public void RemoveLine(OrderLine line)
        //{
        //    if (line == null)
        //    {
        //        throw new ArgumentNullException("line");
        //    }

        //    line.IsArchived = true;

        //    if (this.OrderLineRemoved != null)
        //    {
        //        this.OrderLineRemoved(this, new EntityEventArgs<OrderLine>(line));
        //    }
        //}

        //public void RemoveLocation(Location location)
        //{
        //    if (location == null)
        //    {
        //        throw new ArgumentNullException("location");
        //    }

        //    location.IsArchived = true;

        //    if (this.LocationRemoved != null)
        //    {
        //        this.LocationRemoved(this, new EntityEventArgs<Location>(location));
        //    }
        //}

        //public void RemoveOrder(Order order)
        //{
        //    if (order == null)
        //    {
        //        throw new ArgumentNullException("order");
        //    }

        //    order.IsArchived = true;

        //    if (this.OrderRemoved != null)
        //    {
        //        this.OrderRemoved(this, new EntityEventArgs<Order>(order));
        //    }
        //}

        //public void RemoveCategory(Category category)
        //{
        //    if (category == null)
        //    {
        //        throw new ArgumentNullException("category");
        //    }

        //    category.IsArchived = true;

        //    if (this.CategoryRemoved != null)
        //    {
        //        this.CategoryRemoved(this, new EntityEventArgs<Category>(category));
        //    }
        //}

        //public void RemoveProductCategory(ProductCategory pc)
        //{
        //    if (pc == null)
        //    {
        //        throw new ArgumentNullException("pc");
        //    }

        //    pc.IsArchived = true;

        //    if (this.CategoryRemoved != null)
        //    {
        //        this.CategoryRemoved(this, new EntityEventArgs<Category>(pc.Category));
        //    }
        //}

        public void SaveToDatabase()
        {
            RepositoryManager.Context.SaveChanges();
        }
    }
}