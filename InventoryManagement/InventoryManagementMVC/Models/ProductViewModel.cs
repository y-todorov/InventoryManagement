﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventoryManagementMVC.Models
{
    public class ProductViewModel
    {
        [Key]
        public int ProductId { get; set; }

        public string UnitMeasure { get; set; }
        
        public string Category { get; set; }

        public string Store { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public decimal? UnitPrice { get; set; }

        public double? UnitsInStock { get; set; }

        public double? UnitsOnOrder { get; set; }

        public double? ReorderLevel { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedByUser { get; set; }

        public decimal? StockValue { get; set; }


    }
}