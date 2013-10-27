using System;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementMVC.Models
{
    public class CategoryViewModel
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please enter a name for the category!")]
        public string Name { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedByUser { get; set; }
    }
}