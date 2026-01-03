using FruitSales.Domain.Pricing;
using FruitSales.Domain.ValueObjects;

namespace FruitSales.Domain.Fruits;

public abstract class Fruit
{
    public string Name { get; }
    private IPricingStrategy PricingStrategy { get; }

    protected Fruit(string name, IPricingStrategy pricingStrategy)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be null or whitespace.", nameof(name));
        }

        if (pricingStrategy is null)
        {
            throw new ArgumentNullException(nameof(pricingStrategy));
        }
        
        Name = name;
        PricingStrategy = pricingStrategy;
    }

    public Price CalculatePrice(decimal quantity)
    {
        return PricingStrategy.CalculatePrice(quantity);
    }
}