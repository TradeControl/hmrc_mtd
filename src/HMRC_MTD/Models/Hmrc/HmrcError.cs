namespace TradeControl.Tax.UK.Models.Hmrc;

public sealed class HmrcError
{
    public string Code { get; init; } = string.Empty;

    public string Message { get; init; } = string.Empty;
}
