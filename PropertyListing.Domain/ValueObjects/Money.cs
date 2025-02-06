namespace PropertyListing.Domain.ValueObjects;

public record Money
{
    public decimal Amount { get; init; }
    public string Currency { get; init; }

    private Money() { }

    public Money(decimal amount, string currency = "TRY")
    {
        Amount = amount;
        Currency = currency;
    }

    public static implicit operator decimal(Money money) => money.Amount;
    
    public static Money FromDecimal(decimal amount, string currency = "TRY") 
        => new Money(amount, currency);
} 