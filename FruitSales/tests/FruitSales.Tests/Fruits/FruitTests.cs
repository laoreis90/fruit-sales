using FruitSales.Domain.Fruits;
using FruitSales.Domain.Pricing;
using FruitSales.Domain.ValueObjects;

namespace FruitSales.Tests.Fruits;

public class FruitTests
{
    // Concrete test implementation of Fruit for testing purposes
    private class TestFruit : Fruit
    {
        public TestFruit(string name, IPricingStrategy pricingStrategy)
            : base(name, pricingStrategy)
        {
        }
    }
    
    [Fact]
    public void Constructor_WithValidParameters_CreatesFruit()
    {
        // Arrange
        Price price = Price.Create(2.00m);
        IPricingStrategy strategy = new PerKilogramPricingStrategy(price);
        
        // Act
        TestFruit fruit = new TestFruit("Apple", strategy);

        // Assert
        Assert.Equal("Apple", fruit.Name);
    }
    
    [Fact]
    public void Constructor_WithNullName_ThrowsArgumentException()
    {
        // Arrange
        IPricingStrategy strategy = new PerKilogramPricingStrategy(Price.Create(2.00m));
        Price price = Price.Create(2.00m);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new TestFruit(null!, strategy));
    }

    [Fact]
    public void Constructor_WithNullStrategy_ThrowsArgumentNullException()
    {
        // Arrange
        Price price = Price.Create(2.00m);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new TestFruit("Apple", null!));
    }

    [Fact]
    public void CalculatePrice_DelegatesStrategyCorrectly()
    {
        // Arrange
        IPricingStrategy strategy = new PerKilogramPricingStrategy(Price.Create(2.00m));
        TestFruit fruit = new TestFruit("Apple", strategy);

        // Act
        Price calculatedPrice = fruit.CalculatePrice(1.5m);

        // Assert
        Assert.Equal(Price.Create(3.00m), calculatedPrice);
    }
}