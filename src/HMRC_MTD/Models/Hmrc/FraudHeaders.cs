namespace TradeControl.Tax.UK.Models.Hmrc;

public sealed class FraudHeaders
{
    public Dictionary<string, string> Headers { get; init; } = new();
}
