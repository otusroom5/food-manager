using FoodStorage.Domain.Entities.Common.Exceptions;
using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.RecipeEntity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FoodStorage.Domain.Tests;

[TestClass]
public class RecipeTest
{
    #region Entity validation
    [TestMethod]
    public void CreateRecipeEntityWithoutPositionTest()
    {
        Recipe recipe = Recipe.CreateNew(RecipeId.CreateNew(), RecipeName.FromString("SomeName"));

        Assert.IsNotNull(recipe);
        Assert.IsInstanceOfType(recipe, typeof(Recipe));
        Assert.IsFalse(recipe.Positions.Any());
    }

    [TestMethod]
    public void CreateRecipeEntityWithPositionTest()
    {
        List<RecipePosition> positions = new() { RecipePosition.CreateNew(ProductId.CreateNew(), 4) };
        Recipe recipe = Recipe.CreateNew(RecipeId.CreateNew(), RecipeName.FromString("SomeName"), positions);

        Assert.IsNotNull(recipe);
        Assert.IsInstanceOfType(recipe, typeof(Recipe));
        Assert.IsTrue(recipe.Positions.Any());
        Assert.AreEqual(positions.Count, recipe.Positions.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidArgumentValueException), "Некорректное значение аргумента 'RecipeId': Передан пустой Guid")]
    public void IncorrectRecipeIdTest()
    {
        RecipeId.FromGuid(Guid.Empty);
    }


    [TestMethod]
    [DataRow("а")]
    [DataRow("многобуквмногобуквмногобуквмногобуквмногобуквмногобуквмногобуквмногобуквмногобуквмногобуквмногобуквмногобукв")]
    [ExpectedException(typeof(InvalidArgumentValueException), "Некорректное значение аргумента 'RecipeName': Передано некорректное значение")]
    public void IncorrectRecipeNameTest(string name)
    {
        RecipeName.FromString(name);
    }

    [TestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    [ExpectedException(typeof(InvalidArgumentValueException), "Некорректное значение аргумента 'RecipeName': Передано пустое значение")]
    public void EmptyRecipeNameTest(string name)
    {
        RecipeName.FromString(name);
    }

    [TestMethod]
    [DataRow(-1)]
    [DataRow(0)]
    [ExpectedException(typeof(InvalidArgumentValueException),
        "Некорректное значение аргумента 'ProductCount': Количество продукта в рецепте должно быть больше 0")]
    public void IncorrectRecipeProductCountTest(int count)
    {
        RecipePosition.CreateNew(ProductId.CreateNew(), count);
    }
    #endregion

    #region methods
    [TestMethod]
    public void RecipeAddPositionSuccessTest()
    {
        RecipePosition position =RecipePosition.CreateNew(ProductId.CreateNew(), 4);
        Recipe recipe = Recipe.CreateNew(RecipeId.CreateNew(), RecipeName.FromString("SomeName"));

        recipe.AddPosition(position);

        Assert.AreEqual(1, recipe.Positions.Count);
        Assert.AreEqual(position.ProductId, recipe.Positions.FirstOrDefault().ProductId);
    }

    [TestMethod]
    public void RecipeRemovePositionSuccessTest()
    {
        RecipePosition position = RecipePosition.CreateNew(ProductId.CreateNew(), 4);
        Recipe recipe = Recipe.CreateNew(RecipeId.CreateNew(), RecipeName.FromString("SomeName"));

        recipe.AddPosition(position);
        Assert.AreEqual(1, recipe.Positions.Count);

        recipe.RemovePosition(position.ProductId);
        Assert.AreEqual(0, recipe.Positions.Count);
    }

    [TestMethod]
    public void RecipeAddPositionThrowExceptionTest()
    {
        RecipePosition position = RecipePosition.CreateNew(ProductId.CreateNew(), 4);
        Recipe recipe = Recipe.CreateNew(RecipeId.CreateNew(), RecipeName.FromString("SomeName"), new List<RecipePosition>() { position });

        try
        {
            // попытка записать в рецепт позицию, ту же, что там уже есть
            recipe.AddPosition(position);
        }
        catch (DomainEntitiesException ex)
        {
            Assert.AreEqual($"Попытка добавить продукт '{position.ProductId}', который уже есть в рецепте", ex.Message);
        }
    }

    [TestMethod]
    public void RecipeRemovePositionThrowExceptionTest()
    {
        RecipePosition position = RecipePosition.CreateNew(ProductId.CreateNew(), 4);
        Recipe recipe = Recipe.CreateNew(RecipeId.CreateNew(), RecipeName.FromString("SomeName"), new List<RecipePosition>() { position });

        var newProductId = ProductId.CreateNew();
        try
        {
            // попытка убрать то, чего нет в рецепте
            recipe.RemovePosition(newProductId);
        }
        catch (DomainEntitiesException ex)
        {
            Assert.AreEqual($"Попытка убрать из списка продукт '{newProductId}', которого нет в рецепте", ex.Message);
        }
    }
    #endregion
}