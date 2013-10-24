using System;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementMVC.Models
{
    public class CategoryViewModel
    {
        [Key]
        public int CategoryId { get; set; }

        [Display(Name="Name")]
        [Required(ErrorMessage="Please enter a name!")]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm:ss}")] // this is from globalization
        
        public DateTime? ModifiedDate { get; set; }

        //[Required(
        public string ModifiedByUser { get; set; }
    }
}