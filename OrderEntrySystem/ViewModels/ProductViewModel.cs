using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OrderEntryDataAccess;
using OrderEntryEngine;

namespace OrderEntrySystem
{
    public class ProductViewModel : WorkspaceViewModel
    {
        private Product product;

        private Repository repository;

        private bool isSelected;

        private MultiCategoryViewModel filteredCategoryViewModel;

        public ProductViewModel(Product product, Repository repository)
            : base("New product")
        {
            this.product = product;
            this.repository = repository;
            this.filteredCategoryViewModel = new MultiCategoryViewModel(this.repository, this.product);
            this.filteredCategoryViewModel.AllCategories = this.FilteredCategories;
        }

        public MultiCategoryViewModel FilteredCategoryViewModel
        {
            get
            {
                return this.filteredCategoryViewModel;
            }
        }

        public ObservableCollection<CategoryViewModel> FilteredCategories
        {
            get
            {
                List<CategoryViewModel> categories = null;

                if (this.product.ProductCategories != null)
                {
                    categories =
                        (from c in this.product.ProductCategories
                        select new CategoryViewModel(c.Category, this.repository)).ToList();
                }

                this.FilteredCategoryViewModel.AddPropertyChangedEvent(categories);

                return new ObservableCollection<CategoryViewModel>(categories);
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

        public string Name
        {
            get
            {
                return this.product.Name;
            }
            set
            {
                this.product.Name = value;
                this.OnPropertyChanged("Name");
            }
        }

        public Condition Condition
        {
            get
            {
                return this.product.Condition;
            }
            set
            {
                this.product.Condition = value;
                this.OnPropertyChanged("Condition");
            }
        }

        public ICollection<Condition> Conditions
        {
            get
            {
                return Enum.GetValues(typeof(Condition)) as ICollection<Condition>;
            }
        }

        public string Description
        {
            get
            {
                return this.product.Description;
            }
            set
            {
                this.product.Description = value;
                this.OnPropertyChanged("Description");
            }
        }

        public Product Product
        {
            get
            {
                return this.product;
            }
        }

        public decimal Price
        {
            get
            {
                return this.product.Price;
            }
            set
            {
                this.product.Price = value;
                this.OnPropertyChanged("Price");
            }
        }

        public int Quantity
        {
            get
            {
                return this.product.Quantity;
            }
            set
            {
                this.product.Quantity = value;
                this.OnPropertyChanged("Quantity");
            }
        }

        public Location Location
        {
            get
            {
                return this.product.Location;
            }
            set
            {
                this.product.Location = value;
                this.OnPropertyChanged("Location");
            }
        }

        public ICollection<Location> Locations
        {
            get
            {
                return this.repository.GetLocations();
            }
        }

        public IEnumerable<Category> Categories
        {
            get
            {
                return this.repository.GetCategories() as IEnumerable<Category>;
            }
        }

        /// <summary>
        /// Creates the commands needed for the product view model.
        /// </summary>
        protected override void CreateCommands()
        {
            this.Commands.Add(new CommandViewModel("OK", new DelegateCommand(p => this.OkExecute())));
            this.Commands.Add(new CommandViewModel("Cancel", new DelegateCommand(p => this.CancelExecute())));
        }

        private void Save()
        {
            // Add product to repository.
            this.repository.AddProduct(this.product);

            this.repository.SaveToDatabase();
        }

        private void OkExecute()
        {
            this.Save();
            this.CloseAction(true);
        }

        /// <summary>
        /// Closes the new Item window without saving.
        /// </summary>
        private void CancelExecute()
        {
            this.CloseAction(false);
        }
    }
}