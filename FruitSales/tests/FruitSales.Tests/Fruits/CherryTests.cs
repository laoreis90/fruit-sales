using FruitSales.Domain.Fruits;
using FruitSales.Domain.Pricing;
using FruitSales.Domain.ValueObjects;

namespace FruitSales.Tests.Fruits;

public class CherryTests
{
    [Fact]
    public void Cherry_HasCorrectName()
    {
        // Arrange
        Price cherryPrice = Price.Create(2.00m);
        IPricingStrategy strategy = new PerKilogramPricingStrategy(cherryPrice);

        // Act
        Cherry cherry = new Cherry(strategy);

        // Assert
        Assert.Equal("Cherry", cherry.Name);
    }

    [Fact]
    public void Cherry_CalculatesPrice_WithProvidedStrategy()
    {
        // Arrange
        Price cherryPrice = Price.Create(2.00m);
        IPricingStrategy strategy = new PerKilogramPricingStrategy(cherryPrice);
        Cherry cherry = new Cherry(strategy);

        // Act
        Price calculatedPrice = cherry.CalculatePrice(2m);

        // Assert
        Assert.Equal(Price.Create(4.00m), calculatedPrice);
    }
}