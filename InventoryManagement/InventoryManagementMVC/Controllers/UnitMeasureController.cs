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
    public class UnitMeasureController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            List<UnitMeasureViewModel> cvms = ContextFactory.Current.UnitMeasures.Select
                (unit => new UnitMeasureViewModel()
                {
                    Name = unit.Name,
                    BaseUnitId = unit.BaseUnitId,
                    IsBaseUnit = unit.IsBaseUnit,
                    UnitMeasureId = unit.UnitMeasureId,
                    ModifiedDate = unit.ModifiedDate,//.GetValueOrDefault().ToString(Thread.CurrentThread.CurrentUICulture);,
                    ModifiedByUser = unit.ModifiedByUser
                }).ToList();
            return Json(cvms.ToDataSourceResult(request));
        }

    }
}
