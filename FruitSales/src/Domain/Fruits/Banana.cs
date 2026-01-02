using FruitSales.Domain.Pricing;

namespace FruitSales.Domain.Fruits;

public class Banana(IPricingStrategy pricingStrategy) : Fruit("Banana", pricingStrategy);