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
            PopulateStores();
            return View();
        }

        private void PopulateStores()
        {
            List<Store> stores = ContextFactory.Current.Stores.ToList();

            ViewData["stores"] = stores;
            ViewData["defaultStore"] = stores.FirstOrDefault();
        }

        private void PopulateUnitMeasures()
        {
            List<UnitMeasure> unitMeasures = ContextFactory.Current.UnitMeasures.ToList();

            ViewData["unitMeasures"] = unitMeasures;
            ViewData["defaultUnitMeasure"] = unitMeasures.FirstOrDefault();
        }

        public void PopulateCategories()
        {
            List<ProductCategory> categories = ContextFactory.Current.ProductCategories.ToList();

            ViewData["categories"] = categories;
            ViewData["defaultCategory"] = categories.FirstOrDefault();
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
                    StockValue = (decimal)p.StockValue,
                    ModifiedDate = p.ModifiedDate,
                    ModifiedByUser = p.ModifiedByUser
                }).ToList();
            return Json(cvms.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<ProductViewModel> products)
        {
            var results = new List<ProductViewModel>();

            if (products != null && ModelState.IsValid)
            {
                foreach (var product in products)
                {
                    Product newProduct = new Product()
                    {
                        CategoryId = product.CategoryId,
                        Code = product.Code,
                        Name = product.Name,
                        ReorderLevel = product.ReorderLevel,
                        StockValue = (double)product.StockValue.GetValueOrDefault(),
                        StoreId = product.StoreId,
                        UnitMeasureId = product.UnitMeasureId,
                        UnitPrice = product.UnitPrice,
                        UnitsInStock = product.UnitsInStock,
                        UnitsOnOrder = product.UnitsOnOrder,
                    };
                    newProduct = ContextFactory.Current.Products.Add(newProduct);
                    ContextFactory.Current.SaveChanges();

                    product.ModifiedByUser = newProduct.ModifiedByUser;
                    product.ModifiedDate = newProduct.ModifiedDate;
                    results.Add(product);
                }
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }
    }
}