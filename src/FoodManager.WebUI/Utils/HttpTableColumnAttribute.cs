namespace FoodManager.WebUI.Utils;

public class HttpTableColumnAttribute: Attribute
{
    public HttpTableColumnAttribute(string caption) 
    { 
        Caption = caption;
    }

    public string Caption { get; }
}
