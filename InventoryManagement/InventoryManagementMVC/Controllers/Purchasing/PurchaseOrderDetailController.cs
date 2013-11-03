using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventoryManagementMVC.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using RecipiesModelNS;
using System.Data.Entity; // .Include !!!!!!! THIS IS SO IMPROTANT

namespace InventoryManagementMVC.Controllers
{
    public class PurchaseOrderDetailController : Controller
    {
        public ActionResult Index()
        {
            ControllerHelper.PopulateProducts(ViewData);
            ControllerHelper.PopulatePurchaseOrderHeaders(ViewData);
            ControllerHelper.PopulateUnitMeasures(ViewData);

            List<PurchaseOrderDetailViewModel> purchaseOrderDetailViewModels =
                ContextFactory.Current.PurchaseOrderDetails
                    .Include(pod => pod.PurchaseOrderHeader.Vendor)
                    .Include(pod => pod.Product.ProductCategory)
                    .ToList().Select
                    (pod =>
                        PurchaseOrderDetailViewModel.ConvertFromPurchaseOrderDetailEntity(pod,
                            new PurchaseOrderDetailViewModel())).ToList();

            return View(purchaseOrderDetailViewModels);
        }

        public ActionResult Read(int? purchaseOrderHeaderId, [DataSourceRequest] DataSourceRequest request)
        {
            List<PurchaseOrderDetailViewModel> purchaseOrderDetailViewModels =
                ContextFactory.Current.PurchaseOrderDetails.Where(
                    pod => purchaseOrderHeaderId.HasValue ? pod.PurchaseOrderId == purchaseOrderHeaderId.Value : true)
                    .Include(pod => pod.PurchaseOrderHeader.Vendor)
                    .Include(pod => pod.Product.ProductCategory)
                    .ToList().Select
                    (pod =>
                        PurchaseOrderDetailViewModel.ConvertFromPurchaseOrderDetailEntity(pod,
                            new PurchaseOrderDetailViewModel())).ToList();
            return Json(purchaseOrderDetailViewModels.ToDataSourceResult(request));
        }
    }
}