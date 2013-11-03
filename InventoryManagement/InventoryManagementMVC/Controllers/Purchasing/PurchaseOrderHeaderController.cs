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
            List<PurchaseOrderHeaderViewModel> purchaseOrderHeaderViewModels =
                ContextFactory.Current.PurchaseOrderHeaders
                    .ToList().Select
                    (pod =>
                        PurchaseOrderHeaderViewModel.ConvertFromPurchaseOrderHeaderEntity(pod,
                            new PurchaseOrderHeaderViewModel())).ToList();
            return View(purchaseOrderHeaderViewModels);
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            List<PurchaseOrderHeaderViewModel> purchaseOrderHeaderViewModels =
                ContextFactory.Current.PurchaseOrderHeaders
                    .ToList().Select
                    (pod =>
                        PurchaseOrderHeaderViewModel.ConvertFromPurchaseOrderHeaderEntity(pod,
                            new PurchaseOrderHeaderViewModel())).ToList();
            return Json(purchaseOrderHeaderViewModels.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<PurchaseOrderHeaderViewModel> purchaseOrderHeaders)
        {
            if (purchaseOrderHeaders != null && ModelState.IsValid)
            {
                foreach (PurchaseOrderHeaderViewModel pohViewModel in purchaseOrderHeaders)
                {
                    PurchaseOrderHeader newPohEntity =
                        PurchaseOrderHeaderViewModel.ConvertToPurchaseOrderHeaderEntity(pohViewModel,
                            new PurchaseOrderHeader());
                    ContextFactory.Current.PurchaseOrderHeaders.Add(newPohEntity);
                    ContextFactory.Current.SaveChanges();
                    PurchaseOrderHeaderViewModel.ConvertFromPurchaseOrderHeaderEntity(newPohEntity, pohViewModel);
                }
            }

            return Json(purchaseOrderHeaders.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<PurchaseOrderHeaderViewModel> purchaseOrderHeaders)
        {
            if (purchaseOrderHeaders != null && ModelState.IsValid)
            {
                foreach (PurchaseOrderHeaderViewModel pohViewModel in purchaseOrderHeaders)
                {
                    PurchaseOrderHeader pohEntity =
                        ContextFactory.Current.PurchaseOrderHeaders.FirstOrDefault(
                            c => c.PurchaseOrderId == pohViewModel.PurchaseOrderHeaderId);

                    PurchaseOrderHeaderViewModel.ConvertToPurchaseOrderHeaderEntity(pohViewModel, pohEntity);

                    ContextFactory.Current.SaveChanges();

                    PurchaseOrderHeaderViewModel.ConvertFromPurchaseOrderHeaderEntity(pohEntity, pohViewModel);
                }
            }

            return Json(purchaseOrderHeaders.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<PurchaseOrderHeaderViewModel> purchaseOrderHeaders)
        {
            foreach (PurchaseOrderHeaderViewModel poh in purchaseOrderHeaders)
            {
                PurchaseOrderHeader pod =
                    ContextFactory.Current.PurchaseOrderHeaders.FirstOrDefault(
                        c => c.PurchaseOrderId == poh.PurchaseOrderHeaderId);
                ContextFactory.Current.PurchaseOrderHeaders.Remove(pod);

                ContextFactory.Current.SaveChanges();
            }

            return Json(purchaseOrderHeaders.ToDataSourceResult(request, ModelState));
        }
    }
}