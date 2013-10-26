using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventoryManagementMVC.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using RecipiesModelNS;

namespace InventoryManagementMVC.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Index()
        {
            PopulateCategories();
            PopulateUnitMeasures();
            return View();
        }

        private void PopulateUnitMeasures()
        {
            List<UnitMeasure> unitMeasures = ContextFactory.Current.UnitMeasures.ToList();

            ViewData["unitMeasures"] = unitMeasures;
            ViewData["defaultUnitMeasure"] = unitMeasures.First();
        }

        public void PopulateCategories()
        {
            List<ProductCategory> categories = ContextFactory.Current.ProductCategories.ToList();

            ViewData["categories"] = categories;
            ViewData["defaultCategory"] = categories.First();
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            List<Product> allProducts = ContextFactory.Current.Products.ToList();
            List<ProductViewModel> cvms = allProducts.Select
                (p => new ProductViewModel()
                {
                    ProductId = p.ProductId,
                    UnitMeasureId = p.UnitMeasureId,
                    CategoryId = p.CategoryId,
                    StoreId = p.StoreId,
                    Name = p.Name,
                    Code = p.Code,
                    UnitPrice = Math.Round(p.UnitPrice.GetValueOrDefault(), 3),
                    UnitsInStock = Math.Round(p.UnitsInStock.GetValueOrDefault(), 3),
                    UnitsOnOrder = Math.Round(p.UnitsOnOrder.GetValueOrDefault(), 3),
                    ReorderLevel = Math.Round(p.ReorderLevel.GetValueOrDefault(), 3),
                    StockValue = (decimal) p.StockValue,
                    ModifiedDate = p.ModifiedDate,
                    ModifiedByUser = p.ModifiedByUser
                }).ToList();
            return Json(cvms.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<ProductViewModel> products)
        {
            var results = new List<CategoryViewModel>();

            if (products != null && ModelState.IsValid)
            {
                foreach (var product in products)
                {
                    //ProductCategory newCategory = ContextFactory.Current.ProductCategories.Add(new ProductCategory()
                    //{
                    //    CategoryId = product.CategoryId,
                    //    Name = product.Name,
                    //    ModifiedByUser = product.ModifiedByUser,
                    //    ModifiedDate = product.ModifiedDate
                    //});
                    //ContextFactory.Current.SaveChanges();
                    //product.ModifiedByUser = newCategory.ModifiedByUser;
                    //product.ModifiedDate = newCategory.ModifiedDate;
                    //results.Add(product);
                }
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }
    }
}