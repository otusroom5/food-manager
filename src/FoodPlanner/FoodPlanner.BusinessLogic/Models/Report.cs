using FoodPlanner.BusinessLogic.Types;

namespace FoodPlanner.BusinessLogic.Models;

public class Report
{
    public ReportId Id { get; init; }
    public ReportName Name { get; init; }
    public ReportState State { get; set; }
    public byte[] Content { get; set; }
    public string Description { get; init; }
    public UserId CreatedBy { get; init; }
    public DateTime CreatedAt { get; init; }

    private Report() { }

    public static Report CreateNew(ReportId id, ReportName name, string description, UserId createdBy)
    {
        return new()
        {
            Id = id,
            Name = name,
            State = ReportState.Created,
            CreatedBy = createdBy,
            Description = description,
            CreatedAt = DateTime.Now
        };
    }



}
