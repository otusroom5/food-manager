using FoodStorage.Domain.Entities.Common.Exceptions;
using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.ProductItemEntity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FoodStorage.Domain.Tests;

[TestClass]
public class PoductItemTest
{
    #region Entity validation

    [TestMethod]
    public void CreateProductItemEntityTest()
    {
        ProductItem productItem = ProductItem.CreateNew(ProductItemId.CreateNew(), ProductId.CreateNew(), 4, DateTime.UtcNow, DateTime.UtcNow.AddDays(3));

        Assert.IsNotNull(productItem);
        Assert.IsInstanceOfType(productItem, typeof(ProductItem));
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidArgumentValueException), "Invalid argument value 'ProductItemId': Empty Guid passed")]
    public void IncorrectProductItemIdTest()
    {
        ProductItemId.FromGuid(Guid.Empty);
    }

    [TestMethod]
    [DataRow(-1)]
    [DataRow(0)]
    [ExpectedException(typeof(InvalidArgumentValueException),
        "Invalid argument value 'Amount': The number of product items must be a positive number")]
    public void EmptyProductItemAmountTest(int amount)
    {
        ProductItem.CreateNew(ProductItemId.CreateNew(), ProductId.CreateNew(), amount, DateTime.UtcNow, DateTime.UtcNow);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidArgumentValueException),
        "Invalid argument value 'CreatingDate': Creating date cannot be from the future")]
    public void IncorrectProductItemCreatingDateTest()
    {
        ProductItem.CreateNew(ProductItemId.CreateNew(), ProductId.CreateNew(), 4, DateTime.UtcNow.AddDays(10), DateTime.UtcNow);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidArgumentValueException),
        "Invalid argument value 'ExpiryDate': The expiration date cannot be earlier than the production date")]
    public void IncorrectProductIteExpiryDateTest()
    {
        ProductItem.CreateNew(ProductItemId.CreateNew(), ProductId.CreateNew(), 4, DateTime.MaxValue, DateTime.MinValue);
    }
    #endregion

    #region methods
    [TestMethod]
    public void ProductItemReduceAmountSuccessTest()
    {
        int amountInItem = 15;
        int amountToReduce = 11;

        var productItem = ProductItem.CreateNew(ProductItemId.CreateNew(), ProductId.CreateNew(), amountInItem, DateTime.UtcNow, DateTime.UtcNow.AddDays(3));

        productItem.ReduceAmount(amountToReduce, null);

        Assert.AreEqual(amountInItem - amountToReduce, productItem.Amount);
    }

    [TestMethod]
    [ExpectedException(typeof(DomainEntitiesException), "You can't take more product than you have")]
    public void ProductItemReduceAmountThrowExceptionTest()
    {
        int amountInItem = 11;
        int amountToReduce = 15;

        var productItem = ProductItem.CreateNew(ProductItemId.CreateNew(), ProductId.CreateNew(), amountInItem, DateTime.UtcNow, DateTime.UtcNow.AddDays(3));

        productItem.ReduceAmount(amountToReduce, null);
    }
    #endregion
}