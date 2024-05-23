using FoodManager.WebUI.Utils;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace FoodManager.WebUI.Extensions
{
    public static class TableForHtmlHelper
    {
        public static IHtmlContent TableFor<T>(this IHtmlHelper helper, T[] values,
            Func<object, IHtmlContent> deleteAction,
            Func<object, IHtmlContent> editAction)
        {
            var columns = GetColumnsByAttribute<T>();
            var keyColumn = GetKeyProperty<T>();

            var table = new TagBuilder("table");
            table.AddCssClass("table");
            table.AddCssClass("table-hover");

            var tHead = new TagBuilder("thead");
            
            var trHead = new TagBuilder("tr");
            var tdCtrlCaption = new TagBuilder("td");
            tdCtrlCaption.InnerHtml.AppendHtml("#");
            trHead.InnerHtml.AppendHtml(tdCtrlCaption);

            foreach (var column in columns) 
            {
                var tdCaption = new TagBuilder("td");
                tdCaption.InnerHtml.AppendHtml(column.Caption);
                trHead.InnerHtml.AppendHtml(tdCaption);
            }
            tHead.InnerHtml.AppendHtml(trHead);
            table.InnerHtml.AppendHtml(tHead);

            var tBody = new TagBuilder("tbody");
            
            foreach (var row in values)
            {
                var trBody = new TagBuilder("tr");
                var tdCtrl = new TagBuilder("td");
                tdCtrl.InnerHtml.AppendHtml(deleteAction?.Invoke(keyColumn.GetValue(row)));
                tdCtrl.InnerHtml.AppendHtml("&nbsp");
                tdCtrl.InnerHtml.AppendHtml(editAction?.Invoke(keyColumn.GetValue(row)));
                trBody.InnerHtml.AppendHtml(tdCtrl);

                foreach (var column in columns)
                {
                    var tdBody = new TagBuilder("td");
                    tdBody.InnerHtml.AppendHtml(column.Property.GetValue(row)?.ToString());
                    trBody.InnerHtml.AppendHtml(tdBody);
                }

                tBody.InnerHtml.AppendHtml(trBody);
            }
            table.InnerHtml.AppendHtml(tBody); 
            return table;
        }


        private static (PropertyInfo Property, string Caption)[] GetColumnsByAttribute<T>()
        {
            var result = new List<(PropertyInfo property, string caption)>();

            foreach (var property in typeof(T).GetProperties().Where(a => a.HasAttribute<HttpTableColumnAttribute>()))
            {
                HttpTableColumnAttribute attribute = property.GetCustomAttribute<HttpTableColumnAttribute>();
                result.Add((property, attribute.Caption));
            }

            return result.ToArray();
        }

        private static PropertyInfo GetKeyProperty<T>()
        {
            return typeof(T).GetProperties().FirstOrDefault(p=>p.HasAttribute<HttpTableColumnKeyAttribute>());
        }


    }
}
