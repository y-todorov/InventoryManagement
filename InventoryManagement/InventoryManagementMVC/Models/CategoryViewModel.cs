using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecipiesModelNS;

namespace InventoryManagementMVC.Models
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedByUser { get; set; }
    }
}