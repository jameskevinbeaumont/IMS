using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.Plugins.InMemory
{
    // If you see a red squiggly line under the interface, it may mean that we are not
    // this class is not implementing all of the methods defined in the interface.
    // To fix - Ctrl+"." and hit enter on "Implement interface" - after it implements
    // the method here, delete the "throw..." line and write the code to implement the method.
    public class InventoryRepository : IInventoryRepository
    {
        private List<Inventory> _inventories;

        public InventoryRepository()
        {
            // Loading _inventories List with some data - this is "in memory"
            _inventories =
            [
                new() { InventoryID = 1, InventoryName = "Bike Seat", Quantity = 10, Price = 2 },
                new Inventory { InventoryID = 2, InventoryName = "Bike Body", Quantity = 10, Price = 15 },
                new Inventory { InventoryID = 3, InventoryName = "Bike Wheels", Quantity = 20, Price = 8 },
                new Inventory { InventoryID = 4, InventoryName = "Bike Pedals", Quantity = 20, Price = 1 }

            ];
        }

        public async Task<Inventory> GetInventoryByIDAsync(int inventoryID)
        {
            return await Task.FromResult(_inventories.First(x => x.InventoryID == inventoryID));
        }
        public Task UpdateInventoryAsync(Inventory inventory)
        {
            // Defensive programming - here we are checking if the name passed in matches the name
            // on an inventory ID that is NOT the inventory ID we are updating - we are not allowing
            // the user to change the name of the inventory item they are updating with the same name
            // as another inventory item that already exists.
            if (_inventories.Any(x => x.InventoryID != inventory.InventoryID && 
                x.InventoryName.Equals(inventory.InventoryName, StringComparison.OrdinalIgnoreCase)))
                return Task.CompletedTask;

            // We are looking for a match in the inventory repository based on the inventory ID.
            var invToUpdate = _inventories.FirstOrDefault(x => x.InventoryID == inventory.InventoryID);
            if (invToUpdate != null)
            {
                invToUpdate.InventoryName = inventory.InventoryName;
                invToUpdate.Quantity = inventory.Quantity;
                invToUpdate.Price = inventory.Price;
            }

            return Task.CompletedTask;
        }

        public Task AddInventoryAsync(Inventory inventory)
        {
            // Defensive programming - the inventory passed may have the same name as one that
            // already exists, so we need to check for that.
            // So if it already exists, just return that the task is completed...
            if (_inventories.Any(x => x.InventoryName.Equals(inventory.InventoryName, StringComparison.OrdinalIgnoreCase))) 
                return Task.CompletedTask;

            // This in memory inventory list does not generate ID's automatically so we have to
            // generate the new ID
            var maxID = _inventories.Max(x => x.InventoryID);
            inventory.InventoryID = maxID + 1;

            //...otherwise, add the new inventory
            _inventories.Add(inventory);
            return Task.CompletedTask;
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
