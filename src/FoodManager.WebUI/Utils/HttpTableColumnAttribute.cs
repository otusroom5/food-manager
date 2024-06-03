namespace FoodManager.WebUI.Utils;

public sealed class HttpTableColumnAttribute: Attribute
{

    public HttpTableColumnAttribute(string caption)
    {
        Caption = caption;
    }

    public HttpTableColumnAttribute(string caption, string columnStyle) : this(caption)
    {
        ColumnStyle = columnStyle;
    }

    public HttpTableColumnAttribute(string caption, string columnStyle, string headerStyle) : this(caption, columnStyle)
    {
        HeaderStyle = headerStyle;
    }

    public string Caption { get; }

    public string HeaderStyle { get; }

    public string ColumnStyle { get; }
}
