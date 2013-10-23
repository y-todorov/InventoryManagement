using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventoryManagementMVC.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using RecipiesModelNS;

namespace InventoryManagementMVC.Controllers
{
    public class CategoryController : Controller
    {
        private RecipiesEntities db = new RecipiesEntities();
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            List<CategoryViewModel> cvms = db.ProductCategories.Select
                (c => new CategoryViewModel()
                {
                    CategoryId = c.CategoryId,
                    Name = c.Name,
                    ModifiedDate = c.ModifiedDate,
                    ModifiedByUser = c.ModifiedByUser
                }).ToList();
            return Json(cvms.ToDataSourceResult(request));
        }

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult Editing_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<ProductViewModel> products)
        //{
        //    var results = new List<ProductViewModel>();

        //    if (products != null && ModelState.IsValid)
        //    {
        //        foreach (var product in products)
        //        {
        //            SessionProductRepository.Insert(product);
        //            results.Add(product);
        //        }
        //    }

        //    return Json(results.ToDataSourceResult(request, ModelState));
        //}

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult Editing_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<ProductViewModel> products)
        //{
        //    if (products != null && ModelState.IsValid)
        //    {
        //        foreach (var product in products)
        //        {
        //            var target = SessionProductRepository.One(p => p.ProductID == product.ProductID);
        //            if (target != null)
        //            {
        //                target.ProductName = product.ProductName;
        //                target.UnitPrice = product.UnitPrice;
        //                target.UnitsInStock = product.UnitsInStock;
        //                target.LastSupply = product.LastSupply;
        //                target.Discontinued = product.Discontinued;
        //                SessionProductRepository.Update(target);
        //            }
        //        }
        //    }

        //    return Json(products.ToDataSourceResult(request, ModelState));
        //}

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult Editing_Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<ProductViewModel> products)
        //{
        //    if (products.Any())
        //    {
        //        foreach (var product in products)
        //        {
        //            SessionProductRepository.Delete(product);
        //        }
        //    }

        //    return Json(products.ToDataSourceResult(request, ModelState));
        //}

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}