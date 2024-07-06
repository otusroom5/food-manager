using FoodManager.WebUI.Contracts;

namespace FoodManager.WebUI.Areas.Manager.Contracts.Responses
{ 
    public sealed class ExpiredProductsReportResponse : ResponseBase
    {
        public Report Data { get; set; }
    }
}
