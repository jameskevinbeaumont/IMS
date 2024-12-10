using IMS.CoreBusiness;

namespace IMS.UseCases.Inventories.Interfaces
{
    public interface IViewInventoryByIDUseCase
    {
        Task<Inventory> ExecuteAsync(int inventoryID);
    }
}