using FruitSales.Domain.ValueObjects;

namespace FruitSales.Domain.Pricing;

public interface IPricingStrategy
{
    Price CalculatePrice(decimal quantity);
}