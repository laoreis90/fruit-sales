using FruitSales.Domain.Orders;
using FruitSales.Domain.ValueObjects;

namespace FruitSales.Application;

public interface IOrderCalculator
{
    Price CalculateOrderTotal(Order order);
}