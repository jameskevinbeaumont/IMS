using System.ComponentModel.DataAnnotations;

namespace IMS.WebApp.ViewModels
{
    public class PurchaseViewModel
    {
        [Required(ErrorMessage = "PO Number is required.")]
        public string PONumber { get; set; } = string.Empty;

        [Range(minimum: 1, maximum:int.MaxValue, ErrorMessage = "You must select an inventory item.")]
        public int InventoryID { get; set; }

        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Quantity must be greater than or equal to 1.")]
        public int QuantityToPurchase { get; set; }

        public double InventoryPrice { get; set; }
    }
}
