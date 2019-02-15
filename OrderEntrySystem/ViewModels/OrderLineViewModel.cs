using System.Collections.Generic;
using OrderEntryDataAccess;
using OrderEntryEngine;

namespace OrderEntrySystem
{
    public class OrderLineViewModel : WorkspaceViewModel
    {
        private OrderLine line;

        /// <summary>
        /// The car view model's database repository.
        /// </summary>
        private Repository repository;

        private bool isSelected;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="line">The line to be shown.</param>
        /// <param name="repository">The car repository.</param>
        public OrderLineViewModel(OrderLine line, Repository repository)
            : base("New order line")
        {
            this.line = line;
            this.repository = repository;
        }

        public OrderLine Line
        {
            get
            {
                return this.line;
            }
        }

        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }
            set
            {
                this.isSelected = value;
                this.OnPropertyChanged("IsSelected");
            }
        }

        public Product Product
        {
            get
            {
                return this.line.Product;
            }
            set
            {
                this.line.Product = value;
                this.OnPropertyChanged("Product");
                this.line.ProductAmount = this.ProductPrice;
                this.OnPropertyChanged("ProductAmount");
                this.line.CalculateTax();
                this.OnPropertyChanged("Tax");
            }
        }

        public decimal ProductTotal
        {
            get
            {
                return this.line.ExtendedProductAmount;
            }
        }

        public decimal TaxTotal
        {
            get
            {
                return this.line.ExtendedTaxAmount;
            }
        }

        public IEnumerable<Product> Products
        {
            get
            {
                return this.repository.GetProducts();
            }
        }

        public int Quantity
        {
            get
            {
                return this.line.Quantity;
            }
            set
            {
                this.line.Quantity = value;
                this.OnPropertyChanged("Quantity");
            }
        }

        public decimal ProductPrice
        {
            get
            {
                return this.line.Product.Price;
            }
        }

        public string ProductDescription
        {
            get
            {
                return this.line.Product.Description;
            }
        }

        /// <summary>
        /// Creates the commands needed for the car view model.
        /// </summary>
        protected override void CreateCommands()
        {
            this.Commands.Add(new CommandViewModel("OK", new DelegateCommand(p => this.OkExecute())));
            this.Commands.Add(new CommandViewModel("Cancel", new DelegateCommand(p => this.CancelExecute())));
        }

        /// <summary>
        /// Saves the car view model's car to the repository.
        /// </summary>
        private void Save()
        {
            this.repository.AddOrder(this.line.Order);

            // Add line to repository.
            this.repository.AddLine(this.line);

            this.repository.SaveToDatabase();
        }

        /// <summary>
        /// Saves the car and closes the new car window.
        /// </summary>
        private void OkExecute()
        {
            this.Save();
            this.CloseAction(true);
        }

        /// <summary>
        /// Closes the new car window without saving.
        /// </summary>
        private void CancelExecute()
        {
            this.CloseAction(false);
        }
    }
}