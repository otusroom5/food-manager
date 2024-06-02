namespace FoodStorage.Application.Implementations.Common;

public class CheckExpiredProductsConfiguration
{
    public const string ReportExpireProductsConfig = "ReportExpireProductsConfig";

    /// <summary>
    /// В какое время дня будет срабатывать проверка продуктов в холодильнике на истечение срока годности
    /// </summary>
    public string CheckExpiredProductsTimeOfDay { get; set; }

    /// <summary>
    /// За какой промежуток времени в днях нужно оповещать о скором порчении продукта
    /// </summary>
    public int TimeBeforeExpireInDaysForReport { get; set; }
}
