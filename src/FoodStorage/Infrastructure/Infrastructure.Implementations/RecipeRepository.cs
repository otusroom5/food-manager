using FoodStorage.Application.Repositories;
using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.RecipeEntity;
using FoodStorage.Infrastructure.EntityFramework;
using FoodStorage.Infrastructure.EntityFramework.Common.Exceptions;
using FoodStorage.Infrastructure.EntityFramework.Contracts;
using FoodStorage.Infrastructure.EntityFramework.Contracts.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FoodStorage.Infrastructure.Implementations;

internal class RecipeRepository : IRecipeRepository
{
    private readonly DatabaseContext _databaseContext;

    public RecipeRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
        _databaseContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public async Task CreateAsync(Recipe recipe)
    {
        if (recipe is null)
        {
            throw new EmptyArgumentValueException(nameof(recipe));
        }

        RecipeDto recipeDto = recipe.ToDto();
        _databaseContext.Recipes.Add(recipeDto);

        await _databaseContext.SaveChangesAsync();
    }

    public async Task<Recipe> FindByIdAsync(RecipeId recipeId)
    {
        RecipeDto recipeDto = await _databaseContext.Recipes.FindAsync(recipeId.ToGuid());
        return recipeDto is null ? null : recipeDto.ToEntity();
    }

    public async Task<Recipe> FindByNameAsync(RecipeName recipeName)
    {
        RecipeDto recipeDto = await _databaseContext.Recipes.FirstOrDefaultAsync(r => r.Name.ToLower() == recipeName.ToString().ToLower());
        return recipeDto is null ? null : recipeDto.ToEntity();
    }

    public async Task<IEnumerable<Recipe>> GetByProductIdAsync(ProductId productId)
    {
        IEnumerable<RecipeDto> recipeDtos = await _databaseContext.Recipes.Where(r => r.Positions.Any(p => p.ProductId == productId.ToGuid()))
                                                                          .ToListAsync();

        return recipeDtos.Select(r => r.ToEntity()).ToList();
    }

    public async Task<IEnumerable<Recipe>> GetAllAsync() => await _databaseContext.Recipes.Select(r => r.ToEntity()).ToListAsync();

    public async Task ChangeAsync(Recipe recipe)
    {
        if (recipe is null)
        {
            throw new EmptyArgumentValueException(nameof(recipe));
        }

        RecipeDto recipeDto = recipe.ToDto();

        // достаем сущность из базы
        var recipeFromBase = await _databaseContext.Recipes.Include(r => r.Positions).FirstOrDefaultAsync(r => r.Id == recipe.Id.ToGuid());
        // прикрепляем ее к трекеру
        _databaseContext.Recipes.Attach(recipeFromBase);
        // достаем ее из трекерв
        EntityEntry<RecipeDto> entry = _databaseContext.ChangeTracker.Entries<RecipeDto>().First(e => e.Entity.Id == recipeDto.Id);
        // устанавливаем ей новые значения
        entry.CurrentValues.SetValues(recipeDto);
        entry.Entity.Positions = recipeDto.Positions;

        await _databaseContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Recipe recipe)
    {
        if (recipe is null)
        {
            throw new EmptyArgumentValueException(nameof(recipe));
        }

        RecipeDto recipeDto = recipe.ToDto();
        _databaseContext.Recipes.Remove(recipeDto);

        await _databaseContext.SaveChangesAsync();
    }
}
