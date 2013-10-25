using System;
using System.ComponentModel.DataAnnotations;
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
            Type modelEntityType = typeof (T);
            PropertyInfo[] modelEntityProperties = modelEntityType.GetProperties();

            builder
                .Groupable(
                    gsb =>
                        gsb.Messages(mb => mb.Empty("Drag a column header and drop it here to group by that column"))
                            .Enabled(true))
                .Pageable(pb => pb.PageSizes(true).Refresh(true).Info(true).Enabled(true).Input(true))
                .Sortable(ssb => ssb.AllowUnsort(true).Enabled(true).SortMode(GridSortMode.MultipleColumn))
                //.Scrollable(s => s.Virtual(true).Height(320))
                .ToolBar(toolbar =>
                {
                    toolbar.Create();
                    toolbar.Save();
                    //toolbar.Custom().Text("test custom column");
                })
                .Editable(editable => editable.Mode(GridEditMode.InCell))
                .Filterable()//(f => f.Extra(true).Messages(fm => fm.And("Yordan add").Filter("Yordan Filter")))
                .Reorderable(r => r.Columns(true))
                .ColumnMenu()
                //.ColumnMenu(
                //    c => c.Enabled(true).Columns(true).Filterable(true).Messages(cm => cm.Columns("yordan colimns")
                //        .Filter("yordan filter").SortAscending("yordan asc").SortDescending("yordan desc"))
                //        .Sortable(true))
                .Columns(columns =>
                {
                    //columns.AutoGenerate(false);
                    foreach (PropertyInfo propertyInfo in modelEntityProperties)
                    {
                        if (propertyInfo.PropertyType == typeof (string))
                        {
                            columns.Bound(propertyInfo.Name);
                        }
                        if (propertyInfo.PropertyType == typeof (double) ||
                            propertyInfo.PropertyType == typeof (double?))
                        {
                            columns.Bound(propertyInfo.Name).Format("{0:F3}");
                        }
                        if (propertyInfo.PropertyType == typeof (decimal) ||
                            propertyInfo.PropertyType == typeof (decimal?))
                        {
                            columns.Bound(propertyInfo.Name).Format("{0:C3}");
                            //.FooterTemplate(f => f.Sum.Format("Yordan Sum:{0:C1}")
                            /*f.Max.Format("Yordan Sum:{0:C1}"); */
                            // .FooterTemplate("<div>Min: #= min #</div><div>Max: #= max #</div>");
                        }
                        if (propertyInfo.PropertyType == typeof (DateTime) ||
                            propertyInfo.PropertyType == typeof (DateTime?))
                        {
                            if (propertyInfo.Name.Equals("ModifiedDate", StringComparison.InvariantCultureIgnoreCase))
                            {
                                columns.Bound(propertyInfo.Name).Format("{0:dd/MM/yyyy HH:mm:ss}"); 
                            }
                            else
                            {
                                columns.Bound(propertyInfo.Name);
                            }
                            //.Format("{0:MM/dd/yyyy}");//.Width(240).Title("Yordan Custom Date");
                        }
                    }
                    columns.Command(command =>
                    {
                        command.Destroy();
                        //command.Edit();
                    }); //.Width(160);
                    //columns.Command(command => { command.Edit(); command.Destroy(); }).Width(160);
                })
                .DataSource(dataSource => dataSource
                    .Ajax()
                    .Batch(true)
                    .PageSize(10)
                    .Model(
                        model =>
                        {
                            //model.Id("CategoryId");

                            PropertyInfo idPropertyInfo =
                                modelEntityProperties.FirstOrDefault(pi => pi.GetCustomAttributes<KeyAttribute>().Any());
                            if (idPropertyInfo == null)
                            {
                                throw new ApplicationException(string.Format(
                                    "The entity {0} does not have a key. You should add a KeyAttribute to a property to denote it as a primary key!",
                                    modelEntityType.FullName));
                            }

                            string idName = idPropertyInfo.Name;
                            model.Id(idName);
                            model.Field("ModifiedDate", typeof (DateTime?)).Editable(false);
                            model.Field("ModifiedByUser", typeof (string)).Editable(false);
                        }
                    ) // this is for editing and deleting
                    .Aggregates(a =>
                    {
                        foreach (PropertyInfo pi in modelEntityProperties)
                        {
                            if (pi.PropertyType == typeof (decimal) || pi.PropertyType == typeof (decimal?))
                            {
                                //a.Add(pi.Name, typeof(T)).Sum();
                                a.Add(pi.Name, pi.PropertyType).Sum().Max().Min();
                            }
                        }
                    })
                    .ServerOperation(true)
                    .Events(events => events.Error("error_handler"))
                    //.Create("Create", "Category")
                    //.Read("Read", "Category")
                    //.Update("Update", "Category")
                    //.Destroy("Destroy", "Category")
                )
                ;

            return builder;
        }
    }
}