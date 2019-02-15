namespace OrderEntryEngine
{
    public class ProductCategoryEventArgs
    {
        public ProductCategoryEventArgs(Category category, Product product)
        {
            this.Category = category;
            this.Product = product;
        }

        public Category Category { get; private set; }

        public Product Product { get; private set; }
    }
}