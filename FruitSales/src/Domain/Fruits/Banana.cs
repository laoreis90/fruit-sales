using FruitSales.Domain.Pricing;
using FruitSales.Domain.ValueObjects;

namespace FruitSales.Domain.Fruits;

public class Banana : Fruit
{
    public Banana(Price basePrice, IPricingStrategy pricingStrategy) 
        : base("Banana", pricingStrategy)
    {
    }
}