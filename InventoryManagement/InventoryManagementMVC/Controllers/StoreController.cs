using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using RecipiesModelNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagementMVC.Controllers
{
    public class StoreController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            List<StoreViewModel> categoryViewModels = ContextFactory.Current.Stores.ToList().Select
                (c => StoreViewModel.ConvertFromStoreEntity(c, new StoreViewModel())).ToList();
            return Json(categoryViewModels.ToDataSourceResult(request));
        }

    }
}
