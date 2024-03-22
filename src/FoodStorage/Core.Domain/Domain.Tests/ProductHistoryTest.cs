using FoodStorage.Domain.Entities;
using FoodStorage.Domain.Entities.Common.Exceptions;
using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.ProductHistoryEntity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FoodStorage.Domain.Tests;

[TestClass]
public class ProductHistoryTest
{
    [TestMethod]
    public void CreateProductHistoryEntityTest()
    {
        var productHistory = ProductHistory.CreateNew(ProductHistoryId.CreateNew(), ProductId.CreateNew(), 
            ProductState.Added, 4, UserId.FromGuid(Guid.NewGuid()), DateTime.UtcNow);

        Assert.IsNotNull(productHistory);
        Assert.IsInstanceOfType(productHistory, typeof(ProductHistory));
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidArgumentValueException), "Некорректное значение аргумента 'ProductHistoryId': Передан пустой Guid")]
    public void IncorrectProductHistoryIdTest()
    {
        ProductHistoryId.FromGuid(Guid.Empty);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidArgumentValueException), "Некорректное значение аргумента 'UserId': Передан пустой Guid")]
    public void IncorrectUserIdTest()
    {
        UserId.FromGuid(Guid.Empty);
    }

    [TestMethod]
    [DataRow(-1)]
    [DataRow(0)]
    [ExpectedException(typeof(InvalidArgumentValueException),
        "Некорректное значение аргумента 'Count': Количество продукта должно быть положительным числом")]
    public void EmptyProductHistoryCountTest(int count)
    {
        ProductHistory.CreateNew(ProductHistoryId.CreateNew(), ProductId.CreateNew(),
            ProductState.Added, count, UserId.FromGuid(Guid.NewGuid()), DateTime.UtcNow);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidArgumentValueException),
        "Некорректное значение аргумента 'CreatedAt': Дата операции не может быть из будущего")]
    public void IncorrectProductHistoryCreatedAtTest()
    {
        ProductHistory.CreateNew(ProductHistoryId.CreateNew(), ProductId.CreateNew(),
            ProductState.Added, 4, UserId.FromGuid(Guid.NewGuid()), DateTime.Now.AddDays(10));
    }
}
