using FruitSales.Domain.Fruits;
using FruitSales.Domain.Pricing;
using FruitSales.Domain.ValueObjects;

namespace FruitSales.Tests.Fruits;

public class AppleTests
{
    [Fact]
    public void Apple_HasCorrectName()
    {
        // Arrange
        Price applePrice = Price.Create(2.00m);
        IPricingStrategy strategy = new PerKilogramPricingStrategy(applePrice);

        // Act
        Apple apple = new Apple(strategy);

        // Assert
        Assert.Equal("Apple", apple.Name);
    }

    [Fact]
    public void Apple_CalculatesPrice_WithProvidedStrategy()
    {
        // Arrange
        Price applePrice = Price.Create(2.00m);
        IPricingStrategy strategy = new PerKilogramPricingStrategy(applePrice);
        Apple apple = new Apple(strategy);

        // Act
        Price calculatedPrice = apple.CalculatePrice(2m);

        // Assert
        Assert.Equal(Price.Create(4.00m), calculatedPrice);
    }
}