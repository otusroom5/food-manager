using FoodStorage.Application.Repositories;
using FoodStorage.Domain.Entities;
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

    public async Task CreateAsync(ProductHistory productHistory)
    {
        if (productHistory is null)
        {
            throw new EmptyArgumentValueException(nameof(productHistory));
        }

        ProductHistoryDto productHistoryDto = productHistory.ToDto();
        await _databaseContext.ProductHistoryItems.AddAsync(productHistoryDto);

        await _databaseContext.SaveChangesAsync();
    }

    public async Task<ProductHistory> FindByIdAsync(ProductHistoryId productHistoryId)
    {
        ProductHistoryDto productHistoryDto = await _databaseContext.ProductHistoryItems.FindAsync(productHistoryId.ToGuid());
        return productHistoryDto is null ? null : productHistoryDto.ToEntity();
    }

    public async Task<IEnumerable<ProductHistory>> GetByProductIdAsync(ProductId productId)
    {
        IEnumerable<ProductHistoryDto> productHistoryDtos = 
            await _databaseContext.ProductHistoryItems.Where(ph => ph.ProductId == productId.ToGuid()).ToListAsync();
        
        return productHistoryDtos.Select(ph => ph.ToEntity()).ToList();
    }

    public async Task<IEnumerable<ProductHistory>> GetByUserIdAsync(UserId userId)
    {
        IEnumerable<ProductHistoryDto> productHistoryDtos =
            await _databaseContext.ProductHistoryItems.Where(ph => ph.CreatedBy == userId.ToGuid()).ToListAsync();

        return productHistoryDtos.Select(ph => ph.ToEntity()).ToList();
    }

    public async Task<IEnumerable<ProductHistory>> GetByStateAsync(ProductActionType state)
    {
        IEnumerable<ProductHistoryDto> productHistoryDtos =
            await _databaseContext.ProductHistoryItems.Where(ph => ph.State == state.ToString()).ToListAsync();

        return productHistoryDtos.Select(ph => ph.ToEntity()).ToList();
    }

    public async Task<IEnumerable<ProductHistory>> GetAllAsync() => await _databaseContext.ProductHistoryItems.Select(ph => ph.ToEntity()).ToListAsync();

    public async Task DeleteAsync(ProductHistory productHistory)
    {
        if (productHistory is null)
        {
            throw new EmptyArgumentValueException(nameof(productHistory));
        }

        ProductHistoryDto productHistoryDto = productHistory.ToDto();
        _databaseContext.ProductHistoryItems.Remove(productHistoryDto);

        await _databaseContext.SaveChangesAsync();
    }
}
