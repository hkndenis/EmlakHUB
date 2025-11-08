namespace PropertyListing.Domain.ValueObjects;

/// <summary>
/// Para birimi ile birlikte tutarı temsil eden value object.
/// Value object representing monetary amount with currency.
/// </summary>
public record Money
{
    /// <summary>
    /// Para tutarı
    /// Monetary amount
    /// </summary>
    public decimal Amount { get; init; }
    
    /// <summary>
    /// Para birimi kodu (örn: TRY, USD, EUR)
    /// Currency code (e.g., TRY, USD, EUR)
    /// </summary>
    public string Currency { get; init; }

    private Money() { }

    /// <summary>
    /// Yeni bir Money örneği oluşturur
    /// Creates a new Money instance
    /// </summary>
    /// <param name="amount">Para tutarı / Monetary amount</param>
    /// <param name="currency">Para birimi kodu (varsayılan: TRY) / Currency code (default: TRY)</param>
    public Money(decimal amount, string currency = "TRY")
    {
        Amount = amount;
        Currency = currency;
    }

    /// <summary>
    /// Money nesnesini decimal değere dönüştürür
    /// Converts Money object to decimal value
    /// </summary>
    public static implicit operator decimal(Money money) => money.Amount;
    
    /// <summary>
    /// Decimal değerden Money nesnesi oluşturur
    /// Creates Money object from decimal value
    /// </summary>
    /// <param name="amount">Para tutarı / Monetary amount</param>
    /// <param name="currency">Para birimi kodu (varsayılan: TRY) / Currency code (default: TRY)</param>
    /// <returns>Money nesnesi / Money object</returns>
    public static Money FromDecimal(decimal amount, string currency = "TRY") 
        => new Money(amount, currency);
} 