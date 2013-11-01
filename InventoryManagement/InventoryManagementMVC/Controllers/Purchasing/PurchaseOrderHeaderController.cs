using InventoryManagementMVC.Models.Purchasing;
using Kendo.Mvc.UI;
using RecipiesModelNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;

namespace InventoryManagementMVC.Controllers.Purchasing
{
    public class PurchaseOrderHeaderController : Controller
    {
        //
        // GET: /PurchaseOrder/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            List<PurchaseOrderHeaderViewModel> purchaseOrderHeaderViewModels =
                ContextFactory.Current.PurchaseOrderHeaders
                //.Include(pod => pod.PurchaseOrderHeader)
                //.Include(pod => pod.Product)
                //.Include(pod => pod.Product.ProductCategory)
                .ToList().Select
                (pod => PurchaseOrderHeaderViewModel.ConvertFromPurchaseOrderHeaderEntity(pod, new PurchaseOrderHeaderViewModel())).ToList();
            return Json(purchaseOrderHeaderViewModels.ToDataSourceResult(request));
        }

    }
}
