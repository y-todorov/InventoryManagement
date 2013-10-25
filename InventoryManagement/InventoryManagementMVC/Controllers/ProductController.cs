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
            return View();
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            List<Product> allProducts = ContextFactory.Current.Products.ToList();
            List<ProductViewModel> cvms = allProducts.Select
                (p => new ProductViewModel()
                {
                    ProductId = p.ProductId,
                    UnitMeasure = p.UnitMeasure.Name,
                    Category = p.ProductCategory.Name,
                    Store = p.Store.Name,
                    Name = p.Name,
                    Code = p.Code,
                    UnitPrice = p.UnitPrice,
                    UnitsInStock = p.UnitsInStock,
                    UnitsOnOrder = p.UnitsOnOrder,
                    ReorderLevel = p.ReorderLevel,
                    StockValue = (decimal)p.StockValue,
                    ModifiedDate = p.ModifiedDate,//.GetValueOrDefault().ToString(Thread.CurrentThread.CurrentUICulture);,
                    ModifiedByUser = p.ModifiedByUser
                }).ToList();
            return Json(cvms.ToDataSourceResult(request));
        }

    }
}
