using RecipiesModelNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventoryManagementMVC.Models.Purchasing
{
    public class PurchaseOrderHeaderViewModel
    {
        [Key]
        public int PurchaseOrderHeaderId { get; set; }

        public int? StatusId { get; set; }

        public int? EmployeeId { get; set; }

        public int? VendorId { get; set; }

        public int? ShipMethodId { get; set; }

        [Required]
        public DateTime? OrderDate { get; set; }

        [Required]
        public DateTime? ShipDate { get; set; }

        public decimal? SubTotal { get; set; }

        public decimal? VAT { get; set; }

        public decimal? Freight { get; set; }

        public decimal? TotalDue { get; set; }

        [StringLength(1000)]
        public string InvoiceNumber { get; set; }

        [StringLength(4000)]
        public string Notes { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedByUser { get; set; }

        public List<PurchaseOrderDetailViewModel> PurchaseOrderDetailViewModels { get; set; }


        public static PurchaseOrderHeaderViewModel ConvertFromPurchaseOrderHeaderEntity(PurchaseOrderHeader pohEntity, PurchaseOrderHeaderViewModel pohViewModel)
        {          

            pohViewModel.EmployeeId = pohEntity.EmployeeId;
            pohViewModel.Freight = pohEntity.Freight;
            pohViewModel.InvoiceNumber = pohEntity.InvoiceNumber;
            pohViewModel.ModifiedByUser = pohEntity.ModifiedByUser;
            pohViewModel.ModifiedDate = pohEntity.ModifiedDate;
            pohViewModel.Notes = pohEntity.Notes;
            pohViewModel.OrderDate = pohEntity.OrderDate;
            pohViewModel.PurchaseOrderHeaderId = pohEntity.PurchaseOrderId;
            pohViewModel.ShipMethodId = pohEntity.ShipMethodId;
            pohViewModel.ShipDate = pohEntity.ShipDate;
            pohViewModel.StatusId = pohEntity.StatusId;
            pohViewModel.SubTotal = pohEntity.SubTotal;
            pohViewModel.TotalDue = pohEntity.TotalDue;
            pohViewModel.VAT = pohEntity.VAT;
            pohViewModel.VendorId = pohEntity.VendorId;

            pohViewModel.PurchaseOrderDetailViewModels = pohEntity.PurchaseOrderDetails.Select(pod =>
                PurchaseOrderDetailViewModel.ConvertFromPurchaseOrderDetailEntity(pod, new PurchaseOrderDetailViewModel())).ToList();
        
            return pohViewModel;
        }

        public static PurchaseOrderHeader ConvertToPurchaseOrderHeaderEntity(PurchaseOrderHeaderViewModel pohViewModel, PurchaseOrderHeader pohEntity)
        {
            pohEntity.EmployeeId = pohViewModel.EmployeeId;
            pohEntity.Freight = pohViewModel.Freight;
            pohEntity.InvoiceNumber = pohViewModel.InvoiceNumber;
            pohEntity.ModifiedByUser = pohViewModel.ModifiedByUser;
            pohEntity.ModifiedDate = pohViewModel.ModifiedDate;
            pohEntity.Notes = pohViewModel.Notes;
            pohEntity.OrderDate = pohViewModel.OrderDate;
            pohEntity.PurchaseOrderId = pohViewModel.PurchaseOrderHeaderId;
            pohEntity.ShipMethodId = pohViewModel.ShipMethodId;
            pohEntity.ShipDate = pohViewModel.ShipDate;
            pohEntity.StatusId = pohViewModel.StatusId;
            pohEntity.SubTotal = pohViewModel.SubTotal;
            pohEntity.TotalDue = pohViewModel.TotalDue;
            pohEntity.VAT = pohViewModel.VAT;
            pohEntity.VendorId = pohViewModel.VendorId;

            return pohEntity;
        }
    }
}