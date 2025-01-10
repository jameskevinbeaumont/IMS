using IMS.CoreBusiness;

namespace IMS.UseCases.Products
{
    public interface IViewProductByIDUseCase
    {
        Task<Product?> ExecuteAsync(int productID);
    }
}