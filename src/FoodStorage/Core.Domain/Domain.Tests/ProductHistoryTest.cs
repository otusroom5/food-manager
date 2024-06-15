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
    [ExpectedException(typeof(InvalidArgumentValueException), "Invalid argument value 'ProductHistoryId': Empty Guid passed")]
    public void IncorrectProductHistoryIdTest()
    {
        ProductHistoryId.FromGuid(Guid.Empty);
    }

    [TestMethod]
    [DataRow(-1)]
    [DataRow(0)]
    [ExpectedException(typeof(InvalidArgumentValueException),
        "Invalid argument value 'Count': Product quantity must be a positive number")]
    public void EmptyProductHistoryCountTest(int count)
    {
        ProductHistory.CreateNew(ProductHistoryId.CreateNew(), ProductId.CreateNew(),
            ProductState.Added, count, UserId.FromGuid(Guid.NewGuid()), DateTime.UtcNow);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidArgumentValueException),
        "Invalid argument value 'CreatedAt': Created date cannot be from the future")]
    public void IncorrectProductHistoryCreatedAtTest()
    {
        ProductHistory.CreateNew(ProductHistoryId.CreateNew(), ProductId.CreateNew(),
            ProductState.Added, 4, UserId.FromGuid(Guid.NewGuid()), DateTime.Now.AddDays(10));
    }
}
