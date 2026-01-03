using FruitSales.Domain.Fruits;
using FruitSales.Domain.Orders;
using FruitSales.Domain.Pricing;
using FruitSales.Domain.ValueObjects;

namespace FruitSales.Tests.Orders;

public class OrderLineItemTests
{
    private Fruit CreateTestFruit(Price? price = null)
    {
        Price testPrice = price ?? Price.Create(1.00m);
        IPricingStrategy strategy = new PerKilogramPricingStrategy(testPrice);
        return new Apple(strategy);
    }

    [Fact]
    public void Constructor_WithValidData_CreatesLineItem()
    {
        Fruit fruit = CreateTestFruit();
        OrderLineItem lineItem = new OrderLineItem(fruit, 1.5m);

        Assert.Equal(fruit, lineItem.Fruit);
        Assert.Equal(1.5m, lineItem.Quantity);
    }

    [Fact]
    public void Constructor_WithNullFruit_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new OrderLineItem(null!, 1m));
    }

    [Fact]
    public void Constructor_WithZeroQuantity_ThrowsArgumentException()
    {
        Fruit fruit = CreateTestFruit();
        Assert.Throws<ArgumentException>(() => new OrderLineItem(fruit, 0));
    }

    [Fact]
    public void CalculateSubtotal_ReturnsCorrectPrice()
    {
        Fruit fruit = CreateTestFruit(Price.Create(2.00m));
        OrderLineItem lineItem = new OrderLineItem(fruit, 1.5m);

        Assert.Equal(Price.Create(3.00m), lineItem.Subtotal);
    }

    [Fact]
    public void UpdateQuantity_ChangesQuantityAndSubtotal()
    {
        Fruit fruit = CreateTestFruit(Price.Create(2.00m));
        OrderLineItem lineItem = new OrderLineItem(fruit, 1m);

        lineItem.UpdateQuantity(2.5m);

        Assert.Equal(2.5m, lineItem.Quantity);
        Assert.Equal(Price.Create(5.00m), lineItem.Subtotal);
    }
}