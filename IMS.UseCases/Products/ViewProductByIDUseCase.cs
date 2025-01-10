using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases.Products
{
    public class ViewProductByIDUseCase : IViewProductByIDUseCase
    {
        private readonly IProductRepository _productRepository;

        public ViewProductByIDUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product?> ExecuteAsync(int productID)
        {
            return await _productRepository.GetProductByIDAsync(productID);
        }
    }
}
