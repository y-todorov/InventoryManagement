using System;
//using System.Data.Objects;
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
using System.Threading;

namespace InventoryManagementMVC.Controllers
{
    public class CategoryController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            List<CategoryViewModel> cvms = ContextFactory.Current.ProductCategories.Select
                (c => new CategoryViewModel()
                {
                    CategoryId = c.CategoryId,
                    Name = c.Name,
                    ModifiedDate = c.ModifiedDate,//.GetValueOrDefault().ToString(Thread.CurrentThread.CurrentUICulture);,
                    ModifiedByUser = c.ModifiedByUser
                }).ToList();
            return Json(cvms.ToDataSourceResult(request));
        }
        
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<CategoryViewModel> categories)
        {
            var results = new List<CategoryViewModel>();

            if (categories != null && ModelState.IsValid)
            {
                foreach (var category in categories)
                {
                    ProductCategory newCategory = ContextFactory.Current.ProductCategories.Add(new ProductCategory()
                    {
                        CategoryId = category.CategoryId,
                        Name = category.Name,
                        ModifiedByUser = category.ModifiedByUser,
                        ModifiedDate = category.ModifiedDate
                    });
                    ContextFactory.Current.SaveChanges();
                    category.ModifiedByUser = newCategory.ModifiedByUser;
                    category.ModifiedDate = newCategory.ModifiedDate;
                    results.Add(category);
                }
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<CategoryViewModel> categories)
        {
            if (categories != null && ModelState.IsValid)
            {
                foreach (CategoryViewModel category in categories)
                {
                    ProductCategory productCategory = ContextFactory.Current.ProductCategories.FirstOrDefault(c => c.CategoryId == category.CategoryId);
                    if (productCategory != null)
                    {
                        productCategory.CategoryId = category.CategoryId;
                        productCategory.Name = category.Name;
                        productCategory.ModifiedByUser = category.ModifiedByUser;
                        productCategory.ModifiedDate = category.ModifiedDate;
                        ContextFactory.Current.SaveChanges();
                        category.ModifiedByUser = productCategory.ModifiedByUser;
                        category.ModifiedDate = productCategory.ModifiedDate;
                    }
                }
            }

            return Json(categories.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<CategoryViewModel> categories)
        {
            if (categories.Any())// && ModelState.IsValid)
            {
                foreach (CategoryViewModel category in categories)
                {
                    RecipiesEntities context = new RecipiesEntities();
                    ProductCategory productCategory = context.ProductCategories.ToList().FirstOrDefault(c => c.CategoryId == category.CategoryId);
                    context.ProductCategories.Remove(productCategory);// .ProductCategories.Remove(productCategory);
                    
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }

            return Json(categories.ToDataSourceResult(request, ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            //db.Dispose();
            base.Dispose(disposing);
        }
    }
}