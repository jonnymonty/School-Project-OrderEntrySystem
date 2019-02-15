using OrderEntryDataAccess;
using OrderEntryEngine;
using OrderEntrySystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OrderEntrySystem
{
    public class MultiEntityViewModel<TEntity, TViewModel, TView> : WorkspaceViewModel, IMultiEntityViewModel where TEntity : class, IEntity where TViewModel : EntityViewModel<TEntity> where TView : System.Windows.Controls.UserControl
    {
        private Repository<TEntity> repository;

        public MultiEntityViewModel()
            : base("All " + typeof(TEntity).Name + "s")
        {
            this.repository = (Repository<TEntity>)RepositoryManager.GetRepository(typeof(TEntity));
            this.CreateAllViewModels();

            repository.EntityAdded += this.OnEntityAdded;
            repository.EntityRemoved += this.OnEntityRemoved;
        }

        public ObservableCollection<TViewModel> AllEntities { get; set; }

        public int NumberOfItemsSelected
        {
            get
            {
                return this.AllEntities.Count(vm => vm.IsSelected);
            }
        }

        public Type Type
        {
            get
            {
                return typeof(TViewModel);
            }
        }

        public void AddPropertyChangedEvent(List<TViewModel> entities)
        {
            (entities as List<TViewModel>).ForEach(entity => entity.PropertyChanged += this.OnEntityViewModelPropertyChanged);
        }

        protected override void CreateCommands()
        {
            this.Commands.Add(new CommandViewModel("New...", new DelegateCommand(param => this.CreateNewEntityExecute()), "createEntity"));
            this.Commands.Add(new CommandViewModel("Edit...", new DelegateCommand(param => this.EditEntityExecute(), p => this.NumberOfItemsSelected == 1), "createEntity"));
            this.Commands.Add(new CommandViewModel("Delete", new DelegateCommand(param => this.DeleteEntityExecute(), p => this.NumberOfItemsSelected == 1), "createEntity"));
        }

        private void CreateAllViewModels()
        {
            List<TViewModel> entities =
                (from e in this.repository.GetEntities()
                select Activator.CreateInstance(typeof(TViewModel), e) as TViewModel).ToList();

                this.AllEntities = new ObservableCollection<TViewModel>(entities);
        }

        private void CreateNewEntityExecute()
        {
            IEntity IEntity = Activator.CreateInstance(typeof(TEntity)) as TEntity;
            TViewModel viewModel = Activator.CreateInstance(typeof(TViewModel), IEntity) as TViewModel;
            ShowEntity(viewModel);
        }

        private void EditEntityExecute()
        {
            TViewModel viewModel = AllEntities.Single(vm => vm.IsSelected);
            if (viewModel != null)
            {
                ShowEntity(viewModel);
                repository.SaveToDatabase();
            }
        }

        private void DeleteEntityExecute()
        {
            TViewModel viewModel = AllEntities.Single(vm => vm.IsSelected);

            if (viewModel != null)
            {
                if (MessageBox.Show("Do you really want to delete the selected Item?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    repository.RemoveEntity(viewModel.Entity);
                    repository.SaveToDatabase();
                }
            }
            else
            {
                MessageBox.Show("Please select only one Item");
            }
        }

        private void ShowEntity(TViewModel viewModel)
        {
            WorkspaceWindow window = new WorkspaceWindow();
            window.Width = 400;
            viewModel.CloseAction = b => window.DialogResult = b;
            window.Title = viewModel.DisplayName;

            TView view = (TView)Activator.CreateInstance(typeof(TView));

            view.DataContext = viewModel;

            window.Content = view;

            window.ShowDialog();
        }

        private void OnEntityAdded(object sender, EntityEventArgs<TEntity> e)
        {
            TViewModel vm = (TViewModel)Activator.CreateInstance(ObjectContext.GetObjectType(e.GetType()));
            vm.PropertyChanged += this.OnEntityViewModelPropertyChanged;

            this.AllEntities.Add(vm);
        }

        private void OnEntityRemoved(object sender, EntityEventArgs<TEntity> e)
        {
            TViewModel viewModel = AllEntities.Single(vm => vm.IsSelected);
            if (viewModel != null)
            {
                if (viewModel.Entity == e.Item)
                {
                    this.AllEntities.Remove(viewModel);
                }
            }
        }

        private void OnEntityViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            string isSelected = "IsSelected";

            if (e.PropertyName == isSelected)
            {
                this.OnPropertyChanged("NumberOfItemsSelected");
            }
        }
    }
}