using FruitSales.Domain.Pricing;
using FruitSales.Domain.ValueObjects;

namespace FruitSales.Tests.Pricing;

public class PerKilogramPricingStrategyTests
{
    [Fact]
    public void CalculatePrice_WithValidQuantity_ReturnsPrice()
    {
        // Arrange
        Price pricePerKilogram = Price.Create(2.00m);
        IPricingStrategy strategy = new PerKilogramPricingStrategy(pricePerKilogram);
        const decimal quantity = 1.5m;
        
        // Act
        Price calculatedPrice = strategy.CalculatePrice(quantity);
        
        // Assert
        Assert.Equal(Price.Create(3.00m), calculatedPrice);
    }
    
    [Fact]
    public void CalculatePrice_WithZeroQuantity_ReturnsZeroPrice()
    {
        // Arrange
        Price pricePerKilogram = Price.Create(2.00m);
        IPricingStrategy strategy = new PerKilogramPricingStrategy(pricePerKilogram);

        // Act
        Price calculatedPrice = strategy.CalculatePrice(0);

        // Assert
        Assert.Equal(Price.Create(0m), calculatedPrice);
    }
    
    [Fact]
    public void CalculatePrice_WithNegativeQuantity_ThrowsArgumentException()
    {
        // Arrange
        Price pricePerKilogram = Price.Create(2.00m);
        IPricingStrategy strategy = new PerKilogramPricingStrategy(pricePerKilogram);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => strategy.CalculatePrice(-1m));
    }
    
    [Fact]
    public void Constructor_WithNullPrice_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new PerKilogramPricingStrategy(null!));
    }
}