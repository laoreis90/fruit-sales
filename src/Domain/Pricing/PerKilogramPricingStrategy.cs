using FruitSales.Domain.ValueObjects;

namespace FruitSales.Domain.Pricing;

public interface IPerKilogramPricingStrategy : IPricingStrategy
{
}

public class PerKilogramPricingStrategy : IPerKilogramPricingStrategy
{
    private readonly Price _pricePerKilogram;

    public PerKilogramPricingStrategy(Price pricePerKilogram)
    {
        if (pricePerKilogram is null)
        {
            throw new ArgumentNullException(nameof(pricePerKilogram));
        }
        
        _pricePerKilogram = pricePerKilogram;
    }
    
    public Price CalculatePrice(decimal quantity)
    {
        if (quantity < 0)
        {
            throw new ArgumentException("Quantity cannot be negative.", nameof(quantity));
        }
        
        return _pricePerKilogram * quantity;
    }
}