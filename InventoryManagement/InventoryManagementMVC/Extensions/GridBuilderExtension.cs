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
                .Pageable(
                    pb =>
                        pb.PageSizes(new[] {5, 10, 20, 50, 100, 500, 999})
                            .Refresh(true)
                            .Info(true)
                            .Enabled(true)
                            .Input(true))
                .Sortable(ssb => ssb.AllowUnsort(true).Enabled(true).SortMode(GridSortMode.SingleColumn))
                .ToolBar(toolbar =>
                {
                    toolbar.Create();
                    toolbar.Save();
                })
                .Editable(editable => editable.Mode(GridEditMode.InCell))
                .Filterable()
                .Reorderable(r => r.Columns(true))
                .ColumnMenu()
                .Columns(columns =>
                {
                    foreach (PropertyInfo propertyInfo in modelEntityProperties)
                    {
                        // do not show foreign key columns
                        if (propertyInfo.GetCustomAttributes<AssociationAttribute>().Any())
                        {
                            continue;
                        }

                        if (propertyInfo.PropertyType == typeof (bool) ||
                            propertyInfo.PropertyType == typeof (bool?))
                        {
                            columns.Bound(propertyInfo.Name);
                        }
                        if (propertyInfo.PropertyType == typeof (string))
                        {
                            columns.Bound(propertyInfo.Name);
                        }
                        if (propertyInfo.PropertyType == typeof (double) ||
                            propertyInfo.PropertyType == typeof (double?))
                        {
                            columns.Bound(propertyInfo.Name).ClientFooterTemplate("#= kendo.format('{0:F3}', sum)#"); //Σ: 
                        }
                        if (propertyInfo.PropertyType == typeof (decimal) ||
                            propertyInfo.PropertyType == typeof (decimal?))
                        {
                            columns.Bound(propertyInfo.Name).Format("{0:C3}")
                                .ClientFooterTemplate("#= kendo.format('{0:C3}', sum)#");
                        }
                        if (propertyInfo.PropertyType == typeof (DateTime) ||
                            propertyInfo.PropertyType == typeof (DateTime?))
                        {
                            if (propertyInfo.Name.Equals("ModifiedDate", StringComparison.InvariantCultureIgnoreCase))
                            {
                                columns.Bound(propertyInfo.Name)
                                    .Format("{0:dd/MM/yyyy HH:mm:ss}")
                                    .ClientFooterTemplate("");
                            }
                            else
                            {
                                columns.Bound(propertyInfo.Name);
                            }
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
                            model.Field(idName, typeof (int)).Editable(false);
                            model.Field("ModifiedDate", typeof (DateTime?)).Editable(false);
                            model.Field("ModifiedByUser", typeof (string)).Editable(false);
                            foreach (PropertyInfo propertyInfo in modelEntityProperties)
                            {
                                if (propertyInfo.Name != idName && propertyInfo.Name != "ModifiedDate" &&
                                    propertyInfo.Name != "ModifiedByUser")
                                {
                                    model.Field(propertyInfo.Name, propertyInfo.PropertyType)
                                        .DefaultValue(GetDefaultValueForType(propertyInfo.PropertyType));
                                }
                            }
                        }
                    ) // this is for editing and deleting
                    .Aggregates(a =>
                    {
                        foreach (PropertyInfo pi in modelEntityProperties)
                        {
                            if (pi.PropertyType == typeof (decimal) || pi.PropertyType == typeof (decimal?))
                            {
                                //a.Add(pi.Name, typeof(T)).Sum();
                                a.Add(pi.Name, pi.PropertyType).Sum().Max().Min().Count().Average();
                            }
                            else if (pi.PropertyType == typeof (double) || pi.PropertyType == typeof (double?))
                            {
                                a.Add(pi.Name, pi.PropertyType).Sum().Max().Min();
                            }
                            else if (pi.PropertyType == typeof (DateTime) || pi.PropertyType == typeof (DateTime?))
                            {
                                a.Add(pi.Name, pi.PropertyType).Max().Min();
                            }
                        }
                    })
                    .ServerOperation(false)
                    .Events(events => events.Error("error_handler"))
                );

            return builder;
        }

        private static object GetDefaultValueForType(Type t)
        {
            Type baseType = Nullable.GetUnderlyingType(t);
            if (baseType != null)
            {
                return Activator.CreateInstance(baseType);
            }
            if (t.IsValueType)
            {
                return Activator.CreateInstance(t);
            }
            return null;
        }
    }
}