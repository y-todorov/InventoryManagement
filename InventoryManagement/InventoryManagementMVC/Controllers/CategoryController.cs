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

        //
        // GET: /Category/

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

        //
        // GET: /Category/Details/5

        public ActionResult Details(int id = 0)
        {
            ProductCategory productcategory = db.ProductCategories.Find(id);
            if (productcategory == null)
            {
                return HttpNotFound();
            }
            return View(productcategory);
        }

        //
        // GET: /Category/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Category/Create

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(ProductCategory productcategory)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.ProductCategories.Add(productcategory);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(productcategory);
        //}

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<CategoryViewModel> categories)
        //{
        //    var results = new List<ProductViewModel>();

        //    if (categories != null && ModelState.IsValid)
        //    {
        //        foreach (var product in categories)
        //        {
        //            SessionProductRepository.Insert(product);
        //            results.Add(product);
        //        }
        //    }

        //    return Json(results.ToDataSourceResult(request, ModelState));
        //}


        //
        // GET: /Category/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ProductCategory productcategory = db.ProductCategories.Find(id);
            if (productcategory == null)
            {
                return HttpNotFound();
            }
            return View(productcategory);
        }

        //
        // POST: /Category/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductCategory productcategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productcategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productcategory);
        }

        //
        // GET: /Category/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ProductCategory productcategory = db.ProductCategories.Find(id);
            if (productcategory == null)
            {
                return HttpNotFound();
            }
            return View(productcategory);
        }

        //
        // POST: /Category/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductCategory productcategory = db.ProductCategories.Find(id);
            db.ProductCategories.Remove(productcategory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}