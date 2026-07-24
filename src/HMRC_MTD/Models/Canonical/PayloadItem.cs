namespace TradeControl.Tax.UK.Models.Canonical;

public sealed class PayloadItem
{
    public required string Tag { get; init; }

    public required object Value { get; init; }
}
