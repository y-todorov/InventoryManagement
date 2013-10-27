﻿using System;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementMVC.Models
{
    public class UnitMeasureViewModel
    {
        [Key]
        public int UnitMeasureId { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsBaseUnit { get; set; }

        [Association("", "", "")]
        public int? BaseUnitId { get; set; }

        [Range(0, int.MaxValue)]
        public double? BaseUnitFactor { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedByUser { get; set; }
    }
}