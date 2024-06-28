using FoodStorage.Domain.Entities.Common.Exceptions;
using FoodStorage.Domain.Entities.ProductEntity;
using FoodStorage.Domain.Entities.UnitEntity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FoodStorage.Domain.Tests;

[TestClass]
public class ProductTest
{
    [TestMethod]
    public void CreateProductEntityTest()
    {
        Product product = Product.CreateNew(ProductId.CreateNew(), ProductName.FromString("SomeName"), UnitType.Mass, 45.6, 4);

        Assert.IsNotNull(product);
        Assert.IsInstanceOfType(product, typeof(Product));
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidArgumentValueException), "Invalid argument value 'ProductId': Empty Guid passed")]
    public void IncorrectProductIdTest()
    {
        ProductId.FromGuid(Guid.Empty);
    }

    [TestMethod]
    [DataRow("a")]
    [DataRow("многобуквмногобуквмногобуквмногобуквмногобуквмногобуквмногобуквмногобуквмногобуквмногобуквмногобуквмногобукв")]
    [ExpectedException(typeof(InvalidArgumentValueException), "Invalid argument value 'ProductName': Incorrect value passed")]
    public void IncorrectProductNameTest(string name)
    {
        ProductName.FromString(name);
    }

    [TestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    [ExpectedException(typeof(InvalidArgumentValueException), "Invalid argument value 'ProductName': Empty Guid passed")]
    public void EmptyProductNameTest(string name)
    {
        ProductName.FromString(name);
    }

    [TestMethod]
    [DataRow(-1.3)]
    [DataRow(0)]
    [ExpectedException(typeof(InvalidArgumentValueException),
        "Invalid argument value 'MinAmountPerDay': The minimum balance must be a positive number")]
    public void IncorrectProductMinAmountPerDayTest(double minAmountPerDay)
    {
        Product.CreateNew(ProductId.CreateNew(), ProductName.FromString("SomeName"), UnitType.Mass, minAmountPerDay, 4);
    }

    [TestMethod]
    [DataRow(-1)]
    [DataRow(0)]
    [ExpectedException(typeof(InvalidArgumentValueException),
        "Invalid argument value 'BestBeforeDate': Expiration date must be a positive number")]
    public void IncorrectProductBestBeforeDateTest(int bestBeforeDate)
    {
        Product.CreateNew(ProductId.CreateNew(), ProductName.FromString("SomeName"), UnitType.Mass, 4, bestBeforeDate);
    }
}