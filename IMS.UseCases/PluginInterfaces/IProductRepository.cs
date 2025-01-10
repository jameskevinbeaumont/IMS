using IMS.CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases.PluginInterfaces
{
    public interface IProductRepository
    {
        Task AddProductAsync(Product product);
        Task DeleteProductByIDAsync(int productID);
        Task<IEnumerable<Product>> GetProductsByNameAsync(string name);
        Task<Product?> GetProductByIDAsync(int productID);
        Task UpdateProductAsync(Product product);
    }
}
