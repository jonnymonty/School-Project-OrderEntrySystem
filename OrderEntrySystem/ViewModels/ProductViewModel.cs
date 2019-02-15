using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using OrderEntryDataAccess;
using OrderEntryEngine;
using OrderEntrySystem.Views;
using System.Windows.Input;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace OrderEntrySystem
{
    [Serializable]
    public class ProductViewModel : EntityViewModel<Bike>
    {
        //private Product product;
        private MultiEntityViewModel<Category, CategoryViewModel, EntityView> filteredCategoryViewModel;

        public ProductViewModel(Bike product)
            : base("New product", product)
        {
            this.Entity = product;
            this.filteredCategoryViewModel = new MultiEntityViewModel<Category, CategoryViewModel, EntityView>();
            this.filteredCategoryViewModel.AllEntities = this.FilteredCategories;
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

        public MultiEntityViewModel<Category, CategoryViewModel, EntityView> FilteredCategoryViewModel
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

                if (this.Entity.ProductCategories != null)
                {
                    categories =
                        (from c in this.Entity.ProductCategories
                        select new CategoryViewModel(c.Category)).ToList();
                }

                this.FilteredCategoryViewModel.AddPropertyChangedEvent(categories);

                return new ObservableCollection<CategoryViewModel>(categories);
            }
        }

        [EntityControlAttribute(ControlType.TextBox, "Name: ", 1), EntityColumn(25, "Name", 1)]
        public string Name
        {
            get
            {
                return this.Entity.Name;
            }
            set
            {
                this.Entity.Name = value;
                this.OnPropertyChanged("Name");
            }
        }

        [EntityControlAttribute(ControlType.ComboBox, "Condition: ", 2), EntityColumn(25, "Condition", 1)]
        public OrderEntryEngine.Condition Condition
        {
            get
            {
                return this.Entity.Condition;
            }
            set
            {
                this.Entity.Condition = value;
                this.OnPropertyChanged("Condition");
            }
        }

        public ICollection<OrderEntryEngine.Condition> Conditions
        {
            get
            {
                return Enum.GetValues(typeof(OrderEntryEngine.Condition)) as ICollection<OrderEntryEngine.Condition>;
            }
        }

        [EntityControlAttribute(ControlType.TextBox, "Description: ", 3)]
        public string Description
        {
            get
            {
                return this.Entity.Description;
            }
            set
            {
                this.Entity.Description = value;
                this.OnPropertyChanged("Description");
            }
        }

        public Bike Product
        {
            get
            {
                return this.Entity;
            }
        }

        [EntityControlAttribute(ControlType.TextBox, "Price: ", 4), EntityColumn(25, "Price", 1)]
        public decimal Price
        {
            get
            {
                return this.Entity.Price;
            }
            set
            {
                this.Entity.Price = value;
                this.OnPropertyChanged("Price");
            }
        }

        [EntityControlAttribute(ControlType.Label, "Extended Price: ", 5), EntityColumn(25, "ExtendedPrice", 1)]
        public string ExtendedPrice
        {
            get
            {
                return this.Entity.ExtendedPrice;
            }
        }

        [EntityControlAttribute(ControlType.ComboBox, "Location: ", 6), EntityColumn(25, "Location", 1)]
        public Location Location
        {
            get
            {
                return this.Entity.Location;
            }
            set
            {
                this.Entity.Location = value;
                this.OnPropertyChanged("Location");
            }
        }

        [EntityControlAttribute(ControlType.ComboBox, "Brand: ", 7), EntityColumn(25, "Brand", 1)]
        public Brand Brand
        {
            get
            {
                return this.Entity.Brand;
            }
            set
            {
                this.Entity.Brand = value;
                this.OnPropertyChanged("Brand");
            }
        }

        [EntityControlAttribute(ControlType.TextBox, "Quantity: ", 8), EntityColumn(25, "Quantity", 1)]
        public int Quantity
        {
            get
            {
                return this.Entity.Quantity;
            }
            set
            {
                this.Entity.Quantity = value;
            }
        }

        [EntityControl(ControlType.Button, "Export", 997)]
        public ICommand Export
        {
            get
            {
                return new DelegateCommand(p => this.ExportExecute());
            }
        }

        public ICommand OkButton
        {
            get
            {
                return new DelegateCommand(p => this.OkExecute());
            }
        }

        public ICommand CancelButton
        {
            get
            {
                return new DelegateCommand(p => this.CancelExecute());
            }
        }

        public IEnumerable<Location> Locations
        {
            get
            {
                IRepository irepository = RepositoryManager.GetRepository(typeof(Location));
                Repository<Location> repository = (Repository<Location>)irepository;

                return repository.GetEntities() as IEnumerable<Location>;
            }
        }

        public IEnumerable<Brand> Brands
        {
            get
            {
                IRepository irepository = RepositoryManager.GetRepository(typeof(Brand));
                Repository<Brand> repository = (Repository<Brand>)irepository;

                return repository.GetEntities() as IEnumerable<Brand>;
            }
        }

        public IEnumerable<Category> Categories
        {
            get
            {
                IRepository irepository = RepositoryManager.GetRepository(typeof(Category));
                Repository<Category> repository = (Repository<Category>)irepository;

                return repository.GetEntities() as IEnumerable<Category>;
            }
        }

        /// <summary>
        /// Creates the commands needed for the product view model.
        /// </summary>
        protected override void CreateCommands()
        {
            this.Commands.Add(new CommandViewModel("OK", OkButton, true, false, "default"));
            this.Commands.Add(new CommandViewModel("Cancel", CancelButton, false, true, "default"));
            this.Commands.Add(new CommandViewModel("Export", Export, false, false, "default"));
        }

        private bool Save()
        {
            bool result = true;

            IRepository irepository = RepositoryManager.GetRepository(typeof(Bike));
            Repository<Bike> repository = (Repository<Bike>)irepository;

            if (this.Entity.IsValid)
            {
                // Add product to repository.
                repository.AddEntity(this.Entity);

                repository.SaveToDatabase();
            }
            else
            {
                result = false;
                MessageBox.Show("One or more fields are invalid. The product could not be saved.");
            }

            return result;
        }

        private void ExportExecute()
        {
            string sourceDirectory = @"C:\StartLocation";
            string fileName = this.Product.Name;
            IFormatter serializer = new BinaryFormatter();
            FileStream saveFile = new FileStream(sourceDirectory + "\\" + fileName + ".bin", FileMode.Create, FileAccess.Write);
            serializer.Serialize(saveFile, this.Product);
            saveFile.Close();
        }

        private void OkExecute()
        {
            if (this.Save())
            {
                this.CloseAction(true);
            }
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