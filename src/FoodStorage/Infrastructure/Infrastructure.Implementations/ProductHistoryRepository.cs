using FoodStorage.Application.Repositories;
using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.ProductHistoryEntity;
using FoodStorage.Infrastructure.EntityFramework;
using FoodStorage.Infrastructure.EntityFramework.Common.Exceptions;
using FoodStorage.Infrastructure.EntityFramework.Contracts;
using FoodStorage.Infrastructure.EntityFramework.Contracts.Extensions;
using Microsoft.EntityFrameworkCore;

namespace FoodStorage.Infrastructure.Implementations;

internal class ProductHistoryRepository : IProductHistoryRepository
{
    private readonly DatabaseContext _databaseContext;

    public ProductHistoryRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
        _databaseContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public void Create(ProductHistory productHistory)
    {
        if (productHistory is null)
        {
            throw new EmptyArgumentValueException(nameof(productHistory));
        }

        ProductHistoryDto productHistoryDto = productHistory.ToDto();
        _databaseContext.ProductHistoryItems.Add(productHistoryDto);

        _databaseContext.SaveChanges();
    }

    public ProductHistory FindById(ProductHistoryId productHistoryId)
    {
        ProductHistoryDto productHistoryDto = _databaseContext.ProductHistoryItems.FirstOrDefault(ph => ph.Id == productHistoryId.ToGuid());
        return productHistoryDto is null ? null : productHistoryDto.ToEntity();
    }

    public IEnumerable<ProductHistory> GetByProductId(ProductId productId)
    {
        IEnumerable<ProductHistoryDto> productHistoryDtos = _databaseContext.ProductHistoryItems.Where(ph => ph.ProductId == productId.ToGuid());
        return productHistoryDtos.Select(ph => ph.ToEntity()).ToList();
    }

    public IEnumerable<ProductHistory> GetAll() =>_databaseContext.ProductHistoryItems.Select(ph => ph.ToEntity()).ToList();

    public void Delete(ProductHistory productHistory)
    {
        if (productHistory is null)
        {
            throw new EmptyArgumentValueException(nameof(productHistory));
        }

        ProductHistoryDto productHistoryDto = productHistory.ToDto();
        _databaseContext.ProductHistoryItems.Remove(productHistoryDto);

        _databaseContext.SaveChanges();
    }
}
