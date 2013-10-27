using RecipiesModelNS;
using System;
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

        public static UnitMeasureViewModel ConvertFromUnitMeasureEntity(UnitMeasure newOrExistingUnitMeasureEntity, UnitMeasureViewModel unitMeasureViewModel)
        {
            if (newOrExistingUnitMeasureEntity == null)
            {
                throw new ApplicationException("newOrExistingUnitMeasureEntity is null in method ConvertFromUnitMeasureEntity!");
            }
            if (unitMeasureViewModel == null)
            {
                throw new ApplicationException("unitMeasureViewModel is null in method ConvertFromUnitMeasureEntity!");
            }

            unitMeasureViewModel.BaseUnitFactor = newOrExistingUnitMeasureEntity.BaseUnitFactor;
            unitMeasureViewModel.BaseUnitId = newOrExistingUnitMeasureEntity.BaseUnitId;
            unitMeasureViewModel.IsBaseUnit = newOrExistingUnitMeasureEntity.IsBaseUnit.GetValueOrDefault();
            unitMeasureViewModel.UnitMeasureId = newOrExistingUnitMeasureEntity.UnitMeasureId;
            unitMeasureViewModel.Name = newOrExistingUnitMeasureEntity.Name;
            unitMeasureViewModel.ModifiedDate = newOrExistingUnitMeasureEntity.ModifiedDate;
            unitMeasureViewModel.ModifiedByUser = newOrExistingUnitMeasureEntity.ModifiedByUser;

            return unitMeasureViewModel;
        }

        public static UnitMeasure ConvertToUnitMeasureEntity(UnitMeasureViewModel unitMeasureViewModel, UnitMeasure newOrExistingUnitMeasureEntity)
        {
            if (newOrExistingUnitMeasureEntity == null)
            {
                throw new ApplicationException("newOrExistingUnitMeasureEntity is null in method ConvertToUnitMeasureEntity!");
            }
            if (unitMeasureViewModel == null)
            {
                throw new ApplicationException("unitMeasureViewModel is null in method ConvertToUnitMeasureEntity!");
            }

            newOrExistingUnitMeasureEntity.BaseUnitFactor = unitMeasureViewModel.BaseUnitFactor;
            newOrExistingUnitMeasureEntity.BaseUnitId = unitMeasureViewModel.BaseUnitId;
            newOrExistingUnitMeasureEntity.IsBaseUnit = unitMeasureViewModel.IsBaseUnit;
            newOrExistingUnitMeasureEntity.UnitMeasureId = unitMeasureViewModel.UnitMeasureId;
            newOrExistingUnitMeasureEntity.Name = unitMeasureViewModel.Name;
            newOrExistingUnitMeasureEntity.ModifiedDate = unitMeasureViewModel.ModifiedDate;
            newOrExistingUnitMeasureEntity.ModifiedByUser = unitMeasureViewModel.ModifiedByUser;

            return newOrExistingUnitMeasureEntity;
        }
    }
}