namespace FoodManager.WebUI.Areas.Cooker.Models;

public class ProductItemCreateModel
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public string UnitType { get; set; }
    public double Amount { get; set; }
    public string UnitId { get; set; }
    private DateTime? _creatingDate;
    public DateTime? CreatingDate 
    {
        get
        {
            if (_creatingDate == null)
            {
                return DateTime.Now.Date;
            }

            return _creatingDate;
        }
        set
        {
            _creatingDate = value;
        } 
    }
}
