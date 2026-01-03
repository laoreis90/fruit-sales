using FruitSales.Domain.ValueObjects;

namespace FruitSales.Domain.Orders;

public class OrderLineItem
{
    public Fruits.Fruit Fruit { get; }
    public decimal Quantity { get; private set; }
    public Price Subtotal { get; private set; }

    public OrderLineItem(Fruits.Fruit fruit, decimal quantity)
    {
        if (fruit is null)
        {
            throw new ArgumentNullException(nameof(fruit));
        }

        if (quantity < 0)
        {
            throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));
        }

        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));
        }

        Fruit = fruit;
        Quantity = quantity;
        Subtotal = CalculateSubtotal();
    }

    public void UpdateQuantity(decimal newQuantity)
    {
        if (newQuantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than zero.", nameof(newQuantity));
        }

        Quantity = newQuantity;
        Subtotal = CalculateSubtotal();
    }

    private Price CalculateSubtotal()
    {
        return Fruit.CalculatePrice(Quantity);
    }
}