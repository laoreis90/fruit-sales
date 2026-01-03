using FruitSales.Application;
using FruitSales.Domain.Fruits;
using FruitSales.Domain.Pricing;
using FruitSales.Domain.ValueObjects;
using FruitSales.FruitSalesApp;
using Microsoft.Extensions.DependencyInjection;

// Create the service collection
ServiceCollection services = new();

// Register pricing strategies

// Apple Strategy
Price applePrice = Price.Create(2.00m);
IPricingStrategy appleStrategy = new PerKilogramPricingStrategy(applePrice);
services.AddSingleton(new Apple(appleStrategy));

// Banana Strategy
Price bananaPrice = Price.Create(0.30m);
IPricingStrategy bananaStrategy = new PerItemPricingStrategy(bananaPrice);
services.AddSingleton(new Banana(bananaStrategy));

// Cherry Strategy
Price cherryPrice = Price.Create(5.00m);
IPricingStrategy cherryStrategy = new DiscountWeightPricingStrategy(cherryPrice, discountThreshold: 2m, discountPercentage: 10m);
services.AddSingleton(new Cherry(cherryStrategy));


// Register application services
services.AddSingleton<IOrderCalculator, OrderCalculator>();
services.AddSingleton<ConsoleApp>();

// Build the service provider
ServiceProvider serviceProvider = services.BuildServiceProvider();

// Get the main application and run it
ConsoleApp app = serviceProvider.GetRequiredService<ConsoleApp>();
app.Run();