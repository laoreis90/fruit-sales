using FruitSales.Domain.Orders;
using FruitSales.Domain.ValueObjects;

namespace FruitSales.Application;

public class OrderCalculator : IOrderCalculator
{
    public Price CalculateOrderTotal(Order order)
    {
        if (order is null)
        {
            throw new ArgumentNullException(nameof(order));
        }
        
        if (order.IsEmpty())
        {
            return Price.Create(0);
        }

        return order.GetLineItems()
            .Aggregate(Price.Create(0), (total, lineItem) => total + lineItem.Subtotal);
    }
}