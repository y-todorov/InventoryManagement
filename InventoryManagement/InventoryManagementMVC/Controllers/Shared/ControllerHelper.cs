using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RecipiesModelNS;

namespace InventoryManagementMVC.Controllers
{
    public static class ControllerHelper
    {
        public static void PopulateUnitMeasures(ViewDataDictionary viewData)
        {
            List<UnitMeasure> unitMeasures = ContextFactory.Current.UnitMeasures.ToList();

            viewData["unitMeasures"] = unitMeasures;
            viewData["defaultUnitMeasure"] = unitMeasures.FirstOrDefault();
        }

        public static void PopulateCategories(ViewDataDictionary viewData)
        {
            List<ProductCategory> categories = ContextFactory.Current.ProductCategories.ToList();

            viewData["categories"] = categories;
            viewData["defaultCategory"] = categories.FirstOrDefault();
        }

        public static void PopulateStores(ViewDataDictionary viewData)
        {
            //List<Store> stores = ContextFactory.Current.Stores.ToList();
            var stores = ContextFactory.Current.Stores.ToList();


            viewData["stores"] = stores;
            viewData["defaultStore"] = stores.FirstOrDefault();
        }

        public static void PopulatePurchaseOrderHeaders(ViewDataDictionary viewData)
        {
            List<PurchaseOrderHeader> purchaseOrderHeaders = ContextFactory.Current.PurchaseOrderHeaders.ToList();

            viewData["purchaseOrderHeaders"] = purchaseOrderHeaders;
            viewData["defaultPurchaseOrderHeader"] = purchaseOrderHeaders.FirstOrDefault();
        }

        public static void PopulateProducts(ViewDataDictionary viewData)
        {
            List<Product> products = ContextFactory.Current.Products.ToList();

            viewData["products"] = products;
            viewData["defaultProduct"] = products.FirstOrDefault();
        }
    }
}