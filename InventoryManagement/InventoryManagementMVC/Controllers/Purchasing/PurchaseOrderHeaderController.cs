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
               (pod => PurchaseOrderHeaderViewModel.ConvertFromPurchaseOrderHeaderEntity(pod, new PurchaseOrderHeaderViewModel())).ToList();
            return View(purchaseOrderHeaderViewModels);
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            List<PurchaseOrderHeaderViewModel> purchaseOrderHeaderViewModels =
                ContextFactory.Current.PurchaseOrderHeaders
                .ToList().Select
                (pod => PurchaseOrderHeaderViewModel.ConvertFromPurchaseOrderHeaderEntity(pod, new PurchaseOrderHeaderViewModel())).ToList();
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
                    PurchaseOrderHeader newPohEntity = PurchaseOrderHeaderViewModel.ConvertToPurchaseOrderHeaderEntity(pohViewModel,
                        new PurchaseOrderHeader());
                    ContextFactory.Current.PurchaseOrderHeaders.Add(newPohEntity);
                    ContextFactory.Current.SaveChanges();
                    PurchaseOrderHeaderViewModel.ConvertFromPurchaseOrderHeaderEntity(newPohEntity, pohViewModel);
                }
            }

            return Json(purchaseOrderHeaders.ToDataSourceResult(request, ModelState));
        }

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult Update([DataSourceRequest] DataSourceRequest request,
        //    [Bind(Prefix = "models")] IEnumerable<CategoryViewModel> categories)
        //{
        //    if (categories != null && ModelState.IsValid)
        //    {
        //        foreach (CategoryViewModel categoryViewModel in categories)
        //        {
        //            ProductCategory categoryEntity =
        //                ContextFactory.Current.ProductCategories.FirstOrDefault(c => c.CategoryId == categoryViewModel.CategoryId);

        //            CategoryViewModel.ConvertToCategoryEntity(categoryViewModel, categoryEntity);

        //            ContextFactory.Current.SaveChanges();

        //            CategoryViewModel.ConvertFromCategoryEntity(categoryEntity, categoryViewModel);
        //        }
        //    }

        //    return Json(categories.ToDataSourceResult(request, ModelState));
        //}

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<CategoryViewModel> categories)
        //{
        //    if (categories.Any())
        //    {
        //        foreach (CategoryViewModel category in categories)
        //        {
        //            ProductCategory productCategory = ContextFactory.Current.ProductCategories.FirstOrDefault(c => c.CategoryId == category.CategoryId);
        //            ContextFactory.Current.ProductCategories.Remove(productCategory);

        //            ContextFactory.Current.SaveChanges();
        //        }
        //    }

        //    return Json(categories.ToDataSourceResult(request, ModelState));
        //}

    }
}
