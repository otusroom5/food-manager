using FoodPlanner.Domain.Entities.Common.Exceptions;

namespace FoodPlanner.Domain.Entities.ReportEntity;

public record ReportName
{
    private readonly string _value;

    private ReportName(string value)
    {
        _value = value;
    }

    public override string ToString() => _value.ToString();

    public static ReportName FromString(string reportName)
    {
        if (string.IsNullOrWhiteSpace(reportName))
        {
            throw new InvalidArgumentValueException("Report name is empty.", nameof(ReportName));
        }

        if (reportName.Length is < 2 or > 100)
        {
            throw new InvalidArgumentValueException("Report name is invalid. Name length must be in 2..100 characters.", nameof(ReportName));
        }

        return new ReportName(reportName);
    }
}
