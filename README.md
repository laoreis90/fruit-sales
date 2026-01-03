# Fruit Sales Calculator

**Position:** Senior Software Engineer\
**Author:** Lao Reis\
**Technology:** C#\
**Requirements:** .NET 8  [Download](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

**How execute:**\
1 - Restore Packages: `dotnet restore`\
2 - Run: `dotnet run --project=src/FruitSalesApp`\
3 - Run tests: `dotnet test`

## Presentation Video

[![IMAGE ALT TEXT](https://img.youtube.com/vi/KZmZt2VwyMM/sddefault.jpg)](https://youtu.be/KZmZt2VwyMM "Preentation Video")

### Design Decisions

Strategy Pattern
- Rather than hardcoding pricing logic into each fruit class with a bunch of if-else statements, I isolated the pricing behavior into separate strategy classes. This way, if a new pricing model comes up later, I just create a new strategy without touching the existing code.

Dependecy Injection
- The system wires up all dependencies in one place (Program.cs). Fruits, strategies, and the order calculator don't create their own dependencies, they receive them. This keeps classes focused on their job and makes testing easier since you can inject test doubles.

Open/Closed Principle
- The system is open for extension (add new fruits or strategies) but closed for modification (existing classes stay the same). Adding a Cherry with a discount strategy doesn't require touching the Apple class. Adding a tiered pricing strategy doesn't require touching the strategy interface. Each new feature is added, not inserted into existing code.

### Design Patterns Used

**Strategy Pattern** - Pricing behavior is isolated in strategy classes. Each fruit uses one strategy, and different pricing models are different strategies.

**Dependency Injection** - Dependencies are provided to classes rather than created internally. This decouples classes and makes the system testable.

**Value Object** - Price is a small immutable object that prevents invalid states (negative prices) and provides consistent formatting and behavior.


### How to Add a New Fruit

Create a new class in `Domain/Fruits/` that inherits from Fruit:

```csharp
using FruitSales.Domain.Pricing;
using FruitSales.Domain.ValueObjects;

namespace FruitSales.Domain.Fruits;

public class Orange : Fruit
{
    public Orange(Price basePrice, IPricingStrategy pricingStrategy)
        : base("Orange", basePrice, pricingStrategy)
    {
    }
}
```

Then register it in `Program.cs`

```csharp
IPricingStrategy orangeStrategy = new PerKilogramPricingStrategy(Price.Create(1.50m));
Orange orange = new Orange(orangeStrategy);
services.AddSingleton(orange);    
```
The price is defined once in the fruit class. The strategy receives the same price for calculation. No duplication, and the fruit is the single source of truth for its price.


### How to Add a New Pricing Strategy

Create a class that implements `IPricingStrategy`. Instead of giving a fruit multiple strategies, create one strategy that handles complexity internally.

For example, tiered pricing where quantities below 5 cost \$1.00, but 5 or more ite cost \$0.70:

```csharp
public class TieredPricingStrategy : IPricingStrategy
{
    private readonly Price _regularPrice;
    private readonly Price _bulkPrice;
    private readonly decimal _bulkThreshold;

    public TieredPricingStrategy(Price regularPrice, Price bulkPrice, decimal bulkThreshold)
    {
        if (regularPrice is null)
        {
            throw new ArgumentNullException(nameof(regularPrice));
        }
        
        if(bulkPrice is null)
        {
            throw new ArgumentNullException(nameof(bulkPrice));
        }
        
        if (bulkThreshold <= 0)
        {
            throw new ArgumentException("Bulk threshold must be greater than zero.", nameof(bulkThreshold));
        }
        
        _regularPrice = regularPrice;
        _bulkPrice = bulkPrice;
        _bulkThreshold = bulkThreshold;
    }

    public Price CalculatePrice(decimal quantity)
    {
        if (quantity < 0)
        {
            throw new ArgumentException("Quantity cannot be negative.", nameof(quantity));
        }

        if (quantity >= _bulkThreshold)
        {
            return _bulkPrice * quantity;
        }

        return _regularPrice * quantity;
    }
}
```

Use it in `Program.cs`

```csharp
var appleStrategy = new TieredPricingStrategy(
    regularPrice: Price.Create(1.00m),
    bulkPrice: Price.Create(0.70m),
    bulkThreshold: 5m);  // 5 items or more gets bulk price

var apple = new Apple(appleStrategy);
services.AddSingleton<Apple>(apple);
```

The fruit just receives the strategy. It doesn't know or care how the strategy calculates the price, it just asks for it. This separation makes adding new pricing models simple and safe.

