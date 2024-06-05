using FoodPlanner.BusinessLogic.Interfaces;

namespace FoodPlanner.BusinessLogic.Reports;

public class ExpiredProductsReport: IReport
{
    public byte[] Prepare()
    {
        return Array.Empty<byte>();
    }
}
