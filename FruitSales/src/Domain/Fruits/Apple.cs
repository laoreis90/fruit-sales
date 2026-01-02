using FruitSales.Domain.Pricing;
using FruitSales.Domain.ValueObjects;

namespace FruitSales.Domain.Fruits;

public class Apple : Fruit
{
    public Apple(Price basePrice, IPricingStrategy pricingStrategy)
        : base("Apple", basePrice, pricingStrategy)
    {
    }
}