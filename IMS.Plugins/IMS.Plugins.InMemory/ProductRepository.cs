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

        public async Task<Product?> GetProductByIDAsync(int productID)
        {
            // When we get the product from the IN MEMORY repository, we are directly returning
            // the instance in that list, and because that specific product is BOUND to the user
            // interface. So as soon as the user changes anything in the user interface for this
            // specific product, it is IMMEDIATELY updated inside the in memory repository...
            var prod = _products.FirstOrDefault(x => x.ProductID == productID);
            // ...so that is why starting here we have to create a new instance of the product
            // object, assign all of the values to it, and return that instance back to the 
            // user interface.
            // Once we implement Entity Framework Core, we will be connecting to a real database
            // and this will no longer be an issue. All of THIS code is because we are using
            // an IN MEMORY data store.
            var newProd = new Product();
            if (prod != null)
            {
                newProd.ProductID = prod.ProductID;
                newProd.ProductName = prod.ProductName;
                newProd.Price = prod.Price;
                newProd.Quantity = prod.Quantity;
                newProd.ProductInventories = new List<ProductInventory>();
                if (prod.ProductInventories != null && prod.ProductInventories.Count > 0)
                {
                    foreach(var prodInv in prod.ProductInventories)
                    {
                        var newProdInv = new ProductInventory
                        {
                            InventoryID = prodInv.InventoryID,
                            ProductID = prodInv.ProductID,
                            Product = prod,
                            Inventory = new Inventory(),
                            InventoryQuantity = prodInv.InventoryQuantity
                        };
                        if (prodInv.Inventory != null)
                        {
                            newProdInv.Inventory.InventoryID = prodInv.Inventory.InventoryID;
                            newProdInv.Inventory.InventoryName = prodInv.Inventory.InventoryName;
                            newProdInv.Inventory.Price = prodInv.Inventory.Price;
                            newProdInv.Inventory.Quantity = prodInv.Inventory.Quantity;
                        }

                        newProd.ProductInventories.Add(newProdInv);
                    }
                }
            }

            return await Task.FromResult(newProd);
        }
        public Task UpdateProductAsync(Product product)
        {
            // Defensive programming - here we are checking if the name passed in matches the name
            // on an Product ID that is NOT the Product ID we are updating - we are not allowing
            // the user to change the name of the Product item they are updating with the same name
            // as another Product item that already exists.
            if (_products.Any(x => x.ProductID != product.ProductID &&
                x.ProductName.ToLower() == product.ProductName.ToLower()))
                return Task.CompletedTask;

            // We are looking for a match in the Product repository based on the Product ID.
            var prodToUpdate = _products.FirstOrDefault(x => x.ProductID == product.ProductID);
            if (prodToUpdate != null)
            {
                prodToUpdate.ProductName = product.ProductName;
                prodToUpdate.Price = product.Price;
                prodToUpdate.Quantity = product.Quantity;
                prodToUpdate.ProductInventories = product.ProductInventories;
            }

            return Task.CompletedTask;
        }

        public Task AddProductAsync(Product product)
        {
            // Defensive programming - the Product passed may have the same name as one that
            // already exists, so we need to check for that.
            // So if it already exists, just return that the task is completed...
            if (_products.Any(x => x.ProductName.Equals(product.ProductName, StringComparison.OrdinalIgnoreCase)))
                return Task.CompletedTask;

            // This in memory Product list does not generate ID's automatically so we have to
            // generate the new ID
            var maxID = _products.Max(x => x.ProductID);
            product.ProductID = maxID + 1;

            //...otherwise, add the new Product
            _products.Add(product);
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
