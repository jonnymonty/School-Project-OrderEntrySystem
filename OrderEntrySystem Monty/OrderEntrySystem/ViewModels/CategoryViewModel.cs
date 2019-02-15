using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using OrderEntryDataAccess;
using OrderEntryEngine;

namespace OrderEntrySystem
{
    public class CategoryViewModel : EntityViewModel<Category>
    {
        //private Category category;

        //private bool isSelected;

        public CategoryViewModel(Category category)
            : base("New product category", category)
        {
            this.Entity = category;
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

        public Category Category
        {
            get
            {
                return this.Entity;
            }
        }

        //public bool IsSelected
        //{
        //    get
        //    {
        //        return this.isSelected;
        //    }
        //    set
        //    {
        //        this.isSelected = value;
        //        this.OnPropertyChanged("IsSelected");
        //    }
        //}

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

        /// <summary>
        /// Creates the commands needed for the product category view model.
        /// </summary>
        //protected override void CreateCommands()
        //{
        //    this.Commands.Add(new CommandViewModel("OK", new DelegateCommand(p => this.OkExecute()), true, false, "default"));
        //    this.Commands.Add(new CommandViewModel("Cancel", new DelegateCommand(p => this.CancelExecute()), false, true, "default"));
        //}

        //private bool Save()
        //{
        //    bool result = true;

        //    IRepository irepository = RepositoryManager.GetRepository(typeof(Category));
        //    Repository<Category> repository = (Repository<Category>)irepository;

        //    if (this.Category.IsValid)
        //    {
        //        // Add product category to repository.
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

        //private void OkExecute()
        //{
        //    if (this.Save())
        //    {
        //        this.CloseAction(true);
        //    }
        //}

        /// <summary>
        /// Closes the window without saving.
        /// </summary>
        //private void CancelExecute()
        //{
        //    this.CloseAction(false);
        //}
    }
}