namespace TradeControl.Tax.UK.Models.Hmrc;

public sealed class Liability
{
    public string? Type { get; init; }

    public DateOnly? OriginalAmount { get; init; }

    public DateOnly? OutstandingAmount { get; init; }

    public DateOnly? Due { get; init; }
}
