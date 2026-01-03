using FruitSales.Domain.Pricing;
using FruitSales.Domain.ValueObjects;

namespace FruitSales.Tests.Pricing;

public class DiscountWeightPricingStrategyTests
{
    [Fact]
    public void CalculatePrice_WithoutDiscount_ReturnsFullPrice()
    {
        // Arrange
        Price pricePerKilogram = Price.Create(5.00m);
        IPricingStrategy strategy = new DiscountWeightPricingStrategy(pricePerKilogram, discountThreshold: 2m, discountPercentage: 10m);
        const decimal quantity = 1.5m;

        // Act
        Price calculatedPrice = strategy.CalculatePrice(quantity);

        // Assert
        Assert.Equal(Price.Create(7.50m), calculatedPrice);
    }

    [Fact]
    public void CalculatePrice_WithDiscount_ReturnsPriceWithDiscount()
    {
        // Arrange
        Price pricePerKilogram = Price.Create(5.00m);
        IPricingStrategy strategy = new DiscountWeightPricingStrategy(pricePerKilogram, discountThreshold: 2m, discountPercentage: 10m);
        const decimal quantity = 3m;

        // Act
        Price calculatedPrice = strategy.CalculatePrice(quantity);
        
        // Assert
        // 3kg * $5 = $15, with 10% discount = $13.50
        Assert.Equal(Price.Create(13.50m), calculatedPrice);
    }

    [Fact]
    public void CalculatePrice_WithNegativeQuantity_ThrowsArgumentException()
    {
        // Arrange
        Price pricePerKilogram = Price.Create(5.00m);
        IPricingStrategy strategy = new DiscountWeightPricingStrategy(pricePerKilogram, discountThreshold: 2m, discountPercentage: 10m);
        const decimal quantity = -1m;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => strategy.CalculatePrice(quantity));
        
    }
    
    [Fact]
    public void Constructor_WithNullPrice_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => 
            new DiscountWeightPricingStrategy(null!, discountThreshold: 2m, discountPercentage: 10m));
    }
    
    [Fact]
    public void Constructor_WithNegativeThreshold_ThrowsArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => 
            new DiscountWeightPricingStrategy(Price.Create(5.00m), discountThreshold: -1m, discountPercentage: 10m));
    }

    [Fact]
    public void Constructor_WithNegativeDiscount_ThrowsArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => 
            new DiscountWeightPricingStrategy(Price.Create(5.00m), discountThreshold: 2m, discountPercentage: -10m));
    }

    [Fact]
    public void Constructor_WithDiscountOver100_ThrowsArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => 
            new DiscountWeightPricingStrategy(Price.Create(5.00m), discountThreshold: 2m, discountPercentage: 150m));
    }
}