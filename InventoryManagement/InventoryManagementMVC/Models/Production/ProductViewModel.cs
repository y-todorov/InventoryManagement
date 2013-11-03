using InventoryManagementMVC.DataAnnotations;
using RecipiesModelNS;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementMVC.Models
{
    public class ProductViewModel
    {
        [Key]
        public int ProductId { get; set; }

        [Relation(EntityType = typeof (UnitMeasure), DataFieldValue = "UnitMeasureId", DataFieldText = "Name")]
        [Display(Name = "UnitMeasure")]
        public int? UnitMeasureId { get; set; }

        [Relation(EntityType = typeof (ProductCategory), DataFieldValue = "CategoryId", DataFieldText = "Name")]
        [Display(Name = "Category")]
        public int? CategoryId { get; set; }

        [Relation(EntityType = typeof (Store), DataFieldValue = "StoreId", DataFieldText = "Name")]
        [Display(Name = "Store")]
        public int? StoreId { get; set; }

        [Required(ErrorMessage = "Please enter a name for the product!")]
        public string Name { get; set; }

        public string Code { get; set; }

        [Range(0, int.MaxValue)]
        public decimal? UnitPrice { get; set; }

        [Range(0, int.MaxValue)]
        public double? UnitsInStock { get; set; }

        [ReadOnly(true)]
        [Range(0, int.MaxValue)]
        public decimal? StockValue { get; set; }

        [Range(0, int.MaxValue)]
        public double? UnitsOnOrder { get; set; }

        [Range(0, int.MaxValue)]
        public double? ReorderLevel { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedByUser { get; set; }

        public static ProductViewModel ConvertFromProductEntity(Product newOrExistingProductEntity,
            ProductViewModel productViewModel)
        {
            if (newOrExistingProductEntity == null)
            {
                throw new ApplicationException("newOrExistingProductEntity is null in method ConvertFromProductEntity!");
            }
            if (productViewModel == null)
            {
                throw new ApplicationException("productViewModel is null in method ConvertFromProductEntity!");
            }
            productViewModel.ProductId = newOrExistingProductEntity.ProductId;
            productViewModel.UnitMeasureId = newOrExistingProductEntity.UnitMeasureId;
            productViewModel.CategoryId = newOrExistingProductEntity.CategoryId;
            productViewModel.StoreId = newOrExistingProductEntity.StoreId;
            productViewModel.Name = newOrExistingProductEntity.Name;
            productViewModel.Code = newOrExistingProductEntity.Code;
            productViewModel.UnitPrice = Math.Round(newOrExistingProductEntity.UnitPrice.GetValueOrDefault(), 3);
            productViewModel.UnitsInStock = Math.Round(newOrExistingProductEntity.UnitsInStock.GetValueOrDefault(), 3);
            productViewModel.UnitsOnOrder = Math.Round(newOrExistingProductEntity.UnitsOnOrder.GetValueOrDefault(), 3);
            productViewModel.ReorderLevel = Math.Round(newOrExistingProductEntity.ReorderLevel.GetValueOrDefault(), 3);
            productViewModel.StockValue = (decimal) newOrExistingProductEntity.StockValue;
            productViewModel.ModifiedDate = newOrExistingProductEntity.ModifiedDate;
            productViewModel.ModifiedByUser = newOrExistingProductEntity.ModifiedByUser;

            return productViewModel;
        }

        public static Product ConvertToProductEntity(ProductViewModel productViewModel,
            Product newOrExistingProductEntity)
        {
            if (newOrExistingProductEntity == null)
            {
                throw new ApplicationException("newOrExistingProductEntity is null in method ConvertToProductEntity!");
            }
            if (productViewModel == null)
            {
                throw new ApplicationException("productViewModel is null in method ConvertToProductEntity!");
            }
            newOrExistingProductEntity.ProductId = productViewModel.ProductId;
            newOrExistingProductEntity.UnitMeasureId = productViewModel.UnitMeasureId;
            newOrExistingProductEntity.CategoryId = productViewModel.CategoryId;
            newOrExistingProductEntity.StoreId = productViewModel.StoreId;
            newOrExistingProductEntity.Name = productViewModel.Name;
            newOrExistingProductEntity.Code = productViewModel.Code;
            newOrExistingProductEntity.UnitPrice = Math.Round(productViewModel.UnitPrice.GetValueOrDefault(), 3);
            newOrExistingProductEntity.UnitsInStock = Math.Round(productViewModel.UnitsInStock.GetValueOrDefault(), 3);
            newOrExistingProductEntity.UnitsOnOrder = Math.Round(productViewModel.UnitsOnOrder.GetValueOrDefault(), 3);
            newOrExistingProductEntity.ReorderLevel = Math.Round(productViewModel.ReorderLevel.GetValueOrDefault(), 3);
            newOrExistingProductEntity.StockValue = (double) productViewModel.StockValue.GetValueOrDefault();
            newOrExistingProductEntity.ModifiedDate = productViewModel.ModifiedDate;
            newOrExistingProductEntity.ModifiedByUser = productViewModel.ModifiedByUser;

            return newOrExistingProductEntity;
        }
    }
}