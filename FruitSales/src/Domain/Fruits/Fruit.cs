using FruitSales.Domain.Pricing;
using FruitSales.Domain.ValueObjects;

namespace FruitSales.Domain.Fruits;

public abstract class Fruit
{
    public string Name { get; }
    public Price BasePrice { get; }
    protected IPricingStrategy PricingStrategy { get; }

    protected Fruit(string name, Price basePrice, IPricingStrategy pricingStrategy)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be null or whitespace.", nameof(name));
        }

        if (basePrice is null)
        {
            throw new ArgumentNullException(nameof(basePrice));
        }

        if (pricingStrategy is null)
        {
            throw new ArgumentNullException(nameof(pricingStrategy));
        }
        
        Name = name;
        BasePrice = basePrice;
        PricingStrategy = pricingStrategy;
    }

    public Price CalculatePrice(decimal quantity)
    {
        return PricingStrategy.CalculatePrice(quantity);
    }
}