using FruitSales.Domain.Pricing;

namespace FruitSales.Domain.Fruits;

public class Cherry(IPricingStrategy pricingStrategy) : Fruit("Cherry", pricingStrategy);