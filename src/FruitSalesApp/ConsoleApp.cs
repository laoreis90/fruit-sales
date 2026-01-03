using FruitSales.Application;
using FruitSales.Domain.Fruits;
using FruitSales.Domain.Orders;
using FruitSales.Domain.ValueObjects;

namespace FruitSales.FruitSalesApp;

public class ConsoleApp
{
    private readonly IOrderCalculator _orderCalculator;
    private readonly Apple _apple;
    private readonly Banana _banana;
    private readonly Cherry _cherry;

    public ConsoleApp(
        IOrderCalculator orderCalculator,
        Apple apple,
        Banana banana,
        Cherry cherry)
    {
        _orderCalculator = orderCalculator;
        _apple = apple;
        _banana = banana;
        _cherry = cherry;
    }

    public void Run()
    {
        Console.WriteLine("Welcome to Fruit Sales Calculator \n");

        Order order = new();
        bool addingItems = true;

        while (addingItems)
        {
            DisplayAvailableFruits();
            Console.Write("Select fruit to add to order (1-3) or 4 to checkout: ");

            string fruitChoice = Console.ReadLine() ?? string.Empty;

            switch (fruitChoice)
            {
                case "1":
                    AddFruitToOrder(order, _apple, "kg"); // Based on the @Program.cs strategy
                    break;
                case "2":
                    AddFruitToOrder(order, _banana, "item(s)"); // Based on the @Program.cs strategy
                    break;
                case "3":
                    AddFruitToOrder(order, _cherry, "kg"); // Based on the @Program.cs strategy
                    break;
                case "4":
                    addingItems = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.\n");
                    break;
            }
        }

        if (order.GetLineItems().Any())
        {
            DisplayOrderSummary(order);
        }
        else
        {
            Console.WriteLine("No items added to order.\n");
        }
    }

    private void DisplayAvailableFruits()
    {
        Console.WriteLine("\n Available Fruits");
        Console.WriteLine("1. Apple - $2.00 per kg"); // Based on the @Program.cs strategy
        Console.WriteLine("2. Banana - $0.30 per item"); // Based on the @Program.cs strategy
        Console.WriteLine("3. Cherry - $5.00 per kg (10% discount for > 2kg)"); // Based on the @Program.cs strategy
        Console.WriteLine("4. Checkout");
    }

    private void AddFruitToOrder(Order order, Fruit fruit, string unit)
    {
        Console.Write($"Enter quantity in {unit}: ");

        if (decimal.TryParse(Console.ReadLine(), out decimal quantity))
        {
            if (quantity > 0)
            {
                order.AddLineItem(fruit, quantity);
                Console.WriteLine($"✓ Added {quantity} {unit} of {fruit.Name} to order.\n");
            }
            else
            {
                Console.WriteLine("Quantity must be greater than 0.\n");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid number.\n");
        }
    }

    private void DisplayOrderSummary(Order order)
    {
        Console.WriteLine("\nORDER SUMMARY");
        Console.WriteLine("Items:");
        Console.WriteLine("---");

        foreach (OrderLineItem lineItem in order.GetLineItems())
        {
            Console.WriteLine($"  {lineItem.Fruit.Name}: {lineItem.Quantity}  = {lineItem.Subtotal}");
        }

        Console.WriteLine("---");
        Price total = _orderCalculator.CalculateOrderTotal(order);
        Console.WriteLine($"Total: {total}");
    }
}