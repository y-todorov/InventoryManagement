using RecipiesModelNS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventoryManagementMVC.Models
{
    public class ProductViewModel
    {
        [Key]
        public int ProductId { get; set; }

        [Association("", "", "")]
        public int? UnitMeasureId { get; set; }

        [Association("", "", "")]
        public int? CategoryId { get; set; }

        [Association("", "", "")]
        public int? StoreId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Code { get; set; }

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

        public static ProductViewModel ConvertFromProductEntity(Product p)
        {
            ProductViewModel productViewModel = new ProductViewModel()
            {
                ProductId = p.ProductId,
                UnitMeasureId = p.UnitMeasureId,
                CategoryId = p.CategoryId,
                StoreId = p.StoreId,
                Name = p.Name,
                Code = p.Code,
                UnitPrice = Math.Round(p.UnitPrice.GetValueOrDefault(), 3),
                UnitsInStock = Math.Round(p.UnitsInStock.GetValueOrDefault(), 3),
                UnitsOnOrder = Math.Round(p.UnitsOnOrder.GetValueOrDefault(), 3),
                ReorderLevel = Math.Round(p.ReorderLevel.GetValueOrDefault(), 3),
                StockValue = (decimal)p.StockValue,
                ModifiedDate = p.ModifiedDate,
                ModifiedByUser = p.ModifiedByUser
            };
            return productViewModel;
        }
    }
}