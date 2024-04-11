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
        ProductItem productItem = ProductItem.CreateNew(ProductItemId.CreateNew(), ProductId.CreateNew(), 4, DateTime.UtcNow, DateTime.UtcNow);

        Assert.IsNotNull(productItem);
        Assert.IsInstanceOfType(productItem, typeof(ProductItem));
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidArgumentValueException), "Некорректное значение аргумента 'ProductItemId': Передан пустой Guid")]
    public void IncorrectProductItemIdTest()
    {
        ProductItemId.FromGuid(Guid.Empty);
    }

    [TestMethod]
    [DataRow(-1)]
    [DataRow(0)]
    [ExpectedException(typeof(InvalidArgumentValueException),
        "Некорректное значение аргумента 'Amount': Количество единиц продукта должно быть положительным числом")]
    public void EmptyProductItemAmountTest(int amount)
    {
        ProductItem.CreateNew(ProductItemId.CreateNew(), ProductId.CreateNew(), amount, DateTime.UtcNow, DateTime.UtcNow);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidArgumentValueException),
        "Некорректное значение аргумента 'CreatingDate': Дата изготовления не может быть из будущего")]
    public void IncorrectProductItemCreatingDateTest()
    {
        ProductItem.CreateNew(ProductItemId.CreateNew(), ProductId.CreateNew(), 4, DateTime.UtcNow.AddDays(10), DateTime.UtcNow);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidArgumentValueException),
        "Некорректное значение аргумента 'ExpiryDate': Дата окончания срока годности не может быть раньше чем дата изготовления")]
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

        var productItem = ProductItem.CreateNew(ProductItemId.CreateNew(), ProductId.CreateNew(), amountInItem, DateTime.UtcNow, DateTime.UtcNow);

        productItem.ReduceAmount(amountToReduce);

        Assert.AreEqual(amountInItem - amountToReduce, productItem.Amount);
    }

    [TestMethod]
    [ExpectedException(typeof(DomainEntitiesException), "Нельзя забрать продукта больше, чем есть")]
    public void ProductItemReduceAmountThrowExceptionTest()
    {
        int amountInItem = 11;
        int amountToReduce = 15;

        var productItem = ProductItem.CreateNew(ProductItemId.CreateNew(), ProductId.CreateNew(), amountInItem, DateTime.UtcNow, DateTime.UtcNow);

        productItem.ReduceAmount(amountToReduce);
    }
    #endregion
}