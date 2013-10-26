using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagementMVC.Models
{
    public class UnitMeasureViewModel
    {
        public int UnitMeasureId { get; set; }

        public string Name { get; set; }

        public bool? IsBaseUnit { get; set; }

        public int BaseUnitId { get; set; }

        public double BaseUnitFactor { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedByUser { get; set; }
    }
}