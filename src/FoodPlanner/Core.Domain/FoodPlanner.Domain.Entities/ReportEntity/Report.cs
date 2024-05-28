namespace FoodPlanner.Domain.Entities.ReportEntity;

public class Report: IReportContent
{
    public ReportId Id { get; init; }
    public ReportName Name { get; init; }
    public ReportType Type { get; init; }
    public ReportState State { get; set; }
    public MemoryStream Content { get; init; }
    public string Description { get; init; }
    public UserId CreatedBy { get; init; }
    public DateTime CreatedAt { get; init; }

    private Report() { } 

    public static Report CreateNew(ReportId id, ReportName name, ReportType type, string description, UserId createdBy)
    {
        return new()
        {
            Id = id,
            Name = name,
            Type = type,
            State = ReportState.Created,            
            CreatedBy = createdBy,
            Description = description,
            CreatedAt = DateTime.Now
        };
    }

    public MemoryStream Generate()
    {
        return Content;
    }
}
