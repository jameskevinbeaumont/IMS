using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.Plugins.InMemory
{
    public class InventoryRepository : IInventoryRepository
    {
        private List<Inventory> _inventories;

        public InventoryRepository()
        {
            // Loading _inventories List with some data - this is "in memory"
            _inventories =
            [
                new() { InventoryID = 1, InventoryName = "Bike Seat", Quantity = 10, Price = 2 },
                new Inventory { InventoryID = 1, InventoryName = "Bike Body", Quantity = 10, Price = 15 },
                new Inventory { InventoryID = 1, InventoryName = "Bike Wheels", Quantity = 20, Price = 8 },
                new Inventory { InventoryID = 1, InventoryName = "Bike Pedals", Quantity = 20, Price = 1 }

            ];
        }

        public async Task<IEnumerable<Inventory>> GetInventoriesByNameAsync(string name)
        {
            // If it's NULL or whitespace, we want to return everything...because it's "in memory" we can use .FromResult
            if (string.IsNullOrEmpty(name)) return await Task.FromResult(_inventories);

            // If the name is not empty (NULL) or whitspace, then we need to filter the list
            // .Where returns IEnumerable and that is what we want
            return _inventories.Where(x => x.InventoryName.Contains(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
