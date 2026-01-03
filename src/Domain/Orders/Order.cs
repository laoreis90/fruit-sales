namespace FruitSales.Domain.Orders;

public class Order
{
    private readonly List<OrderLineItem> _lineItems = [];

    public IReadOnlyList<OrderLineItem> GetLineItems()
    {
        return _lineItems.AsReadOnly();
    }

    public void AddLineItem(Fruits.Fruit fruit, decimal quantity)
    {
        if (fruit is null)
        {
            throw new ArgumentNullException(nameof(fruit));
        }

        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));
        }

        OrderLineItem? existingItem = _lineItems.FirstOrDefault(item => item.Fruit.Name == fruit.Name);

        if (existingItem is not null)
        {
            existingItem.UpdateQuantity(existingItem.Quantity + quantity);
        }
        else
        {
            OrderLineItem lineItem = new(fruit, quantity);
            _lineItems.Add(lineItem);
        }
    }

    public bool IsEmpty()
    {
        return _lineItems.Count == 0;
    }
}