using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.Plugins.InMemory
{
    // If you see a red squiggly line under the interface, it may mean that this 
    // class is not implementing all of the methods defined in the interface.
    // To fix - Ctrl+"." and hit enter on "Implement interface..." - after it implements
    // the method here, delete the "throw..." line and write the code to implement the method.
    public class ProductRepository : IProductRepository
    {
        private List<Product> _products;

        public ProductRepository()
        {
            // Loading _products List with some data - this is "in memory"
            _products =
            [
                new() { ProductID = 1, ProductName = "Bike", Quantity = 10, Price = 150 },
                new Product { ProductID = 2, ProductName = "Car", Quantity = 10, Price = 25000 }

            ];
        }

        public async Task<Product> GetProductByIDAsync(int ProductID)
        {
            return await Task.FromResult(_products.First(x => x.ProductID == ProductID));
        }

        public Task UpdateProductAsync(Product Product)
        {
            // Defensive programming - here we are checking if the name passed in matches the name
            // on an Product ID that is NOT the Product ID we are updating - we are not allowing
            // the user to change the name of the Product item they are updating with the same name
            // as another Product item that already exists.
            if (_products.Any(x => x.ProductID != Product.ProductID &&
                x.ProductName.Equals(Product.ProductName, StringComparison.OrdinalIgnoreCase)))
                return Task.CompletedTask;

            // We are looking for a match in the Product repository based on the Product ID.
            var prodToUpdate = _products.FirstOrDefault(x => x.ProductID == Product.ProductID);
            if (prodToUpdate != null)
            {
                prodToUpdate.ProductName = Product.ProductName;
                prodToUpdate.Quantity = Product.Quantity;
                prodToUpdate.Price = Product.Price;
            }

            return Task.CompletedTask;
        }

        public Task AddProductAsync(Product Product)
        {
            // Defensive programming - the Product passed may have the same name as one that
            // already exists, so we need to check for that.
            // So if it already exists, just return that the task is completed...
            if (_products.Any(x => x.ProductName.Equals(Product.ProductName, StringComparison.OrdinalIgnoreCase)))
                return Task.CompletedTask;

            // This in memory Product list does not generate ID's automatically so we have to
            // generate the new ID
            var maxID = _products.Max(x => x.ProductID);
            Product.ProductID = maxID + 1;

            //...otherwise, add the new Product
            _products.Add(Product);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
        {
            // If it's NULL or whitespace, we want to return everything...because it's "in memory" we can use .FromResult
            if (string.IsNullOrEmpty(name)) return await Task.FromResult(_products);

            // If the name is not empty (NULL) or whitspace, then we need to filter the list
            // .Where returns IEnumerable and that is what we want
            return _products.Where(x => x.ProductName.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        public Task DeleteProductByIDAsync(int ProductID)
        {
            var product = _products.FirstOrDefault(x => x.ProductID == ProductID);

            if (product != null)
            {
                _products.Remove(product);
            }

            return Task.CompletedTask;
        }
    }
}
