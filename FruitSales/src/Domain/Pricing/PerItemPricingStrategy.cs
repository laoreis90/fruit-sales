using FruitSales.Domain.ValueObjects;

namespace FruitSales.Domain.Pricing;

public interface IPerItemPricingStrategy : IPricingStrategy
{
}

public class PerItemPricingStrategy : IPerItemPricingStrategy
{
    private readonly Price _pricePerItem;

    public PerItemPricingStrategy(Price pricePerItem)
    {
        if (pricePerItem is null)
        {
            throw new ArgumentNullException(nameof(pricePerItem));
        }

        _pricePerItem = pricePerItem;
    }


    public Price CalculatePrice(decimal quantity)
    {
        if (quantity < 0)
        {
            throw new ArgumentException("Quantity cannot be negative.", nameof(quantity));
        }
        
        return _pricePerItem * quantity;
    }
}