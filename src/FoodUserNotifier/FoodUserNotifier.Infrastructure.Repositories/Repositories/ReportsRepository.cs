using FoodUserNotifier.Core.Entities;
using FoodUserNotifier.Core.Interfaces.Repositories;
using FoodUserNotifier.Infrastructure.Repositories.Context;
using Microsoft.EntityFrameworkCore;

namespace FoodUserNotifier.Infrastucture.Repositories;

internal class ReportsRepository : IReportsRepository
{
    private readonly DatabaseContext _databaseContext;

    public ReportsRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public void Create(Report report)
    {
        _databaseContext.Add(report);
    }

    public async Task<Report> Get(Guid id)
    {
        return await _databaseContext.Reports.FirstOrDefaultAsync(r => r.Id == id);
    }
}
