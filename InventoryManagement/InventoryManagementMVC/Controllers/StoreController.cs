using InventoryManagementMVC.Models;
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

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<StoreViewModel> stores)
        {
            if (stores != null && ModelState.IsValid)
            {
                foreach (StoreViewModel store in stores)
                {
                    Store newStore = StoreViewModel.ConvertToStoreEntity(store,
                        new Store());
                    ContextFactory.Current.Stores.Add(newStore);
                    ContextFactory.Current.SaveChanges();
                    StoreViewModel.ConvertFromStoreEntity(newStore, store);
                }
            }

            return Json(stores.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<StoreViewModel> stores)
        {
            if (stores != null && ModelState.IsValid)
            {
                foreach (StoreViewModel storeViewModel in stores)
                {
                    Store storeEntity =
                        ContextFactory.Current.Stores.FirstOrDefault(c => c.StoreId == storeViewModel.StoreId);

                    StoreViewModel.ConvertToStoreEntity(storeViewModel, storeEntity);

                    ContextFactory.Current.SaveChanges();

                    StoreViewModel.ConvertFromStoreEntity(storeEntity, storeViewModel);
                }
            }

            return Json(stores.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<StoreViewModel> stores)
        {
            if (stores.Any())
            {
                foreach (StoreViewModel store in stores)
                {
                    Store storeEntity = ContextFactory.Current.Stores.FirstOrDefault(c => c.StoreId == store.StoreId);
                    ContextFactory.Current.Stores.Remove(storeEntity);

                    ContextFactory.Current.SaveChanges();
                }
            }

            return Json(stores.ToDataSourceResult(request, ModelState));
        }

    }
}
