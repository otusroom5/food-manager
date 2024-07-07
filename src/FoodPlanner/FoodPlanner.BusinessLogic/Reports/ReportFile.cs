using System.Text;

namespace FoodPlanner.BusinessLogic.Reports;

public class ReportFile
{
    public string Header { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public string Footer{ get; set; } = string.Empty;
    public override string ToString() =>
        new StringBuilder()
        .AppendLine(Header)
        .AppendLine(Body)
        .AppendLine(Footer)
        .ToString();
}
