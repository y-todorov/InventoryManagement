using InventoryManagementMVC.DataAnnotations;
using RecipiesModelNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventoryManagementMVC.Models
{
    public class ProductInventoryViewModel : InventoryViewModel
    {
        [Key]
        public int InventoryId { get; set; }

        [Relation(EntityType = typeof (Product), DataFieldValue = "ProductId", DataFieldText = "Name")]
        [Display(Name = "Product")]
        public int? ProductId { get; set; }

        public static ProductInventoryViewModel ConvertFromProductInventoryEntity(
            ProductInventory newOrExistingProductInventoryEntity,
            ProductInventoryViewModel productInventoryViewModel)
        {
            InventoryViewModel.ConvertFromInventoryEntity(newOrExistingProductInventoryEntity, productInventoryViewModel);
            productInventoryViewModel.ProductId = newOrExistingProductInventoryEntity.ProductId;

            return productInventoryViewModel;
        }

        public static ProductInventory ConvertToProductInventoryEntity(
            ProductInventoryViewModel productInventoryViewModel, ProductInventory newOrExistingProductInventoryEntity)
        {
            InventoryViewModel.ConvertToInventoryEntity(productInventoryViewModel, newOrExistingProductInventoryEntity);
            newOrExistingProductInventoryEntity.ProductId = productInventoryViewModel.ProductId;

            return newOrExistingProductInventoryEntity;
        }
    }
}