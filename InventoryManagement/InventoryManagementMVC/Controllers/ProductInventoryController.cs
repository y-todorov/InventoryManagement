using InventoryManagementMVC.Models;
using Kendo.Mvc.UI;
using RecipiesModelNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;

namespace InventoryManagementMVC.Controllers
{
    public class ProductInventoryController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            List<ProductInventoryViewModel> productInventoriesViewModels = ContextFactory.Current.Inventories.OfType<ProductInventory>().ToList().Select
                (pi => ProductInventoryViewModel.ConvertFromProductInventoryEntity(pi, new ProductInventoryViewModel())).ToList();
            return Json(productInventoriesViewModels.ToDataSourceResult(request));
        }

    }
}
