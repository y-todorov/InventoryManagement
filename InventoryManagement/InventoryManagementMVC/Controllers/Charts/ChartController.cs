using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventoryManagementMVC.Models.Chart;
using RecipiesModelNS;
using System.Data.Objects;
using System.Data.Entity; // .Include !!!!!!! THIS IS SO IMPROTANT

namespace InventoryManagementMVC.Controllers
{
    public class ChartController : CustomControllerBase
    {
        public ActionResult ProductsCountByCategory()
        {
            var pc = ContextFactory.Current.ProductCategories.Include(c => c.Products).ToList()
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
