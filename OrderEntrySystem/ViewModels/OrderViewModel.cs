using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using OrderEntryDataAccess;
using OrderEntryEngine;
using OrderEntrySystem.Views;

namespace OrderEntrySystem
{
    public class OrderViewModel : EntityViewModel<Order>
    {
        /// <summary>
        /// The car being shown.
        /// </summary>
        //private Order order;

        /// <summary>
        /// The car view model's database repository.
        /// </summary>
        //private Repository repository;

        private MultiEntityViewModel<OrderLine, OrderLineViewModel, EntityView> filteredLineViewModel;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="order">The car to be shown.</param>
        /// <param name="repository">The car repository.</param>
        public OrderViewModel(Order order)
            : base("New order", order)
        {
            this.Entity = order;
            this.filteredLineViewModel = new MultiEntityViewModel<OrderLine, OrderLineViewModel, EntityView>();
            this.filteredLineViewModel.AllEntities = this.FilteredLines;
        }

        public Order Order
        {
            get
            {
                return this.Entity;
            }
        }

        [EntityControlAttribute(ControlType.TextBox, "Product Total: ", 1), EntityColumn(25, "ProductTotal", 1)]
        public decimal ProductTotal
        {
            get
            {
                return this.Entity.ProductTotal;
            }
        }

        [EntityControlAttribute(ControlType.TextBox, "Tax Total: ", 2), EntityColumn(25, "Tax Total", 1)]
        public decimal TaxTotal
        {
            get
            {
                return this.Entity.TaxTotal;
            }
        }

        [EntityControlAttribute(ControlType.TextBox, "Total: ", 3), EntityColumn(25, "Total", 1)]
        public decimal Total
        {
            get
            {
                return this.Entity.Total;
            }
        }

        public ObservableCollection<OrderLineViewModel> FilteredLines
        {
            get
            {
                List<OrderLineViewModel> lines = null;

                if (this.Entity.Lines != null)
                {
                    lines =
                        (from l in this.Entity.Lines
                         select new OrderLineViewModel(l)).ToList();
                }

                this.FilteredLineViewModel.AddPropertyChangedEvent(lines);

                return new ObservableCollection<OrderLineViewModel>(lines);
            }
        }

        public MultiEntityViewModel<OrderLine, OrderLineViewModel, EntityView> FilteredLineViewModel
        {
            get
            {
                return this.filteredLineViewModel;
            }
        }

        [EntityControlAttribute(ControlType.ComboBox, "Status: ", 1), EntityColumn(25, "Status", 1)]
        public OrderStatus Status
        {
            get
            {
                return this.Entity.Status;
            }
            set
            {
                this.Entity.Status = value;
            }
        }

        [EntityControlAttribute(ControlType.ComboBox, "Customer: ", 1), EntityColumn(25, "Customer", 1)]
        public Customer Customer
        {
            get
            {
                return this.Entity.Customer;
            }
            set
            {
                this.Entity.Customer = value;
            }
        }

        [EntityControlAttribute(ControlType.TextBox, "ShippingAmount: ", 1), EntityColumn(25, "Shipping Amount", 1)]
        public decimal ShippingAmount
        {
            get
            {
                return this.Entity.ShippingAmount;
            }
            set
            {
                this.Entity.ShippingAmount = value;
                this.OnPropertyChanged("ShippingAmount");
                this.OnPropertyChanged("Total");
            }
        }

        public IEnumerable<OrderStatus> OrderStatuses
        {
            get
            {
                return Enum.GetValues(typeof(OrderStatus)) as IEnumerable<OrderStatus>;
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

        public void UpdateOrderTotals()
        {
            this.OnPropertyChanged("ProductTotal");
            this.OnPropertyChanged("TaxTotal");
            this.OnPropertyChanged("Total");
            this.OnPropertyChanged("Status");
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
        //private void Save()
        //{
        //    // Add car to repository.
        //    IRepository irepository = RepositoryManager.GetRepository(typeof(Order));
        //    Repository<Order> repository = (Repository<Order>)irepository;

        //    repository.AddEntity(this.Entity);

        //    repository.SaveToDatabase();
        //}

        /// <summary>
        /// Saves the car and closes the new car window.
        /// </summary>
        //private void OkExecute()
        //{
        //    this.Save();
        //    this.CloseAction(true);
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