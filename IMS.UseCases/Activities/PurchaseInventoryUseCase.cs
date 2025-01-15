using IMS.CoreBusiness;
using IMS.UseCases.Activities.Interfaces;
using IMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases.Activities
{
    public class PurchaseInventoryUseCase : IPurchaseInventoryUseCase
    {
        private readonly IInventoryTransactionRepository _inventoryTransactionRepository;
        private readonly IInventoryRepository _inventoryRepository;

        // We don't have an inventory transaction repository yet but with the following
        // we can pretend that we have it.
        public PurchaseInventoryUseCase(IInventoryTransactionRepository inventoryTransactionRepository,
                                        IInventoryRepository inventoryRepository)
        {
            _inventoryTransactionRepository = inventoryTransactionRepository;
            _inventoryRepository = inventoryRepository;
        }
        public async Task ExecuteAsync(string poNumber, Inventory inventory, int quantity, string doneBy)
        {
            if (inventory != null)
            {
                // Insert a record in the transaction table
                _inventoryTransactionRepository.PurchaseAsync(poNumber, inventory, quantity, doneBy, inventory.Price);

                // Update the inventory (Increase the quantity)
                inventory.Quantity += quantity;
                await _inventoryRepository.UpdateInventoryAsync(inventory);
            }
        }
    }
}
