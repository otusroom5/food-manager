﻿using FoodStorage.Domain.Entities;
using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.ProductHistoryEntity;

namespace FoodStorage.Application.Repositories;

public interface IProductHistoryRepository
{
    public Task CreateAsync(ProductHistory productHistory);
    public Task<ProductHistory> FindByIdAsync(ProductHistoryId productHistoryId);
    public Task<IEnumerable<ProductHistory>> GetByProductIdAsync(ProductId productId);
    public Task<IEnumerable<ProductHistory>> GetByUserIdAsync(UserId userId);
    public Task<IEnumerable<ProductHistory>> GetByStateAsync(ProductState state);
    public Task<IEnumerable<ProductHistory>> GetAllAsync();
    public Task DeleteAsync(ProductHistory productHistory);
}