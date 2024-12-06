namespace IMS.CoreBusiness
{
    public class Inventory
    {
        public int InventoryID { get; set; }
        /// <summary>
        /// InventoryName cannot be Null so we cannot use string?
        /// and therefore we will initialize this with string.Empty
        /// to avoid a potential null reference exception
        /// </summary>
        public string InventoryName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
