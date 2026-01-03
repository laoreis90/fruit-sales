namespace FruitSales.Domain.ValueObjects;

public record Price
{
    public decimal Amount { get; }

    private Price(decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Price cannot be negative.", nameof(amount));
        }

        Amount = amount;
    }

    public static Price Create(decimal amount)
    {
        return new Price(amount);
    }

    public static Price operator +(Price left, Price right)
    {
        return new Price(left.Amount + right.Amount);
    }

    public static Price operator *(Price price, decimal multiplier)
    {
        if (multiplier < 0)
        {
            throw new ArgumentException("Multiplier cannot be negative.", nameof(multiplier));
        }

        return new Price(price.Amount * multiplier);
    }
    
    public override string ToString()
    {
        return $"${Amount:F2}";
    }
}