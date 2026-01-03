using FruitSales.Domain.ValueObjects;

namespace FruitSales.Domain.Pricing;

public interface IDiscountWeightPricingStrategy : IPricingStrategy
{
}

public class DiscountWeightPricingStrategy : IDiscountWeightPricingStrategy
{
    private readonly Price _pricePerKilogram;
    private readonly decimal _discountThreshold;
    private readonly decimal _discountPercentage;

    public DiscountWeightPricingStrategy(Price pricePerKilogram, decimal discountThreshold, decimal discountPercentage)
    {
        if (pricePerKilogram is null)
        {
            throw new ArgumentNullException(nameof(pricePerKilogram));
        }

        if (discountThreshold <= 0)
        {
            throw new ArgumentException("Discount threshold must be greater than zero.", nameof(discountThreshold));
        }

        if (discountPercentage is < 0 or > 100)
        {
            throw new ArgumentException("Discount percentage must be between 0 and 100.", nameof(discountPercentage));
        }

        _pricePerKilogram = pricePerKilogram;
        _discountThreshold = discountThreshold;
        _discountPercentage = discountPercentage;
    }

    public Price CalculatePrice(decimal quantity)
    {
        if (quantity < 0)
        {
            throw new ArgumentException("Quantity cannot be negative.", nameof(quantity));
        }

        Price basePrice = _pricePerKilogram * quantity;

        // Quantity is greater than discount weight, so apply discount
        if (quantity > _discountThreshold)
        {
            // Discount in percentage
            return basePrice * (1 - _discountPercentage / 100);
        }

        return basePrice;
    }
}