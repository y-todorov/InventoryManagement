using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using InventoryManagementMVC.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using RecipiesModelNS;

namespace InventoryManagementMVC.Controllers
{
    public class CategoryController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            List<CategoryViewModel> categoryViewModels = ContextFactory.Current.ProductCategories.ToList().Select
                (c => CategoryViewModel.ConvertFromCategoryEntity(c, new CategoryViewModel())).ToList();
            return Json(categoryViewModels.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<CategoryViewModel> categories)
        {
            if (categories != null && ModelState.IsValid)
            {
                foreach (CategoryViewModel categoryViewModel in categories)
                {
                    ProductCategory newCategory = CategoryViewModel.ConvertToCategoryEntity(categoryViewModel,
                        new ProductCategory());
                    ContextFactory.Current.ProductCategories.Add(newCategory);
                    ContextFactory.Current.SaveChanges();
                    CategoryViewModel.ConvertFromCategoryEntity(newCategory, categoryViewModel);
                }
            }

            return Json(categories.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<CategoryViewModel> categories)
        {
            if (categories != null && ModelState.IsValid)
            {
                foreach (CategoryViewModel categoryViewModel in categories)
                {
                    ProductCategory categoryEntity =
                        ContextFactory.Current.ProductCategories.FirstOrDefault(c => c.CategoryId == categoryViewModel.CategoryId);

                    CategoryViewModel.ConvertToCategoryEntity(categoryViewModel, categoryEntity);
                    
                    ContextFactory.Current.SaveChanges();

                    CategoryViewModel.ConvertFromCategoryEntity(categoryEntity, categoryViewModel);
                }
            }

            return Json(categories.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<CategoryViewModel> categories)
        {
            if (categories.Any())
            {
                foreach (CategoryViewModel category in categories)
                {
                    ProductCategory productCategory = ContextFactory.Current.ProductCategories.FirstOrDefault(c => c.CategoryId == category.CategoryId);
                    ContextFactory.Current.ProductCategories.Remove(productCategory);

                    ContextFactory.Current.SaveChanges();
                }
            }

            return Json(categories.ToDataSourceResult(request, ModelState));
        }
    }
}