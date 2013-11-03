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

        public static CategoryViewModel ConvertFromCategoryEntity(ProductCategory newOrExistingCategoryEntity,
            CategoryViewModel categoryViewModel)
        {
            if (newOrExistingCategoryEntity == null)
            {
                throw new ApplicationException(
                    "newOrExistingCategoryEntity is null in method ConvertFromCategoryEntity!");
            }
            if (categoryViewModel == null)
            {
                throw new ApplicationException("categoryViewModel is null in method ConvertFromCategoryEntity!");
            }

            categoryViewModel.CategoryId = newOrExistingCategoryEntity.CategoryId;
            categoryViewModel.Name = newOrExistingCategoryEntity.Name;
            categoryViewModel.ModifiedDate = newOrExistingCategoryEntity.ModifiedDate;
            categoryViewModel.ModifiedByUser = newOrExistingCategoryEntity.ModifiedByUser;

            return categoryViewModel;
        }

        public static ProductCategory ConvertToCategoryEntity(CategoryViewModel categoryViewModel,
            ProductCategory newOrExistingCategoryEntity)
        {
            if (newOrExistingCategoryEntity == null)
            {
                throw new ApplicationException("newOrExistingCategoryEntity is null in method ConvertToCategoryEntity!");
            }
            if (categoryViewModel == null)
            {
                throw new ApplicationException("categoryViewModel is null in method ConvertToCategoryEntity!");
            }

            newOrExistingCategoryEntity.CategoryId = categoryViewModel.CategoryId;
            newOrExistingCategoryEntity.Name = categoryViewModel.Name;
            newOrExistingCategoryEntity.ModifiedDate = categoryViewModel.ModifiedDate;
            newOrExistingCategoryEntity.ModifiedByUser = categoryViewModel.ModifiedByUser;

            return newOrExistingCategoryEntity;
        }
    }
}