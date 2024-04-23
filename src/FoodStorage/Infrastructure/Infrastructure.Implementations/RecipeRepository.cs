using FoodStorage.Application.Repositories;
using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.RecipeEntity;
using FoodStorage.Infrastructure.EntityFramework;
using FoodStorage.Infrastructure.EntityFramework.Common.Exceptions;
using FoodStorage.Infrastructure.EntityFramework.Contracts;
using FoodStorage.Infrastructure.EntityFramework.Contracts.Extensions;

namespace FoodStorage.Infrastructure.Implementations;

internal class RecipeRepository : IRecipeRepository
{
    private readonly DatabaseContext _databaseContext;

    public RecipeRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
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

    public IEnumerable<Recipe> GetByProductId(ProductId productId)
    {
        IEnumerable<RecipeDto> recipeDtos = _databaseContext.Recipes.Where(r => r.Positions.Any(p => p.ProductId == productId.ToGuid()));
        return recipeDtos.Select(r => r.ToEntity()).ToList();
    }

    public IEnumerable<Recipe> GetAll()
    {
        return _databaseContext.Recipes.Select(r => r.ToEntity()).ToList();
    }

    public void Change(Recipe recipe)
    {
        if (recipe is null)
        {
            throw new EmptyArgumentValueException(nameof(recipe));
        }

        RecipeDto recipeDto = recipe.ToDto();
        _databaseContext.Recipes.Update(recipeDto);
    }

    public void Delete(Recipe recipe)
    {
        throw new NotImplementedException();
    }
}
