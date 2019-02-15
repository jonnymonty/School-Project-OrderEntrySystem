using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderEntryDataAccess;
using OrderEntryEngine;

namespace OrderEntrySystem
{
    public class CategoryViewModel : WorkspaceViewModel
    {
        private Category category;

        private Repository repository;

        private bool isSelected;

        public CategoryViewModel(Category category, Repository repository)
            : base("New product category")
        {
            this.category = category;
            this.repository = repository;
        }

        public Category Category
        {
            get
            {
                return this.category;
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
                return this.category.Name;
            }
            set
            {
                this.category.Name = value;
                this.OnPropertyChanged("Name");
            }
        }

        /// <summary>
        /// Creates the commands needed for the product category view model.
        /// </summary>
        protected override void CreateCommands()
        {
            this.Commands.Add(new CommandViewModel("OK", new DelegateCommand(p => this.OkExecute())));
            this.Commands.Add(new CommandViewModel("Cancel", new DelegateCommand(p => this.CancelExecute())));
        }

        private void Save()
        {
            // Add product category to repository.
            this.repository.AddCategory(this.category);

            this.repository.SaveToDatabase();
        }

        private void OkExecute()
        {
            this.Save();
            this.CloseAction(true);
        }

        /// <summary>
        /// Closes the window without saving.
        /// </summary>
        private void CancelExecute()
        {
            this.CloseAction(false);
        }
    }
}