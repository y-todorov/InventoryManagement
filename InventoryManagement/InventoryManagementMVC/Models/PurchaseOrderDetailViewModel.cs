using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using RecipiesModelNS;

namespace InventoryManagementMVC.Models
{
    public class PurchaseOrderDetailViewModel
    {
        [Key]
        public int PurchaseOrderDetailId { get; set; }

        [Association("", "", "")]
        public int? PurchaseOrderHeaderId { get; set; }

        [Association("", "", "")]
        public int? ProductId { get; set; }

        [Association("", "", "")]
        public int? UnitMeasureId { get; set; }

        public double? OrderQuantity { get; set; }

        public decimal? UnitPrice { get; set; }

        public decimal LineTotal { get; set; }

        public double? ReceivedQuantity { get; set; }

        public double? ReturnedQuantity { get; set; }

        public double StockedQuantity { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedByUser { get; set; }

        public static PurchaseOrderDetailViewModel ConvertFromPurchaseOrderDetailEntity(PurchaseOrderDetail newOrExistingPod, PurchaseOrderDetailViewModel podViewModel)
        {
            if (newOrExistingPod == null)
            {
                throw new ApplicationException("PurchaseOrderDetail is null in method ConvertFromPurchaseOrderDetailEntity!");
            }
            if (podViewModel == null)
            {
                throw new ApplicationException("PurchaseOrderDetailViewModel is null in method ConvertFromPurchaseOrderDetailEntity!");
            }

            podViewModel.LineTotal = (decimal)newOrExistingPod.LineTotal;
            podViewModel.ModifiedByUser = newOrExistingPod.ModifiedByUser;
            podViewModel.ModifiedDate = newOrExistingPod.ModifiedDate;
            podViewModel.OrderQuantity = newOrExistingPod.OrderQuantity;
            podViewModel.ProductId = newOrExistingPod.ProductId;
            podViewModel.PurchaseOrderDetailId = newOrExistingPod.PurchaseOrderDetailId;
            podViewModel.PurchaseOrderHeaderId = newOrExistingPod.PurchaseOrderId;
            podViewModel.ReceivedQuantity = newOrExistingPod.ReceivedQuantity;
            podViewModel.ReturnedQuantity = newOrExistingPod.ReturnedQuantity;
            podViewModel.StockedQuantity = newOrExistingPod.StockedQuantity;
            podViewModel.UnitMeasureId = newOrExistingPod.UnitMeasureId;
            podViewModel.UnitPrice = newOrExistingPod.UnitPrice;

            return podViewModel;
        }

        public static PurchaseOrderDetail ConvertToPurchaseOrderDetailEntity(PurchaseOrderDetailViewModel podViewModel, PurchaseOrderDetail newOrExistingPod)
        {
            if (newOrExistingPod == null)
            {
                throw new ApplicationException("PurchaseOrderDetail is null in method ConvertToPurchaseOrderDetailEntity!");
            }
            if (podViewModel == null)
            {
                throw new ApplicationException("PurchaseOrderDetailViewModel is null in method ConvertToPurchaseOrderDetailEntity!");
            }

            newOrExistingPod.LineTotal = (double)podViewModel.LineTotal;
            newOrExistingPod.ModifiedByUser = podViewModel.ModifiedByUser;
            newOrExistingPod.ModifiedDate = podViewModel.ModifiedDate;
            newOrExistingPod.OrderQuantity = podViewModel.OrderQuantity;
            newOrExistingPod.ProductId = podViewModel.ProductId;
            newOrExistingPod.PurchaseOrderDetailId = podViewModel.PurchaseOrderDetailId;
            newOrExistingPod.PurchaseOrderId = podViewModel.PurchaseOrderHeaderId;
            newOrExistingPod.ReceivedQuantity = podViewModel.ReceivedQuantity;
            newOrExistingPod.ReturnedQuantity = podViewModel.ReturnedQuantity;
            newOrExistingPod.StockedQuantity = podViewModel.StockedQuantity;
            newOrExistingPod.UnitMeasureId = podViewModel.UnitMeasureId;
            newOrExistingPod.UnitPrice = podViewModel.UnitPrice;

            return newOrExistingPod;
        }
    }
}