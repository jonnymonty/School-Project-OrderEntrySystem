using OrderEntryDataAccess;
using OrderEntryEngine;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OrderEntrySystem
{
    [Serializable]
    public class EntityViewModel<T> : WorkspaceViewModel where T : class, IEntity
    {
        /// <summary>
        /// An indicator of whether or not an car is selected.
        /// </summary>
        private bool isSelected;

        //private T entity;

        public EntityViewModel(string displayName, T entity)
            : base(displayName)
        {
            //this.entity = entity;
        }

        public T Entity { get; set; }

        protected override void CreateCommands()
        {
            this.Commands.Add(new CommandViewModel("OK", OK, true, false, "default"));
            this.Commands.Add(new CommandViewModel("Cancel", Cancel, false, true, "default"));
            //this.Commands.Add(new CommandViewModel("Export", Export, false, false, "default"));
        }

        //[EntityControl(ControlType.Button, "Export", 997)]
        //public ICommand Export
        //{
        //    get
        //    {
        //        return new DelegateCommand(p => this.ExportExecute());
        //    }
        //}

        private void OkExecute()
        {
            if (this.Save())
            {
                this.CloseAction(true);
            }
        }

        private void CancelExecute()
        {
            this.CloseAction(false);
        }

        /// <summary>
        /// Gets or sets a value indicating whether this car is selected in the UI.
        /// </summary>
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

        [EntityControl(ControlType.Button, "OK", 998)]
        public ICommand OK
        {
            get
            {
                return new DelegateCommand(p => this.OkExecute());
            }
        }

        [EntityControl(ControlType.Button, "Cancel", 999)]
        public ICommand Cancel
        {
            get
            {
                return new DelegateCommand(p => this.CancelExecute());
            }
        }

        //private void ExportExecute()
        //{
            
        //}

        private bool Save()
        {
            bool result = true;

            if (this.Entity != null)
            {
                IRepository irepository = RepositoryManager.GetRepository(ObjectContext.GetObjectType(Entity.GetType()));
                Repository<T> repository = (Repository<T>)irepository;

                repository.AddEntity(this.Entity);
                repository.SaveToDatabase();
            }
            else
            {
                result = false;
            }

            return result;
        }
    }
}