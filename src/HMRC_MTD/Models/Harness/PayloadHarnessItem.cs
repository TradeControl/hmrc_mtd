namespace TradeControl.Tax.UK.Models.Harness;

public sealed class PayloadHarnessItem
{
    public required string Tag { get; init; }

    public required object Value { get; init; }
}
