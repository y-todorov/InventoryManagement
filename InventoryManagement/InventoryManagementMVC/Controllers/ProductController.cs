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
            return View();
        }

        public void PopulateCategories()
        {
            //List<CategoryViewModel> cvms = ContextFactory.Current.ProductCategories.Select
            //    (c => new CategoryViewModel()
            //    {
            //        CategoryId = c.CategoryId,
            //        Name = c.Name,
            //        ModifiedDate = c.ModifiedDate,//.GetValueOrDefault().ToString(Thread.CurrentThread.CurrentUICulture);,
            //        ModifiedByUser = c.ModifiedByUser
            //    }).ToList();

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
                    UnitMeasure = p.UnitMeasure.Name,
                    CategoryId = p.CategoryId,
                    Store = p.Store.Name,
                    Name = p.Name,
                    Code = p.Code,
                    UnitPrice = p.UnitPrice,
                    UnitsInStock = p.UnitsInStock,
                    UnitsOnOrder = p.UnitsOnOrder,
                    ReorderLevel = p.ReorderLevel,
                    StockValue = (decimal)p.StockValue,
                    ModifiedDate = p.ModifiedDate,
                    ModifiedByUser = p.ModifiedByUser
                }).ToList();
            return Json(cvms.ToDataSourceResult(request));
        }

    }
}
