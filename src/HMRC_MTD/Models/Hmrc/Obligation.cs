namespace TradeControl.Tax.UK.Models.Hmrc;

public sealed class Obligation
{
    public string? PeriodKey { get; init; }

    public string? Status { get; init; }

    public DateOnly? Start { get; init; }

    public DateOnly? End { get; init; }

    public DateOnly? Due { get; init; }

    public DateOnly? Received { get; init; }
}
