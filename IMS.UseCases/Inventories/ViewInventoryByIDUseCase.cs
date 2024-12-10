using IMS.CoreBusiness;
using IMS.UseCases.Inventories.Interfaces;
using IMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases.Inventories
{
    public class ViewInventoryByIDUseCase : IViewInventoryByIDUseCase
    {
        private readonly IInventoryRepository _inventoryRepository;

        public ViewInventoryByIDUseCase(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<Inventory> ExecuteAsync(int inventoryID)
        {
            return await _inventoryRepository.GetInventoryByIDAsync(inventoryID);
        }
    }
}
