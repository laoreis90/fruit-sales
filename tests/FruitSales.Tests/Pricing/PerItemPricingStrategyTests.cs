using FruitSales.Domain.Pricing;
using FruitSales.Domain.ValueObjects;

namespace FruitSales.Tests.Pricing;

public class PerItemPricingStrategyTests
{
    [Fact]
    public void CalculatePrice_WithValidQuantity_ReturnsCorrectPrice()
    {
        // Arrange
        Price pricePerItem = Price.Create(0.30m);
        IPricingStrategy strategy = new PerItemPricingStrategy(pricePerItem);
        const decimal quantity = 5;

        // Act
        Price calculatedPrice = strategy.CalculatePrice(quantity);

        // Assert
        Assert.Equal(Price.Create(1.50m), calculatedPrice);
    }

    [Fact]
    public void CalculatePrice_WithSingleItem_ReturnsItemPrice()
    {
        // Arrange
        Price pricePerItem = Price.Create(0.30m);
        IPricingStrategy strategy = new PerItemPricingStrategy(pricePerItem);

        // Act
        Price calculatedPrice = strategy.CalculatePrice(1);

        // Assert
        Assert.Equal(Price.Create(0.30m), calculatedPrice);
    }

    [Fact]
    public void CalculatePrice_WithZeroQuantity_ReturnsZeroPrice()
    {
        // Arrange
        Price pricePerItem = Price.Create(0.30m);
        IPricingStrategy strategy = new PerItemPricingStrategy(pricePerItem);

        // Act
        Price calculatedPrice = strategy.CalculatePrice(0);

        // Assert
        Assert.Equal(Price.Create(0m), calculatedPrice);
    }

    [Fact]
    public void CalculatePrice_WithNegativeQuantity_ThrowsArgumentException()
    {
        // Arrange
        Price pricePerItem = Price.Create(0.30m);
        IPricingStrategy strategy = new PerItemPricingStrategy(pricePerItem);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => strategy.CalculatePrice(-5));
    }

    [Fact]
    public void Constructor_WithNullPrice_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new PerItemPricingStrategy(null!));
    }
}