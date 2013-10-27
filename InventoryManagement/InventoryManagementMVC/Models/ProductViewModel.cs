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

        public int? UnitMeasureId { get; set; }

        //public string UnitMeasure { get; set; }

        [Required(ErrorMessage = "Please select a category!")]
        public int? CategoryId { get; set; }

        //public string Category { get; set; }

        public int? StoreId { get; set; }
        
        //public string Store { get; set; }

        [Required]
        public string Name { get; set; }

        public string Code { get; set; }

        //[DisplayFormat(DataFormatString = "{0:F3}", ApplyFormatInEditMode = true)]
        public decimal? UnitPrice { get; set; }
        
        [DisplayName("Units in stock")]
        [DataType("Double")]
        //[DisplayFormat(DataFormatString="{0:F3}", ApplyFormatInEditMode=true)]
        [Range(0, int.MaxValue)]
        public double? UnitsInStock { get; set; }

        [ReadOnly(true)]
        public decimal? StockValue { get; set; }

        //[DisplayFormat(DataFormatString = "{0:F3}", ApplyFormatInEditMode = true)]
        public double? UnitsOnOrder { get; set; }

        //[DisplayFormat(DataFormatString = "{0:F3}", ApplyFormatInEditMode = true)]
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
                StockValue = (decimal) p.StockValue,
                ModifiedDate = p.ModifiedDate,
                ModifiedByUser = p.ModifiedByUser
            };
            return productViewModel;
        }
    }
}