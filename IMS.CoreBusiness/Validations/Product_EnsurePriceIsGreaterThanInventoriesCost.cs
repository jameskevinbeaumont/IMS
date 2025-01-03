using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.CoreBusiness.Validations
{
    // Custom validation using the ValidationAttribute (base class for all validation attributes in System.ComponentModel.DataAnnotations)
    // This can be applied to almost all ASP.NET Core applications including Blazor Web Assembly and Blazor Server, Razor Pages, MVC and WebAPI
    // This is a C# thing - doesn't need a specific framework
    public class Product_EnsurePriceIsGreaterThanInventoriesCost: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var product = validationContext.ObjectInstance as Product;
            if (product != null)
            {
                if (!ValidatePricing(product))
                    return new ValidationResult($"The product's price is less than the inventories cost: {TotalInventoriesCost(product).ToString("c")}!",
                                                new List<string> { validationContext.MemberName });
            }

            return ValidationResult.Success;
        }

        private double TotalInventoriesCost(Product product)
        {
            if (product == null || product.ProductInventories == null)
                return 0.0;

            return product.ProductInventories.Sum(x => x.Inventory?.Price * x.InventoryQuantity ?? 0.0);
        }

        private bool ValidatePricing(Product product)
        {
            if (product.ProductInventories == null || product.ProductInventories.Count <= 0)
                return true;

            if (TotalInventoriesCost(product) > product.Price)
                return false;

            return true;
        }
    }
}
