using FruitSales.Domain.Fruits;
using FruitSales.Domain.Pricing;
using FruitSales.Domain.ValueObjects;

namespace FruitSales.Tests.Fruits;

public class BananaTests
{
    [Fact]
    public void Banana_HasCorrectName()
    {
        // Arrange
        Price bananaPrice = Price.Create(2.00m);
        IPricingStrategy strategy = new PerKilogramPricingStrategy(bananaPrice);

        // Act
        Banana banana = new Banana(bananaPrice, strategy);

        // Assert
        Assert.Equal("Banana", banana.Name);
    }

    [Fact]
    public void Banana_CalculatesPrice_WithProvidedStrategy()
    {
        // Arrange
        Price bananaPrice = Price.Create(2.00m);
        IPricingStrategy strategy = new PerKilogramPricingStrategy(bananaPrice);
        Banana banana = new Banana(bananaPrice, strategy);
        // Act
        Price calculatedPrice = banana.CalculatePrice(2m);

        // Assert
        Assert.Equal(Price.Create(4.00m), calculatedPrice);
    }
}