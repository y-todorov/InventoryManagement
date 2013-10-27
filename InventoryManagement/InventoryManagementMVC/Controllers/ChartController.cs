using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventoryManagementMVC.Models.Chart;
using RecipiesModelNS;

namespace InventoryManagementMVC.Controllers
{
    public class ChartController : Controller
    {
        public ActionResult ProductsCountByCategory()
        {
            var pc = ContextFactory.Current.ProductCategories.ToList()
                   .Select(
                       cat =>
                           new ProductsPerCategory
                           {
                               CategoryName = cat.Name.Substring(0, cat.Name.Length >= 10 ? 10 : cat.Name.Length),
                               //CategoryName = cat.Name,
                               ProductCount = cat.Products.Count,

                               ProductValue = (decimal)(cat.Products.Count != 0 ? Math.Round(cat.Products.Sum(p => p.StockValue), 3) : 0)
                           })
                   .OrderByDescending(res => res.ProductCount)
                   .ToList();


            return Json(pc);
        }

    }
}
