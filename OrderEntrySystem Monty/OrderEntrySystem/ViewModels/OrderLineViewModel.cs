using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using OrderEntryDataAccess;
using OrderEntryEngine;

namespace OrderEntrySystem
{
    public class OrderLineViewModel : EntityViewModel<OrderLine>
    {
        //private OrderLine line;

        /// <summary>
        /// The car view model's database repository.
        /// </summary>
        //private Repository repository;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="line">The line to be shown.</param>
        /// <param name="repository">The car repository.</param>
        public OrderLineViewModel(OrderLine line)
            : base("New order line", line)
        {
            this.Entity = line;
        }

        public string Error
        {
            get
            {
                return this.Entity.Error;
            }
        }

        public string this[string propertyName]
        {
            get
            {
                return this.Entity[propertyName];
            }
        }

        public OrderLine Line
        {
            get
            {
                return this.Entity;
            }
        }

        [EntityControlAttribute(ControlType.ComboBox, "Product: ", 1), EntityColumn(25, "Product", 1)]
        public Bike Product
        {
            get
            {
                return this.Entity.Product;
            }
            set
            {
                this.Entity.Product = value;
                this.OnPropertyChanged("Product");
                this.Entity.ProductAmount = value.Price;
                this.OnPropertyChanged("ProductTotal");
                this.Entity.CalculateTax();
                this.OnPropertyChanged("TaxTotal");
            }
        }

        public IEnumerable<Bike> Products
        {
            get
            {
                IRepository irepository = RepositoryManager.GetRepository(typeof(Bike));
                Repository<Bike> repository = (Repository<Bike>)irepository;

                return repository.GetEntities();
            }
        }

        [EntityControlAttribute(ControlType.TextBox, "Quantity: ", 1), EntityColumn(25, "Quantity", 1)]
        public int Quantity
        {
            get
            {
                return this.Entity.Quantity;
            }
            set
            {
                this.Entity.Quantity = value;
                this.OnPropertyChanged("Quantity");
                this.OnPropertyChanged("ProductTotal");
                this.Entity.CalculateTax();
                this.OnPropertyChanged("TaxTotal");
            }
        }

        [EntityControlAttribute(ControlType.TextBox, "Product Total: ", 1), EntityColumn(25, "Product Total", 1)]
        public decimal ProductTotal
        {
            get
            {
                return this.Entity.ExtendedProductAmount;
            }
        }

        [EntityControlAttribute(ControlType.TextBox, "Tax Total: ", 1), EntityColumn(25, "Tax Total", 1)]
        public decimal TaxTotal
        {
            get
            {
                return this.Entity.ExtendedTax;
            }
        }

        /// <summary>
        /// Creates the commands needed for the car view model.
        /// </summary>
        //protected override void CreateCommands()
        //{
        //    this.Commands.Add(new CommandViewModel("OK", new DelegateCommand(p => this.OkExecute()), true, false, "default"));
        //    this.Commands.Add(new CommandViewModel("Cancel", new DelegateCommand(p => this.CancelExecute()), false, true, "default"));
        //}

        /// <summary>
        /// Saves the car view model's car to the repository.
        /// </summary>
        //private bool Save()
        //{
        //    bool result = true;

        //    IRepository irepository = RepositoryManager.GetRepository(typeof(OrderLine));
        //    Repository<OrderLine> repository = (Repository<OrderLine>)irepository;

        //    if (this.Line.IsValid)
        //    {
        //        // Add line to repository.
        //        repository.AddEntity(this.Entity);

        //        repository.SaveToDatabase();
        //    }
        //    else
        //    {
        //        MessageBox.Show("One or more properties are invalid. Order line could not be saved.");
        //        result = false;
        //    }

        //    return result;
        //}

        /// <summary>
        /// Saves the car and closes the new car window.
        /// </summary>
        //private void OkExecute()
        //{
        //    if (this.Save())
        //    {
        //        this.CloseAction(true);
        //    }
        //}

        ///// <summary>
        ///// Closes the new car window without saving.
        ///// </summary>
        //private void CancelExecute()
        //{
        //    this.CloseAction(false);
        //}
    }
}