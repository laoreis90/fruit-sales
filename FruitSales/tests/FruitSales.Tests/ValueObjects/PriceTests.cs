using FruitSales.Domain.ValueObjects;

namespace FruitSales.Tests.ValueObjects;

public class PriceTests
{
    [Fact]
    public void Create_WithValidAmount_ReturnsPrice()
    {
        // Arrange
        const decimal amount = 2.50m;

        // Act
        Price price = Price.Create(amount);

        // Assert
        Assert.NotNull(price);
        Assert.Equal(amount, price.Amount);
    }

    [Fact]
    public void Create_WithZero_ReturnsPrice()
    {
        // Arrange
        const decimal amount = 0m;

        // Act
        Price price = Price.Create(amount);

        // Assert
        Assert.Equal(0m, price.Amount);
    }

    [Fact]
    public void Create_WithNegativeAmount_ThrowsArgumentException()
    {
        // Arrange
        const decimal amount = -1.50m;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => Price.Create(amount));
    }

    [Fact]
    public void Add_TwoItems_ReturnsSummedPrice()
    {
        // Arrange
        Price price1 = Price.Create(1.50m);
        Price price2 = Price.Create(2.50m);

        // Act
        Price sum = price1 + price2;

        // Assert
        Assert.Equal(4m, sum.Amount);
    }

    [Fact]
    public void Multiply_TwoItems_ReturnMultipliedPrice()
    {
        // Arrange
        Price price = Price.Create(1.50m);
        const decimal multiplier = 3;

        // Act
        Price multiplied = price * multiplier;

        // Assert
        Assert.Equal(4.50m, multiplied.Amount);
    }

    [Fact]
    public void Multiply_NegativeMultiplier_ThrowsArgumentException()
    {
        // Arrange
        Price price = Price.Create(2.00m);
        const decimal multiplier = -1m;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => price * multiplier);
    }
}