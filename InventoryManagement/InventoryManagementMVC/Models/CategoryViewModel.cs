using System;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementMVC.Models
{
    public class CategoryViewModel
    {
        [Key]
        public int CategoryId { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please enter a name!")]
        public string Name { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true)] 
        public DateTime? ModifiedDate { get; set; }

        public string ModifiedByUser { get; set; }
    }
}