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

        //[Bind(Prefix = "models")] IS VERY IMPORTANT, DOWSNT WORK WITHOUT IT

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<CategoryViewModel> categories)
        {
            var results = new List<CategoryViewModel>();

            if (categories != null && ModelState.IsValid)
            {
                foreach (var category in categories)
                {
                    db.ProductCategories.Add(new ProductCategory() { CategoryId = category.CategoryId, Name = category.Name, ModifiedByUser=category.ModifiedByUser, ModifiedDate = category.ModifiedDate});
                    db.SaveChanges();
                    results.Add(category);
                }
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }

        //[Bind(Prefix = "models")] IS VERY IMPORTANT, DOWSNT WORK WITHOUT IT

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<CategoryViewModel> categories)
        {
            if (categories != null && ModelState.IsValid)
            {
                //foreach (var product in categories)
                //{
                //    var target = SessionProductRepository.One(p => p.ProductID == product.ProductID);
                //    if (target != null)
                //    {
                //        target.ProductName = product.ProductName;
                //        target.UnitPrice = product.UnitPrice;
                //        target.UnitsInStock = product.UnitsInStock;
                //        target.LastSupply = product.LastSupply;
                //        target.Discontinued = product.Discontinued;
                //        SessionProductRepository.Update(target);
                //    }
                //}
            }

            return Json(categories.ToDataSourceResult(request, ModelState));
        }

        //[Bind(Prefix = "models")] IS VERY IMPORTANT, DOWSNT WORK WITHOUT IT

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