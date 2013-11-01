using RecipiesModelNS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventoryManagementMVC.Models
{
    public class InventoryViewModel
    {
        [Key]
        public int InventoryId { get; set; }

        public DateTime? ForDate { get; set; }

        public decimal? AverageUnitPrice { get; set; }

        public double? QuantityByDocuments { get; set; }

        [ReadOnly(true)]
        public decimal? ValueByDocuments { get; set; }

        [ReadOnly(true)]
        public double? StocktakeQuantity { get; set; }

        [ReadOnly(true)]
        public decimal? StocktakeValue { get; set; }

        [ReadOnly(true)]
        public double? DeficiencyQuantity { get; set; }

        [ReadOnly(true)]
        public decimal? DeficiencyValue { get; set; }

        [ReadOnly(true)]
        public double? SurplusQuantity { get; set; }

        [ReadOnly(true)]
        public decimal? SurplusValue { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedByUser { get; set; }

        public static InventoryViewModel ConvertFromInventoryEntity(Inventory newOrExistingInventoryEntity, InventoryViewModel inventoryViewModel)
        {
            inventoryViewModel.AverageUnitPrice = newOrExistingInventoryEntity.AverageUnitPrice;
            inventoryViewModel.DeficiencyQuantity = newOrExistingInventoryEntity.DeficiencyQuantity;
            inventoryViewModel.DeficiencyValue = (decimal?)newOrExistingInventoryEntity.DeficiencyValue;
            inventoryViewModel.ForDate = newOrExistingInventoryEntity.ForDate;
            inventoryViewModel.InventoryId = newOrExistingInventoryEntity.InventoryId;
            inventoryViewModel.ModifiedByUser = newOrExistingInventoryEntity.ModifiedByUser;
            inventoryViewModel.ModifiedDate = newOrExistingInventoryEntity.ModifiedDate;
            inventoryViewModel.QuantityByDocuments = newOrExistingInventoryEntity.QuantityByDocuments;
            inventoryViewModel.StocktakeQuantity = newOrExistingInventoryEntity.StocktakeQuantity;
            inventoryViewModel.StocktakeValue = (decimal?)newOrExistingInventoryEntity.StocktakeValue;
            inventoryViewModel.SurplusQuantity = newOrExistingInventoryEntity.SurplusQuantity;
            inventoryViewModel.SurplusValue = (decimal?)newOrExistingInventoryEntity.SurplusValue;
            inventoryViewModel.ValueByDocuments = (decimal?)newOrExistingInventoryEntity.ValueByDocuments;

            return inventoryViewModel;
        }

        public static Inventory ConvertToInventoryEntity(InventoryViewModel inventoryViewModel, Inventory newOrExistingInventoryEntity)
        {
            newOrExistingInventoryEntity.AverageUnitPrice = inventoryViewModel.AverageUnitPrice;
            newOrExistingInventoryEntity.DeficiencyQuantity = inventoryViewModel.DeficiencyQuantity;
            newOrExistingInventoryEntity.DeficiencyValue = (double?)inventoryViewModel.DeficiencyValue;
            newOrExistingInventoryEntity.ForDate = inventoryViewModel.ForDate;
            newOrExistingInventoryEntity.InventoryId = inventoryViewModel.InventoryId;
            newOrExistingInventoryEntity.ModifiedByUser = inventoryViewModel.ModifiedByUser;
            newOrExistingInventoryEntity.ModifiedDate = inventoryViewModel.ModifiedDate;
            newOrExistingInventoryEntity.QuantityByDocuments = inventoryViewModel.QuantityByDocuments;
            newOrExistingInventoryEntity.StocktakeQuantity = inventoryViewModel.StocktakeQuantity;
            newOrExistingInventoryEntity.StocktakeValue = (double?)inventoryViewModel.StocktakeValue;
            newOrExistingInventoryEntity.SurplusQuantity = inventoryViewModel.SurplusQuantity;
            newOrExistingInventoryEntity.SurplusValue = (double?)inventoryViewModel.SurplusValue;
            newOrExistingInventoryEntity.ValueByDocuments = (double?)inventoryViewModel.ValueByDocuments;

            return newOrExistingInventoryEntity;
        }

    }
}