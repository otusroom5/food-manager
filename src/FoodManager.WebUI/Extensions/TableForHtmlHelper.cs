using FoodManager.WebUI.Utils;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace FoodManager.WebUI.Extensions;

public static class TableForHtmlHelper
{
    public static IHtmlContent TableFor<TEntity>(this IHtmlHelper helper, TEntity[] values,
       Func<object, IHtmlContent> action)
    {
        var columns = GetColumnsByAttribute<TEntity>();
        var keyColumn = GetKeyProperty<TEntity>();
        bool needAddControlColumn = (keyColumn != null);


        if ((keyColumn == null) && 
            (action == null))
        {
            throw new ArgumentException("Key attribute is  mandatory for class");
        }

        var table = new TagBuilder("table");
        table.AddCssClass("table");
        table.AddCssClass("table-hover");

        table.InnerHtml.AppendHtml(GenerateHeaderColumns(columns, needAddControlColumn));

        var tBody = new TagBuilder("tbody");
        foreach (var row in values)
        {
            var trBody = new TagBuilder("tr");
            
            if (needAddControlColumn)
            {
                var tdCtrl = new TagBuilder("td");
                tdCtrl.InnerHtml.AppendHtml(action?.Invoke(keyColumn.GetValue(row)));
                trBody.InnerHtml.AppendHtml(tdCtrl);
            }

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

    private static IHtmlContent GenerateHeaderColumns(ColumnDefinition[] columns, bool needAddControlColumn)
    {
        var tHead = new TagBuilder("thead");

        var tr = new TagBuilder("tr");
        
        if (needAddControlColumn)
        {
            var tdCtrlCaption = new TagBuilder("td");
            tdCtrlCaption.InnerHtml.AppendHtml("#");
            tr.InnerHtml.AppendHtml(tdCtrlCaption);
        }

        foreach (var column in columns)
        {
            var td = new TagBuilder("td");
            td.InnerHtml.AppendHtml(column.Caption);
            tr.InnerHtml.AppendHtml(td);
        }

        tHead.InnerHtml.AppendHtml(tr);
        return tHead;
    }

    private static ColumnDefinition[] GetColumnsByAttribute<TEntity>()
    {
        var result = new List<ColumnDefinition>();

        foreach (var property in typeof(TEntity).GetProperties().Where(a => a.HasAttribute<HttpTableColumnAttribute>()))
        {
            HttpTableColumnAttribute attribute = property.GetCustomAttribute<HttpTableColumnAttribute>();
            result.Add(new ColumnDefinition(property, attribute.Caption));
        }

        return result.ToArray();
    }

    private static PropertyInfo GetKeyProperty<TEntity>()
    {
        return typeof(TEntity).GetProperties().FirstOrDefault(p => p.HasAttribute<HttpTableColumnKeyAttribute>());
    }
    private sealed class ColumnDefinition
    {
        public ColumnDefinition(PropertyInfo property, string caption)
        {
            Property = property;
            Caption = caption;
        }

        public PropertyInfo Property { get; }
        public string Caption { get; }
    }
}
