using FoodPlanner.BusinessLogic.Interfaces;
using FoodPlanner.BusinessLogic.Services;
using FoodPlanner.DataAccess.Interfaces;
using System.Text;

namespace FoodPlanner.BusinessLogic.Reports;

public class ExpiredProductsReport: IReport
{
    private readonly IStorageRepository _storageRepository;
    public ExpiredProductsReport(IStorageRepository storageRepository) 
    {
        _storageRepository = storageRepository;
    }

    public byte[] Prepare()
    {
        var justForTest = new StringBuilder();        
        foreach (var item in _storageRepository.GetExpiredProductsAsync().Result)
        {
            justForTest.AppendLine(item.ProductId.ToString());
        }

        return new PdfHandlerService().CreateDocument(justForTest.ToString());
    }   
}
