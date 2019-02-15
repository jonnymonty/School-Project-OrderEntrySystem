using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using OrderEntryDataAccess;
using OrderEntryEngine;
using OrderEntrySystem.Views;

namespace OrderEntrySystem
{
    public class CustomerViewModel : EntityViewModel<Customer>
    {
        /// <summary>
        /// The car being shown.
        /// </summary>
        //private Customer customer;

        /// <summary>
        /// The car view model's database repository.
        /// </summary>
        //private Repository repository;

        private MultiEntityViewModel<Order, OrderViewModel, EntityView> filteredOrderViewModel;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="customer">The car to be shown.</param>
        /// <param name="repository">The car repository.</param>
        public CustomerViewModel(Customer customer)
            : base("New customer", customer)
        {
            this.Entity = customer;
            this.filteredOrderViewModel = new MultiEntityViewModel<Order, OrderViewModel, EntityView>();
            this.filteredOrderViewModel.AllEntities = this.FilteredOrders;
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

        [EntityControlAttribute(ControlType.TextBox, "First Name: ", 1), EntityColumn(25, "FirstName", 1)]
        public string FirstName
        {
            get
            {
                return this.Entity.FirstName;
            }
            set
            {
                this.Entity.FirstName = value;
                this.OnPropertyChanged("FirstName");
            }
        }

        [EntityControlAttribute(ControlType.TextBox, "Last Name: ", 2), EntityColumn(25, "LastName", 1)]
        public string LastName
        {
            get
            {
                return this.Entity.LastName;
            }
            set
            {
                this.Entity.LastName = value;
                this.OnPropertyChanged("LastName");
            }
        }

        [EntityControlAttribute(ControlType.TextBox, "Phone: ", 3), EntityColumn(25, "Phone", 1)]
        public string Phone
        {
            get
            {
                return this.Entity.Phone;
            }
            set
            {
                this.Entity.Phone = value;
                this.OnPropertyChanged("Phone");
            }
        }

        [EntityControlAttribute(ControlType.TextBox, "Email: ", 4), EntityColumn(25, "Email", 1)]
        public string Email
        {
            get
            {
                return this.Entity.Email;
            }
            set
            {
                this.Entity.Email = value;
                this.OnPropertyChanged("Email");
            }
        }

        public ObservableCollection<OrderViewModel> FilteredOrders
        {
            get
            {
                var orders =
                    (from o in this.Entity.Orders
                    select new OrderViewModel(o)).ToList();

                this.FilteredOrderViewModel.AddPropertyChangedEvent(orders);

                return new ObservableCollection<OrderViewModel>(orders);
            }
        }

        public MultiEntityViewModel<Order, OrderViewModel, EntityView> FilteredOrderViewModel
        {
            get
            {
                return this.filteredOrderViewModel;
            }
        }

        [EntityControlAttribute(ControlType.TextBox, "Address: ", 5), EntityColumn(25, "Address", 1)]
        public string Address
        {
            get
            {
                return this.Entity.Address;
            }
            set
            {
                this.Entity.Address = value;
                this.OnPropertyChanged("Address");
            }
        }

        [EntityControlAttribute(ControlType.TextBox, "City: ", 6), EntityColumn(25, "City", 1)]
        public string City
        {
            get
            {
                return this.Entity.City;
            }
            set
            {
                this.Entity.City = value;
                this.OnPropertyChanged("City");
            }
        }

        [EntityControlAttribute(ControlType.TextBox, "State: ", 7), EntityColumn(25, "State", 1)]
        public string State
        {
            get
            {
                return this.Entity.State;
            }
            set
            {
                this.Entity.State = value;
                this.OnPropertyChanged("State");
            }
        }

        public Customer Customer
        {
            get
            {
                return this.Entity;
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

        ///// <summary>
        ///// Saves the car view model's car to the repository.
        ///// </summary>
        //private bool Save()
        //{
        //    bool result = true;

        //    IRepository irepository = RepositoryManager.GetRepository(typeof(Customer));
        //    Repository<Customer> repository = (Repository<Customer>)irepository;

        //    if (this.Customer.IsValid)
        //    {
        //        // Add customer to repository.
        //        repository.AddEntity(this.Entity);

        //        repository.SaveToDatabase();
        //    }
        //    else
        //    {
        //        MessageBox.Show("One or more properties are invalid. Customer could not be saved.");
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
        ///// Closes the new customer window without saving.
        ///// </summary>
        //private void CancelExecute()
        //{
        //    this.CloseAction(false);
        //}
    }
}