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
    public class AddInventoryUseCase : IAddInventoryUseCase
    {
        private readonly IInventoryRepository _inventoryRepository;

        // This is constructor dependency injection
        public AddInventoryUseCase(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        // Typically a use case has ONE public method, that's it.
        // If you find your use case containing more that a single public
        // method, you probably need to further breakdown your use case.
        // The use case CAN contain private helper methods that are called
        // from withing the public method.
        public async Task ExecuteAsync(Inventory inventory)
        {
            // Complicated business logic ALWAYS goes inside ExecuteAsync method
            // This is a simple pass through but we need to conform to proper principles
            // _inventoryRepository is a Data Access Layer (DAL) and therefore should NOT
            // be dependency injected into a Razor Component - We are following the single
            // responsibility principle so DAL needs to stay in the DAL - if we put this
            // into the Razor Component then all of the business logic associated with it
            // will also be contained within the Razor Component, and we don't want that.

            await _inventoryRepository.AddInventoryAsync(inventory);
        }
    }
}
