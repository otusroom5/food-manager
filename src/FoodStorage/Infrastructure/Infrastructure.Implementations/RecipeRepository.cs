using FoodStorage.Application.Repositories;
using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.RecipeEntity;
using FoodStorage.Infrastructure.EntityFramework;
using FoodStorage.Infrastructure.EntityFramework.Common.Exceptions;
using FoodStorage.Infrastructure.EntityFramework.Contracts;
using FoodStorage.Infrastructure.EntityFramework.Contracts.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using UnitsNet;

namespace FoodStorage.Infrastructure.Implementations;

internal class RecipeRepository : IRecipeRepository
{
    private readonly DatabaseContext _databaseContext;

    public RecipeRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
        _databaseContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public void Create(Recipe recipe)
    {
        if (recipe is null)
        {
            throw new EmptyArgumentValueException(nameof(recipe));
        }

        RecipeDto recipeDto = recipe.ToDto();
        _databaseContext.Recipes.Add(recipeDto);

        _databaseContext.SaveChanges();
    }

    public Recipe FindById(RecipeId recipeId)
    {
        RecipeDto recipeDto = _databaseContext.Recipes.FirstOrDefault(r => r.Id == recipeId.ToGuid());
        return recipeDto is null ? null : recipeDto.ToEntity();
    }

    public Recipe FindByName(RecipeName recipeName)
    {
        RecipeDto recipeDto = _databaseContext.Recipes.FirstOrDefault(r => r.Name.ToLower() == recipeName.ToString().ToLower());
        return recipeDto is null ? null : recipeDto.ToEntity();
    }

    public IEnumerable<Recipe> GetByProductId(ProductId productId)
    {
        IEnumerable<RecipeDto> recipeDtos = _databaseContext.Recipes.Where(r => r.Positions.Any(p => p.ProductId == productId.ToGuid()));
        return recipeDtos.Select(r => r.ToEntity()).ToList();
    }

    public IEnumerable<Recipe> GetAll() => _databaseContext.Recipes.Select(r => r.ToEntity()).ToList();

    public void Change(Recipe recipe)
    {
        if (recipe is null)
        {
            throw new EmptyArgumentValueException(nameof(recipe));
        }

        RecipeDto recipeDto = recipe.ToDto();

        // достаем сущность из базы
        var recipeFromBase = _databaseContext.Recipes.Include(r => r.Positions).FirstOrDefault(r => r.Id == recipe.Id.ToGuid());
        // прикрепляем ее к трекеру
        _databaseContext.Recipes.Attach(recipeFromBase);
        // достаем ее из трекерв
        EntityEntry<RecipeDto> entry = _databaseContext.ChangeTracker.Entries<RecipeDto>().First(e => e.Entity.Id == recipeDto.Id);
        // устанавливаем ей новые значения
        entry.CurrentValues.SetValues(recipeDto);
        entry.Entity.Positions = recipeDto.Positions;

        _databaseContext.SaveChanges();
    }

    public void Delete(Recipe recipe)
    {
        if (recipe is null)
        {
            throw new EmptyArgumentValueException(nameof(recipe));
        }

        RecipeDto recipeDto = recipe.ToDto();
        _databaseContext.Recipes.Remove(recipeDto);

        _databaseContext.SaveChanges();
    }
}
