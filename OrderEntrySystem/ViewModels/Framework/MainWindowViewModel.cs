using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using OrderEntryDataAccess;
using OrderEntryEngine;
using System.Data.Entity;
using System.Collections.Generic;
using System.Windows.Controls;
using OrderEntrySystem;
using OrderEntrySystem.Views;

namespace OrderEntrySystem
{
    public class MainWindowViewModel : WorkspaceViewModel
    {
        private ObservableCollection<UserControl> views;

        public MainWindowViewModel()
            : base("Order Entry System - Monty")
        {
            RepositoryManager.InitializeRepository();
        }

        public ObservableCollection<UserControl> Views
        {
            get
            {
                if (this.views == null)
                {
                    this.views = new ObservableCollection<UserControl>();
                }

                return this.views;
            }
        }

        /// <summary>
        /// Creates the commands required by the view model.
        /// </summary>
        protected override void CreateCommands()
        {
            AddMultiEntityCommand("Products");
            AddMultiEntityCommand("Customers");
            AddMultiEntityCommand("Locations");
            AddMultiEntityCommand("Categories");
            AddMultiEntityCommand("Orders");
            //this.Commands.Add(new CommandViewModel("View all products", new DelegateCommand(p => this.ShowAllProducts()), "products"));
            //this.Commands.Add(new CommandViewModel("View all customers", new DelegateCommand(p => this.ShowAllEntities()), "customers"));
            //this.Commands.Add(new CommandViewModel("View all locations", new DelegateCommand(p => this.ShowAllLocations()), "locations"));
            //this.Commands.Add(new CommandViewModel("View all categories", new DelegateCommand(p => this.ShowAllProductCategories()), "categories"));
            //this.Commands.Add(new CommandViewModel("View all orders", new DelegateCommand(p => this.ShowAllOrders()), "orders"));
            //this.Commands.Add(new CommandViewModel("View reports", new DelegateCommand(p => this.ShowReport()), "reports"));
        }

        //private void ShowAllProducts()
        //{
        //    MultiProductViewModel viewModel = this.ViewModels.FirstOrDefault(vm => vm is MultiProductViewModel) as MultiProductViewModel;

        //    if (viewModel == null)
        //    {
        //        viewModel = new MultiProductViewModel();

        //        viewModel.RequestClose += this.OnWorkspaceRequestClose;

        //        this.ViewModels.Add(viewModel);
        //    }

        //    this.ActivateViewModel(viewModel);
        //}

        private void AddMultiEntityCommand(string displayName)
        {
            Commands.Add(new CommandViewModel(displayName, new DelegateCommand(p => this.ShowAllEntities("View all " + displayName)), "Entity Commands"));
        }

        private void ShowAllEntities(string displayName)
        {
            UserControl view = this.Views.FirstOrDefault(v => (v.DataContext as IMultiEntityViewModel).DisplayName == displayName);

            if (view == null)
            {
                IMultiEntityViewModel viewModel = null;

                switch (displayName)
                {
                    case "View all Products":
                        viewModel = new MultiEntityViewModel<Bike, ProductViewModel, EntityView>();
                        break;
                    case "View all Customers":
                        viewModel = new MultiEntityViewModel<Customer, CustomerViewModel, EntityView>();
                        break;
                    case "View all Locations":
                        viewModel = new MultiEntityViewModel<Location, LocationViewModel, EntityView>();
                        break;
                    case "View all Categories":
                        viewModel = new MultiEntityViewModel<Category, CategoryViewModel, EntityView>();
                        break;
                    case "View all Orders":
                        viewModel = new MultiEntityViewModel<Order, OrderViewModel, EntityView>();
                        break;
                }

                viewModel.RequestClose += this.OnWorkspaceRequestClose;

                view = new MultiEntityView();

                view.DataContext = viewModel;

                this.Views.Add(view);
            }
            this.ActivateViewModel(view);
        }

        //private void ShowAllLocations()
        //{
        //    MultiLocationViewModel viewModel = this.ViewModels.FirstOrDefault(vm => vm is MultiLocationViewModel) as MultiLocationViewModel;

                        //    if (viewModel == null)
                        //    {
                        //        viewModel = new MultiLocationViewModel();

                        //        viewModel.RequestClose += this.OnWorkspaceRequestClose;

                        //        this.ViewModels.Add(viewModel);
                        //    }

                        //    this.ActivateViewModel(viewModel);
                        //}

                        //private void ShowAllProductCategories()
                        //{
                        //    MultiCategoryViewModel viewModel = this.ViewModels.FirstOrDefault(vm => vm is MultiCategoryViewModel) as MultiCategoryViewModel;

                        //    if (viewModel == null)
                        //    {
                        //        viewModel = new MultiCategoryViewModel(null);

                        //        viewModel.RequestClose += this.OnWorkspaceRequestClose;

                        //        this.ViewModels.Add(viewModel);
                        //    }

                        //    this.ActivateViewModel(viewModel);
                        //}

                        //private void ShowAllOrders()
                        //{
                        //    MultiOrderViewModel viewModel = this.ViewModels.FirstOrDefault(vm => vm is MultiOrderViewModel) as MultiOrderViewModel;

                        //    if (viewModel == null)
                        //    {
                        //        viewModel = new MultiOrderViewModel(new Customer());

                        //        viewModel.RequestClose += this.OnWorkspaceRequestClose;

                        //        this.ViewModels.Add(viewModel);
                        //    }

                        //    this.ActivateViewModel(viewModel);
                        //}

                        //private void ShowReport()
                        //{
                        //    ReportViewModel viewModel = this.ViewModels.FirstOrDefault(vm => vm is ReportViewModel) as ReportViewModel;

                        //    if (viewModel == null)
                        //    {
                        //        viewModel = new ReportViewModel();

                        //        viewModel.RequestClose += this.OnWorkspaceRequestClose;

                        //        this.ViewModels.Add(viewModel);
                        //    }

                        //    this.ActivateViewModel(viewModel);
                        //}

                        /// <summary>
                        /// A handler which responds to a request to close a workspace.
                        /// </summary>
                        /// <param name="sender">The object that initiated the event.</param>
                        /// <param name="e">The arguments for the event.</param>
        private void OnWorkspaceRequestClose(object sender, EventArgs e)
        {
            UserControl viewModel = sender as UserControl;

            this.Views.Remove(viewModel);
        }

        private void ActivateViewModel(UserControl view)
        {
            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.Views);

            if (collectionView != null)
            {
                collectionView.MoveCurrentTo(view);
            }
        }
    }
}