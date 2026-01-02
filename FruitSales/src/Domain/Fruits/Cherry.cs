using FruitSales.Domain.Pricing;
using FruitSales.Domain.ValueObjects;

namespace FruitSales.Domain.Fruits;

public class Cherry : Fruit
{
    public Cherry(Price basePrice, IPricingStrategy pricingStrategy)
        : base("Cherry", pricingStrategy)
    {
    }
}