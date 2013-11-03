using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using InventoryManagementMVC.Models;

namespace RecipiesModelNS
{
    public class StoreViewModel
    {
        [Key]
        public int StoreId { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedByUser { get; set; }

        public static StoreViewModel ConvertFromStoreEntity(Store newOrExistingStoreEntity,
            StoreViewModel storeViewModel)
        {
            storeViewModel.StoreId = newOrExistingStoreEntity.StoreId;
            storeViewModel.Name = newOrExistingStoreEntity.Name;
            storeViewModel.ModifiedDate = newOrExistingStoreEntity.ModifiedDate;
            storeViewModel.ModifiedByUser = newOrExistingStoreEntity.ModifiedByUser;

            return storeViewModel;
        }

        public static Store ConvertToStoreEntity(StoreViewModel storeViewModel, Store newOrExistingStoreEntity)
        {
            newOrExistingStoreEntity.StoreId = storeViewModel.StoreId;
            newOrExistingStoreEntity.Name = storeViewModel.Name;
            newOrExistingStoreEntity.ModifiedDate = storeViewModel.ModifiedDate;
            newOrExistingStoreEntity.ModifiedByUser = storeViewModel.ModifiedByUser;

            return newOrExistingStoreEntity;
        }
    }
}