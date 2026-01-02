using FruitSales.Domain.Fruits;
using FruitSales.Domain.Orders;
using FruitSales.Domain.Pricing;
using FruitSales.Domain.ValueObjects;

namespace FruitSales.Tests.Orders;

public class OrderTests
{
    private Fruit CreateApple()
    {
        IPricingStrategy strategy = new PerKilogramPricingStrategy(Price.Create(2.00m));
        return new Apple(strategy);
    }

    private Fruit CreateBanana()
    {
        IPricingStrategy strategy = new PerItemPricingStrategy(Price.Create(0.30m));
        return new Banana(strategy);
    }

    [Fact]
    public void AddLineItem_WithValidData_AddsToOrder()
    {
        Order order = new();
        Fruit fruit = CreateApple();

        order.AddLineItem(fruit, 1m);

        Assert.Single(order.GetLineItems());
    }

    [Fact]
    public void AddLineItem_WithNullFruit_ThrowsArgumentNullException()
    {
        Order order = new();
        Assert.Throws<ArgumentNullException>(() => order.AddLineItem(null!, 1m));
    }

    [Fact]
    public void AddLineItem_WithZeroQuantity_ThrowsArgumentException()
    {
        Order order = new();
        Fruit fruit = CreateApple();
        Assert.Throws<ArgumentException>(() => order.AddLineItem(fruit, 0));
    }

    [Fact]
    public void AddLineItem_SameFruitTwice_CombinesQuantity()
    {
        Order order = new();
        Fruit fruit = CreateApple();

        order.AddLineItem(fruit, 1m);
        order.AddLineItem(fruit, 1.5m);

        Assert.Single(order.GetLineItems());
        Assert.Equal(2.5m, order.GetLineItems()[0].Quantity);
    }

    [Fact]
    public void AddLineItem_DifferentFruits_AddsMultipleItems()
    {
        Order order = new();
        order.AddLineItem(CreateApple(), 1m);
        order.AddLineItem(CreateBanana(), 5);

        Assert.Equal(2, order.GetLineItems().Count);
    }

    [Fact]
    public void IsEmpty_WithNoItems_ReturnsTrue()
    {
        Order order = new();
        Assert.True(order.IsEmpty());
    }

    [Fact]
    public void IsEmpty_WithItems_ReturnsFalse()
    {
        Order order = new();
        order.AddLineItem(CreateApple(), 1m);
        Assert.False(order.IsEmpty());
    }
}