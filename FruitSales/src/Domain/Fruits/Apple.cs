using FruitSales.Domain.Pricing;

namespace FruitSales.Domain.Fruits;

public class Apple(IPricingStrategy pricingStrategy) : Fruit("Apple", pricingStrategy);