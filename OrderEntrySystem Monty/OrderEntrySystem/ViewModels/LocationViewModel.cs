using System.ComponentModel;
using System.Windows;
using OrderEntryDataAccess;
using OrderEntryEngine;

namespace OrderEntrySystem
{
    public class LocationViewModel : EntityViewModel<Location>
    {
        //private Location location;

        public LocationViewModel(Location location)
            : base("New location", location)
        {
            this.Entity = location;
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

        public Location Location
        {
            get
            {
                return this.Entity;
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

        [EntityControlAttribute(ControlType.TextBox, "Description: ", 2), EntityColumn(25, "Description", 1)]
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

        [EntityControlAttribute(ControlType.TextBox, "City: ", 3), EntityColumn(25, "City", 1)]
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

        [EntityControlAttribute(ControlType.TextBox, "State: ", 4), EntityColumn(25, "State", 1)]
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

        /// <summary>
        /// Creates the commands needed for the car view model.
        /// </summary>
        //protected override void CreateCommands()
        //{
        //    this.Commands.Add(new CommandViewModel("OK", new DelegateCommand(p => this.OkExecute()), true, false, "default"));
        //    this.Commands.Add(new CommandViewModel("Cancel", new DelegateCommand(p => this.CancelExecute()), false, true, "default"));
        //}

        //private bool Save()
        //{
        //    bool result = true;

        //    IRepository irepository = RepositoryManager.GetRepository(typeof(Location));
        //    Repository<Location> repository = (Repository<Location>)irepository;

        //    if (this.Location.IsValid)
        //    {
        //        // Add location to repository.
        //        repository.AddEntity(this.Entity);

        //        repository.SaveToDatabase();
        //    }
        //    else
        //    {
        //        MessageBox.Show("One or more properties are invalid. Location could not be saved.");
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

        ///// <summary>
        ///// Closes the new car window without saving.
        ///// </summary>
        //private void CancelExecute()
        //{
        //    this.CloseAction(false);
        //}
    }
}