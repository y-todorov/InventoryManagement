using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using InventoryManagementMVC.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using RecipiesModelNS;

namespace InventoryManagementMVC.Controllers
{
    public class UnitMeasureController : Controller
    {
        public ActionResult Index()
        {
            List<UnitMeasureViewModel> unitMeasuresViewModels = ContextFactory.Current.UnitMeasures.ToList().Select
                (unit => UnitMeasureViewModel.ConvertFromUnitMeasureEntity(unit, new UnitMeasureViewModel())).ToList();
            return View(unitMeasuresViewModels);
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            List<UnitMeasureViewModel> unitMeasuresViewModels = ContextFactory.Current.UnitMeasures.ToList().Select
                (unit => UnitMeasureViewModel.ConvertFromUnitMeasureEntity(unit, new UnitMeasureViewModel())).ToList();
            return Json(unitMeasuresViewModels.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<UnitMeasureViewModel> unitMeasures)
        {
            if (unitMeasures != null && ModelState.IsValid)
            {
                foreach (UnitMeasureViewModel unitMeasureViewModel in unitMeasures)
                {
                    UnitMeasure newUnitMeasure = UnitMeasureViewModel.ConvertToUnitMeasureEntity(unitMeasureViewModel,
                        new UnitMeasure());
                    ContextFactory.Current.UnitMeasures.Add(newUnitMeasure);
                    ContextFactory.Current.SaveChanges();
                    UnitMeasureViewModel.ConvertFromUnitMeasureEntity(newUnitMeasure, unitMeasureViewModel);
                }
            }

            return Json(unitMeasures.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<UnitMeasureViewModel> unitMeasures)
        {
            if (unitMeasures != null && ModelState.IsValid)
            {
                foreach (UnitMeasureViewModel unitMeasureViewModel in unitMeasures)
                {
                    UnitMeasure unitMeasureEntity =
                        ContextFactory.Current.UnitMeasures.FirstOrDefault(
                            u => u.UnitMeasureId == unitMeasureViewModel.UnitMeasureId);

                    UnitMeasureViewModel.ConvertToUnitMeasureEntity(unitMeasureViewModel, unitMeasureEntity);

                    ContextFactory.Current.SaveChanges();

                    UnitMeasureViewModel.ConvertFromUnitMeasureEntity(unitMeasureEntity, unitMeasureViewModel);
                }
            }

            return Json(unitMeasures.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<UnitMeasureViewModel> unitMeasures)
        {
            if (unitMeasures.Any())
            {
                foreach (UnitMeasureViewModel unitMeasureViewModel in unitMeasures)
                {
                    UnitMeasure unitMeasure =
                        ContextFactory.Current.UnitMeasures.FirstOrDefault(
                            unit => unit.UnitMeasureId == unitMeasureViewModel.UnitMeasureId);
                    ContextFactory.Current.UnitMeasures.Remove(unitMeasure);

                    ContextFactory.Current.SaveChanges();
                }
            }

            return Json(unitMeasures.ToDataSourceResult(request, ModelState));
        }
    }
}