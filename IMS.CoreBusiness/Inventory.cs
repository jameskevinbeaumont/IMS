using System.ComponentModel.DataAnnotations;

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
        [Required]
        [StringLength(150)]
        public string InventoryName { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be greater than or equal to 0.")]
        public int Quantity { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Price must be greater than or equal to 0.")]
        public double Price { get; set; }
    }
}
