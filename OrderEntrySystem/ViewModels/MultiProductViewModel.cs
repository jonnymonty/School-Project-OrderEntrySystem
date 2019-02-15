using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using OrderEntryDataAccess;
using OrderEntryEngine;

namespace OrderEntrySystem
{
    public class MultiProductViewModel : WorkspaceViewModel
    {
        private Repository repository;

        public MultiProductViewModel(Repository repository)
            : base("All products")
        {
            this.repository = repository;

            List<ProductViewModel> products =
                (from item in this.repository.GetProducts()
                select new ProductViewModel(item, this.repository)).ToList();

            this.AddPropertyChangedEvent(products);

            this.AllProducts = new ObservableCollection<ProductViewModel>(products);

            this.repository.ProductAdded += this.OnProductAdded;
            this.repository.ProductRemoved += this.OnProductRemoved;
        }

        public ObservableCollection<ProductViewModel> AllProducts { get; set; }

        public int NumberOfItemsSelected
        {
            get
            {
                return this.AllProducts.Count(vm => vm.IsSelected);
            }
        }

        public void AddPropertyChangedEvent(List<ProductViewModel> products)
        {
            products.ForEach(pvm => pvm.PropertyChanged += this.OnProductViewModelPropertyChanged);
        }

        protected override void CreateCommands()
        {
            this.Commands.Add(new CommandViewModel("New...", new DelegateCommand(param => this.CreateNewProductExecute())));
            this.Commands.Add(new CommandViewModel("Edit...", new DelegateCommand(param => this.EditProductExecute(), p => this.NumberOfItemsSelected == 1)));
            this.Commands.Add(new CommandViewModel("Delete", new DelegateCommand(param => this.DeleteProductExecute(), p => this.NumberOfItemsSelected == 1)));
        }

        private void OnProductAdded(object sender, ProductEventArgs e)
        {
            ProductViewModel vm = new ProductViewModel(e.Product, this.repository);
            vm.PropertyChanged += this.OnProductViewModelPropertyChanged;

            this.AllProducts.Add(vm);
        }

        /// <summary>
        /// A handler which responds when a product view model's property changes.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnProductViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            string isSelected = "IsSelected";

            if (e.PropertyName == isSelected)
            {
                this.OnPropertyChanged("NumberOfItemsSelected");
            }
        }

        private void CreateNewProductExecute()
        {
            Product product = new Product();

            ProductViewModel viewModel = new ProductViewModel(product, this.repository);

            this.ShowProduct(viewModel);
        }

        private void EditProductExecute()
        {
            ProductViewModel viewModel = this.GetOnlySelectedViewModel();

            if (viewModel != null)
            {
                this.ShowProduct(viewModel);

                this.repository.SaveToDatabase();
            }
            else
            {
                MessageBox.Show("Please select only one product.");
            }
        }

        private void DeleteProductExecute()
        {
            ProductViewModel viewModel = this.GetOnlySelectedViewModel();

            if (viewModel != null)
            {
                if (MessageBox.Show("Do you really want to delete the selected Item?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    this.repository.RemoveProduct(viewModel.Product);
                    this.repository.SaveToDatabase();
                }
            }
            else
            {
                MessageBox.Show("Please select only one Item");
            }
        }

        private ProductViewModel GetOnlySelectedViewModel()
        {
            ProductViewModel result;

            try
            {
                result = this.AllProducts.Single(vm => vm.IsSelected);
            }
            catch
            {
                result = null;
            }

            return result;
        }

        /// <summary>
        /// Creates a new window to edit a car.
        /// </summary>
        /// <param name="viewModel">The view model for the car to be edited.</param>
        private void ShowProduct(WorkspaceViewModel viewModel)
        {
            WorkspaceWindow window = new WorkspaceWindow();
            window.Width = 400;
            viewModel.CloseAction = b => window.DialogResult = b;
            window.Title = viewModel.DisplayName;

            ProductView view = new ProductView();

            view.DataContext = viewModel;

            window.Content = view;

            window.ShowDialog();
        }

        private void OnProductRemoved(object sender, ProductEventArgs e)
        {
            ProductViewModel viewModel = this.GetOnlySelectedViewModel();
            if (viewModel != null)
            {
                if (viewModel.Product == e.Product)
                {
                    this.AllProducts.Remove(viewModel);
                }
            }
        }
    }
}