using FoodPlanner.BusinessLogic.Exceptions;

namespace FoodPlanner.BusinessLogic.Models;

public record ReportId
{
    private readonly Guid _value;

    private ReportId(Guid value)
    {
        _value = value;
    }

    public Guid ToGuid() => _value;

    public static ReportId FromGuid(Guid value)
    {
        value.ValidateOrThrow(nameof(ReportId));
        return new ReportId(value);
    }

    public static ReportId CreateNew() => new(Guid.NewGuid());
}
