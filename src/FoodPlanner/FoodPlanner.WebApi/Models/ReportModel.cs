using FoodPlanner.BusinessLogic.Types;

namespace FoodPlanner.WebApi.Models;

public class ReportModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ReportType Type { get; set; }
    public ReportState State { get; set; }
    public MemoryStream Content { get; set; }
    public string Description { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
}
