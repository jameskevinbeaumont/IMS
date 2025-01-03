using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IMS.CoreBusiness
{
    public class ProductInventory
    {
        public int ProductID { get; set; }

        // This is just a navigation property and doesn't need to be serialized
        [JsonIgnore]
        public Product? Product { get; set; }

        public int InventoryID { get; set; }

        // This is just a navigation property and doesn't need to be serialized
        [JsonIgnore]
        public Inventory? Inventory { get; set; }

        public int InventoryQuantity { get; set; }
    }
}
