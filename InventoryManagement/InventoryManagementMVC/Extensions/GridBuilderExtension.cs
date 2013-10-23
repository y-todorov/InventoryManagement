using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Kendo.Mvc.UI;
using Kendo.Mvc.UI.Fluent;

namespace InventoryManagementMVC.Extensions
{
    public static class GridBuilderExtension
    {
        public static GridBuilder<T> AddDefaultOptions<T>(this GridBuilder<T> builder) where T : class
        {
            Type t = typeof (T);
            PropertyInfo[] props = t.GetProperties();

            builder
                .Groupable(gsb => gsb.Messages(mb => mb.Empty("Yordan not grouped")).Enabled(true))
                .Pageable(pb => pb.PageSizes(true).Refresh(true).Info(true).Enabled(true).Input(true))
                .Sortable(ssb => ssb.AllowUnsort(true).Enabled(true).SortMode(GridSortMode.MultipleColumn))
                .Scrollable(s => s.Virtual(true).Height(320))
                .ToolBar(toolbar =>
                {
                    toolbar.Create();
                    toolbar.Save();

                    toolbar.Custom().Text("yodan command");
                })
                .Editable(editable => editable.Mode(GridEditMode.InCell))
                .Filterable(f => f.Extra(true).Messages(fm => fm.And("Yordan add").Filter("Yordan Filter")))
                .Reorderable(r => r.Columns(true))
                .ColumnMenu(
                    c => c.Enabled(true).Columns(true).Filterable(true).Messages(cm => cm.Columns("yordan colimns")
                        .Filter("yordan filter").SortAscending("yordan asc").SortDescending("yordan desc"))
                        .Sortable(true))
                //.DataSource(dataSource => dataSource
                //    .Ajax()
                //    .Read(read => read.Action("Read", "Category")))
                .Columns(columns =>
                {
                    //columns.AutoGenerate(false);
                    foreach (PropertyInfo pi in props)
                    {
                        if (pi.PropertyType == typeof (string))
                        {
                            columns.Bound(pi.Name);
                        }
                        if (pi.PropertyType == typeof (double) || pi.PropertyType == typeof (double?))
                        {
                            columns.Bound(pi.Name);
                        }
                        if (pi.PropertyType == typeof (decimal) || pi.PropertyType == typeof (decimal?))
                        {
                            columns.Bound(pi.Name).Format("{0:C3}")
                                .FooterTemplate(f => f.Sum.Format("Yordan Sum:{0:C1}")
                                /*f.Max.Format("Yordan Sum:{0:C1}"); */);
                            // .FooterTemplate("<div>Min: #= min #</div><div>Max: #= max #</div>");
                        }
                        if (pi.PropertyType == typeof (DateTime) || pi.PropertyType == typeof (DateTime?))
                        {
                            columns.Bound(pi.Name).Format("{0:MM/dd/yyyy}").Width(240).Title("Yordan Custom Date");
                        }
                    }
                    columns.Command(command =>
                    {
                        command.Destroy();
                        command.Edit();
                    }).Width(160);
                    //columns.Command(command => { command.Edit(); command.Destroy(); }).Width(160);
                })
                .DataSource(dataSource => dataSource
                    .Ajax()
                    .PageSize(20)
                    .Model(model => model.Id("CategoryId")) // this is for editing and deleting
                    .Aggregates(a =>
                    {
                        foreach (PropertyInfo pi in props)
                        {
                            if (pi.PropertyType == typeof (decimal) || pi.PropertyType == typeof (decimal?))
                            {
                                //a.Add(pi.Name, typeof(T)).Sum();
                                a.Add(pi.Name, pi.PropertyType).Sum().Max().Min();
                            }
                        }
                    })
                    .ServerOperation(false)
                    //.Events(events => events.Error("error_handler"))
                    .Create("Create", "Category")
                    .Read("Read", "Category")
                    .Update("Update", "Category")
                    .Destroy("Destroy", "Category")
                )
                ;

            return builder;
        }
    }
}