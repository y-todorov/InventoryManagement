using System;
using System.ComponentModel.DataAnnotations;
using RecipiesModelNS;

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

        public static CategoryViewModel ConvertFromCategoryEntity(ProductCategory newOrExistingCategoryEntity, CategoryViewModel categoryViewModel)
        {
            if (newOrExistingCategoryEntity == null)
            {
                throw new ApplicationException("newOrExistingProductEntity is null in method ConvertFromProductEntity!");
            }
            if (categoryViewModel == null)
            {
                throw new ApplicationException("productViewModel is null in method ConvertFromProductEntity!");
            }
          
            categoryViewModel.CategoryId = newOrExistingCategoryEntity.CategoryId;
            categoryViewModel.Name = newOrExistingCategoryEntity.Name;
            categoryViewModel.ModifiedDate = newOrExistingCategoryEntity.ModifiedDate;
            categoryViewModel.ModifiedByUser = newOrExistingCategoryEntity.ModifiedByUser;

            return categoryViewModel;
        }

        public static ProductCategory ConvertToCategoryEntity(CategoryViewModel categoryViewModel, ProductCategory newOrExistingCategoryEntity)
        {
            if (newOrExistingCategoryEntity == null)
            {
                throw new ApplicationException("newOrExistingProductEntity is null in method ConvertToProductEntity!");
            }
            if (categoryViewModel == null)
            {
                throw new ApplicationException("productViewModel is null in method ConvertToProductEntity!");
            }
        
            newOrExistingCategoryEntity.CategoryId = categoryViewModel.CategoryId;
            newOrExistingCategoryEntity.Name = categoryViewModel.Name;
            newOrExistingCategoryEntity.ModifiedDate = categoryViewModel.ModifiedDate;
            newOrExistingCategoryEntity.ModifiedByUser = categoryViewModel.ModifiedByUser;

            return newOrExistingCategoryEntity;
        }
    }
}