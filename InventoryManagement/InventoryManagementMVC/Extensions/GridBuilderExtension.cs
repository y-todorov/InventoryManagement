using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Kendo.Mvc.UI;
using Kendo.Mvc.UI.Fluent;

namespace InventoryManagementMVC.Extensions
{
    public static class GridBuilderExtension
    {
        public static GridBuilder<T> AddReadOnlyOptions<T>(this GridBuilder<T> builder) where T : class
        {
            builder
                 .AddBaseOptions()
                 .Editable(editable => editable.Enabled(false))
                 .AddToolbarOptions(false, false)
                 .AddColumnOptions(false, false, false)
                 .AddDataSourceOptions();

            return builder;
        }

        public static GridBuilder<T> AddBaseOptions<T>(this GridBuilder<T> builder) where T : class
        {
            Type modelEntityType = typeof(T);

            builder
                .Name(modelEntityType.Name + "Grid")
                .Groupable(
                    gsb =>
                        gsb.Messages(mb => mb.Empty("Drag a column header and drop it here to group by that column"))
                            .Enabled(true))
                .Pageable(
                    pb =>
                        pb.PageSizes(new[] { 10, 20, 50, 100, 500, 999 })
                            .Refresh(true)
                            .Info(true)
                            .Enabled(true)
                            .Input(true))
                .Sortable(ssb => ssb.AllowUnsort(true).Enabled(true).SortMode(GridSortMode.SingleColumn))
                .Filterable()
                .Reorderable(r => r.Columns(true))
                .Resizable(resize => resize.Columns(true))
                .ColumnMenu();
            return builder;
        }

        public static GridBuilder<T> AddToolbarOptions<T>(this GridBuilder<T> builder, bool isCreateVisible = true,
            bool isSaveVisible = true) where T : class
        {
            Type modelEntityType = typeof(T);
            PropertyInfo[] modelEntityProperties = modelEntityType.GetProperties();

            builder
                .ToolBar(toolbar =>
                {
                    if (isSaveVisible)
                    {
                        toolbar.Create();
                    }
                    if (isSaveVisible)
                    {
                        toolbar.Save();
                    }
                });
            return builder;
        }

        public static GridBuilder<T> AddColumnOptions<T>(this GridBuilder<T> builder, bool isDeleteColumnVisible = true,
            bool isEditColumnVisible = true, bool isSelectColumnVisible = true) where T : class
        {
            Type modelEntityType = typeof(T);
            PropertyInfo[] modelEntityProperties = modelEntityType.GetProperties();

            builder
                .Columns(columns =>
                {
                    foreach (PropertyInfo propertyInfo in modelEntityProperties)
                    {
                        // do not show foreign key columns
                        if (propertyInfo.GetCustomAttributes<AssociationAttribute>().Any() ||
                            propertyInfo.GetCustomAttributes<KeyAttribute>().Any())
                        {
                            continue;
                        }

                        if (propertyInfo.PropertyType == typeof(bool) ||
                            propertyInfo.PropertyType == typeof(bool?))
                        {
                            columns.Bound(propertyInfo.Name)
                                .ClientFooterTemplate("Count: #= kendo.format('{0:F3}', count)#")
                                .ClientGroupFooterTemplate("Count: #= kendo.format('{0:F3}', count)#");
                            //.ClientGroupHeaderTemplate(string.Format("{0}: #= value # (Count: #= count#)", propertyInfo.Name));
                        }
                        if (propertyInfo.PropertyType == typeof(string))
                        {
                            columns.Bound(propertyInfo.Name)
                                .ClientFooterTemplate("Count: #= kendo.format('{0:F3}', count)#")
                                .ClientGroupFooterTemplate("Count: #= kendo.format('{0:F3}', count)#");
                        }
                        if (propertyInfo.PropertyType == typeof(double) ||
                            propertyInfo.PropertyType == typeof(double?))
                        {
                            columns.Bound(propertyInfo.Name)
                                .ClientFooterTemplate("Sum: #= kendo.format('{0:F3}', sum)#")
                                .ClientGroupFooterTemplate("Sum: #= kendo.format('{0:F3}', sum)#");
                        }
                        if (propertyInfo.PropertyType == typeof(decimal) ||
                            propertyInfo.PropertyType == typeof(decimal?))
                        {
                            columns.Bound(propertyInfo.Name).Format("{0:C3}")
                                .ClientFooterTemplate("Sum: #= kendo.format('{0:C3}', sum)#")
                                .ClientGroupFooterTemplate("Sum: #= kendo.format('{0:C3}', sum)#");
                        }
                        if (propertyInfo.PropertyType == typeof(int) ||
                            propertyInfo.PropertyType == typeof(int?))
                        {
                            columns.Bound(propertyInfo.Name).Format("{0:N}")
                                .ClientFooterTemplate("Sum: #= kendo.format('{0:N}', sum)#")
                                .ClientGroupFooterTemplate("Sum: #= kendo.format('{0:N}', sum)#");
                        }
                        if (propertyInfo.PropertyType == typeof(DateTime) ||
                            propertyInfo.PropertyType == typeof(DateTime?))
                        {
                            if (propertyInfo.Name.Equals("ModifiedDate", StringComparison.InvariantCultureIgnoreCase))
                            {
                                columns.Bound(propertyInfo.Name)
                                    .Format("{0:dd/MM/yyyy HH:mm:ss}")
                                    .ClientFooterTemplate("Count: #= kendo.format('{0:F3}', count)#")
                                    .ClientGroupFooterTemplate("Count: #= kendo.format('{0:F3}', count)#");
                            }
                            else
                            {
                                columns.Bound(propertyInfo.Name)
                                    .Format("{0:dd/MM/yyyy}")
                                    .ClientFooterTemplate("Count: #= kendo.format('{0:F3}', count)#")
                                    .ClientGroupFooterTemplate("Count: #= kendo.format('{0:F3}', count)#");
                            }
                        }
                    }
                    if (isDeleteColumnVisible || isEditColumnVisible || isSelectColumnVisible)
                    {
                        columns.Command(command =>
                        {
                            if (isDeleteColumnVisible)
                            {
                                command.Destroy().Text("Delete");
                            }
                            if (isEditColumnVisible)
                            {
                                command.Edit().Text("Edit");
                            }
                            if (isSelectColumnVisible)
                            {
                                command.Select().Text("Select");
                            }
                        }); //.ClientFooterTemplate("Delete");
                    }
                });
            return builder;
        }

        public static GridBuilder<T> AddDataSourceOptions<T>(this GridBuilder<T> builder) where T : class
        {
            Type modelEntityType = typeof(T);
            PropertyInfo[] modelEntityProperties = modelEntityType.GetProperties();

            builder
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
                            model.Field(idName, typeof(int)).Editable(false);
                            model.Field("ModifiedDate", typeof(DateTime?)).Editable(false);
                            model.Field("ModifiedByUser", typeof(string)).Editable(false);
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
                            a.Add(pi.Name, pi.PropertyType).Average().Count().Max().Min().Sum();
                        }
                    })
                    .ServerOperation(false)
                    .Events(events => events.Error("error_handler")));
            return builder;
        }

        public static GridBuilder<T> AddDefaultOptions<T>(this GridBuilder<T> builder) where T : class
        {
            builder
                .AddBaseOptions()
                .Editable(editable => editable.Mode(GridEditMode.InCell))
                .AddToolbarOptions(true, true)
                .AddColumnOptions(true, false, false)
                .AddDataSourceOptions();

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