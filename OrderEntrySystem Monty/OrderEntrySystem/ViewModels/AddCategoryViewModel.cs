using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderEntryDataAccess;
using OrderEntryEngine;

namespace OrderEntrySystem
{
    public class AddCategoryViewModel : WorkspaceViewModel
    {
        private Bike product;

        public AddCategoryViewModel(Bike product)
            : base("Add category")
        {
            this.product = product;
        }

        public Category Category { get; set; }

        public IEnumerable<Category> Categories
        {
            get
            {
                IRepository irepository = RepositoryManager.GetRepository(typeof(Category));
                Repository<Category> repository = (Repository<Category>)irepository;

                return repository.GetEntities();
            }
        }

        /// <summary>
        /// Creates the commands needed for the add category view model.
        /// </summary>
        protected override void CreateCommands()
        {
            this.Commands.Add(new CommandViewModel("OK", new DelegateCommand(p => this.OkExecute()), true, false, "default"));
            this.Commands.Add(new CommandViewModel("Cancel", new DelegateCommand(p => this.CancelExecute()), false, true, "default"));
        }

        private void Save()
        {
            ProductCategory pc = new ProductCategory();

            IRepository irepository = RepositoryManager.GetRepository(typeof(ProductCategory));
            Repository<ProductCategory> repository = (Repository<ProductCategory>)irepository;

            pc.Category = this.Category;
            pc.Product = this.product;

            repository.AddEntity(pc);

            repository.SaveToDatabase();
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