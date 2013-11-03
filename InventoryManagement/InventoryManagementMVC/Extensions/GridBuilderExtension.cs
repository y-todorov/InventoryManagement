using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using InventoryManagementMVC.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.UI.Fluent;
using System.Web;
using System.Web.Mvc;
using System.Web.Security.AntiXss;
using System.Web.Util;
using System.Diagnostics;
using InventoryManagementMVC.DataAnnotations;
using RecipiesModelNS;
using System.Collections;

namespace InventoryManagementMVC.Extensions
{
    public static class GridBuilderExtension
    {
        public static IHtmlString ToMvcClientTemplate(this MvcHtmlString mvcString)
        {
            if (HttpEncoder.Current.GetType() == typeof (AntiXssEncoder))
            {
                var initial = mvcString.ToHtmlString();
                var corrected = initial.Replace("\\u0026", "&")
                    .Replace("%23", "#")
                    .Replace("%3D", "=")
                    .Replace("&#32;", " ");
                return new HtmlString(corrected);
            }

            return mvcString;
        }


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
            Type modelEntityType = typeof (T);

            builder
                .Name(modelEntityType.Name + "Grid")
                .Groupable(
                    gsb =>
                        gsb.Messages(mb => mb.Empty("Drag a column header and drop it here to group by that column"))
                            .Enabled(true))
                .Pageable(
                    pb =>
                        pb.PageSizes(new[] {10, 20, 50, 100, 500, 999})
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
            Type modelEntityType = typeof (T);
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

        // Problems with aggregates in client mode !!!!!!!!!!!!
        public static GridBuilder<T> AddColumnOptions<T>(this GridBuilder<T> builder, bool isClient = false,
            bool isDeleteColumnVisible = true,
            bool isEditColumnVisible = true, bool isSelectColumnVisible = true) where T : class
        {
            Type modelEntityType = typeof (T);
            PropertyInfo[] modelEntityProperties = modelEntityType.GetProperties();

            builder
                .Columns(columns =>
                {
                    foreach (PropertyInfo propertyInfo in modelEntityProperties)
                    {
                        if (propertyInfo.GetCustomAttributes<RelationAttribute>().Any())
                        {
                            RelationAttribute rellAttribute = propertyInfo.GetCustomAttributes<RelationAttribute>().FirstOrDefault();
                            columns.ForeignKey(propertyInfo.Name,
                               ContextFactory.Current.Set(rellAttribute.EntityType), rellAttribute.DataFieldValue, rellAttribute.DataFieldText)
                                .FooterTemplate(f => f.Count.Format("Count: {0}"))
                                .GroupFooterTemplate(f => f.Count.Format("Count: {0}"));
                        }

                        // do not show foreign key columns
                        if (propertyInfo.GetCustomAttributes<RelationAttribute>().Any() ||
                            propertyInfo.GetCustomAttributes<KeyAttribute>().Any())
                        {
                            continue;
                        }

                        if (propertyInfo.PropertyType == typeof (bool) ||
                            propertyInfo.PropertyType == typeof (bool?))
                        {
                            if (!isClient)
                            {
                                columns.Bound(propertyInfo.Name)
                                .FooterTemplate(f => f.Count.Format("Count: {0}"))
                                .GroupFooterTemplate(f => f.Count.Format("Count: {0}"));
                            }
                            else
                            {
                                columns.Bound(propertyInfo.Name)
                                .ClientFooterTemplate("Count: #= kendo.format('{0}', count)#")
                                .ClientGroupFooterTemplate("Count: #= kendo.format('{0}', count)#");
                            }
                        }
                        if (propertyInfo.PropertyType == typeof (string))
                        {
                            if (!isClient)
                            {
                                columns.Bound(propertyInfo.Name)
                                //    .ClientFooterTemplate("Count: #= kendo.format('{0}', count)#")
                                //.ClientGroupFooterTemplate("Count: #= kendo.format('{0}', count)#");
                                .FooterTemplate(f => f.Count.Format("Count: {0}"))
                                .GroupFooterTemplate(f => f.Count.Format("Count: {0}"));
                            }
                            else
                            {
                                columns.Bound(propertyInfo.Name)
                                     .FooterTemplate(f => f.Count)
                                    .GroupFooterTemplate(f => f.Count);
                                //.ClientFooterTemplate("Count: #= kendo.format('{0}', count)#")
                                //.ClientGroupFooterTemplate("Count: #= kendo.format('{0}', count)#");
                            }
                        }
                        if (propertyInfo.PropertyType == typeof (double) ||
                            propertyInfo.PropertyType == typeof (double?))
                        {
                            if (!isClient)
                            {
                                columns.Bound(propertyInfo.Name)
                                .FooterTemplate(f => f.Sum.Format("Sum: {0}"))
                                .GroupFooterTemplate(f => f.Sum.Format("Sum: {0}"));
                            }
                            else
                            {
                                columns.Bound(propertyInfo.Name);
                                //.ClientFooterTemplate("Sum: #= kendo.format('{0:F3}', sum)#")
                                //.ClientGroupFooterTemplate("Sum: #= kendo.format('{0:F3}', sum)#");
                            }
                        }
                        if (propertyInfo.PropertyType == typeof (decimal) ||
                            propertyInfo.PropertyType == typeof (decimal?))
                        {
                            if (!isClient)
                            {
                                columns.Bound(propertyInfo.Name).Format("{0:C3}")
                                 .FooterTemplate(f => f.Sum.Format("Sum: {0:C3}"))
                                 .GroupFooterTemplate(f => f.Sum.Format("Sum: {0:C3}"));
                            }
                            else
                            {
                                columns.Bound(propertyInfo.Name);
                                //.ClientFooterTemplate("Sum: #= kendo.format('{0:C3}', sum)#")
                                //.ClientGroupFooterTemplate("Sum: #= kendo.format('{0:C3}', sum)#");
                            }
                        }
                        if (propertyInfo.PropertyType == typeof (int) ||
                            propertyInfo.PropertyType == typeof (int?))
                        {
                            if (!isClient)
                            {
                                columns.Bound(propertyInfo.Name).Format("{0:N}")
                                .FooterTemplate(f => f.Sum.Format("Sum: {0:N}"))
                                .GroupFooterTemplate(f => f.Sum.Format("Sum: {0:N}"));
                            }
                            else
                            {
                                columns.Bound(propertyInfo.Name);
                                //.ClientFooterTemplate("Sum: #= kendo.format('{0:N}', sum)#")
                                //.ClientGroupFooterTemplate("Sum: #= kendo.format('{0:N}', sum)#");
                            }
                        }
                        if (propertyInfo.PropertyType == typeof (DateTime) ||
                            propertyInfo.PropertyType == typeof (DateTime?))
                        {
                            if (!isClient)
                            {
                                if (propertyInfo.Name.Equals("ModifiedDate", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    columns.Bound(propertyInfo.Name)
                                        .Format("{0:dd/MM/yyyy HH:mm:ss}")
                                    .FooterTemplate(f => f.Count.Format("Count: {0}"))
                                    .GroupFooterTemplate(f => f.Count.Format("Count: {0}"));
                                }
                                else
                                {
                                    columns.Bound(propertyInfo.Name)
                                        .Format("{0:dd/MM/yyyy}")
                                    .FooterTemplate(f => f.Count.Format("Count: {0}"))
                                    .GroupFooterTemplate(f => f.Count.Format("Count: {0}"));
                                }
                            }
                            else
                            {
                                if (propertyInfo.Name.Equals("ModifiedDate", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    columns.Bound(propertyInfo.Name)
                                        .Format("{0:dd/MM/yyyy HH:mm:ss}");
                                    //.ClientFooterTemplate("Count: #= kendo.format('{0}', count)#")
                                    //.ClientGroupFooterTemplate("Count: #= kendo.format('{0}', count)#");
                                }
                                else
                                {
                                    columns.Bound(propertyInfo.Name)
                                        .Format("{0:dd/MM/yyyy}");
                                    //.ClientFooterTemplate("Count: #= kendo.format('{0}', count)#")
                                    //.ClientGroupFooterTemplate("Count: #= kendo.format('{0}', count)#");
                                }
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

        public static GridBuilder<T> AddDataSourceOptions<T>(this GridBuilder<T> builder, bool isBatch = true)
            where T : class
        {
            Type modelEntityType = typeof (T);
            PropertyInfo[] modelEntityProperties = modelEntityType.GetProperties();

            builder
                .DataSource(dataSource => dataSource
                    .Ajax()
                    .Batch(isBatch)
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
                                    RelationAttribute rellAttribute = propertyInfo.GetCustomAttributes<RelationAttribute>().FirstOrDefault();
                                    if (rellAttribute != null)
                                {
                                    model.Field(propertyInfo.Name, propertyInfo.PropertyType).DefaultValue(-1);
                                    IEnumerator enumerator = ContextFactory.Current.Set(rellAttribute.EntityType).AsQueryable().GetEnumerator();
                                    if (enumerator.MoveNext())
                                    {
                                        var obj = enumerator.Current;
                                        if (obj != null)
                                        {
                                            Type objType = obj.GetType();
                                            PropertyInfo pi = objType.GetProperty(rellAttribute.DataFieldValue);
                                            if (pi != null)
                                            {
                                                object val = pi.GetValue(obj);
                                                model.Field(propertyInfo.Name, propertyInfo.PropertyType).DefaultValue(val);
                                            }
                                        }
                                    }
  
                                    //model.Field(propertyInfo.Name, propertyInfo.PropertyType).DefaultValue(ContextFactory.Current.Set(rellAttribute.EntityType));
                                       
                                }

                            }
                        }
                    ) // this is for editing and deleting
                    .Aggregates(a =>
                    {
                        Type[] numericTypes = new Type[]
                        {
                            typeof (int), typeof (int?), typeof (double), typeof (double?), typeof (float),
                            typeof (float?),
                            typeof (decimal), typeof (decimal?)
                        };
                        foreach (PropertyInfo pi in modelEntityProperties)
                        {
                            if (numericTypes.Contains(pi.PropertyType))
                            {
                                a.Add(pi.Name, pi.PropertyType).Average().Count().Max().Min().Sum();
                            }
                            else
                            {
                                a.Add(pi.Name, pi.PropertyType).Count();
                            }
                        }
                    })
                    .ServerOperation(false)
                    .Events(events => events.Error("error_handler")));
            return builder;
        }

        public static GridBuilder<T> AddDefaultOptions<T>(this GridBuilder<T> builder, bool isClient = false)
            where T : class
        {
            Stopwatch s = new Stopwatch();
            s.Start();

            builder
                // THIS WILL BE FOR SIGNAL R
                //.Events(ev => ev.SaveChanges("saveChanges"))
                .AddBaseOptions()
                .Editable(editable => editable.Mode(GridEditMode.InCell))
                .AddToolbarOptions(true, true)
                .AddColumnOptions(isClient, true, false, false)
                .AddDataSourceOptions();

            s.Stop();
            var mils = s.ElapsedMilliseconds;

            return builder;
        }

        public static GridBuilder<T> AddDefaultOptionsPopUpEdit<T>(this GridBuilder<T> builder) where T : class
        {
            builder
                // THIS WILL BE FOR SIGNAL R
                //.Events(ev => ev.SaveChanges("saveChanges"))
                .AddBaseOptions()
                .Editable(editable => editable.Mode(GridEditMode.PopUp))
                .AddToolbarOptions(true, true)
                .AddColumnOptions(true, false, false)
                .AddDataSourceOptions(false);

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