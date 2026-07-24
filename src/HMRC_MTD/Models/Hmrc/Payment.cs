namespace TradeControl.Tax.UK.Models.Hmrc;

public sealed class Payment
{
    public decimal Amount { get; init; }

    public DateOnly? Received { get; init; }
}
