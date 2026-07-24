namespace TradeControl.Tax.UK.Models.Canonical;

public sealed class PayloadEnvelope
{
    public required string PayloadVersion { get; init; }

    public required string TaxSourceCode { get; init; }

    public required string PeriodStart { get; init; }

    public required string PeriodEnd { get; init; }

    public required string SubjectCode { get; init; }

    public required IReadOnlyList<PayloadItem> Items { get; init; }

    public Dictionary<string, object?>? Meta { get; init; }
}
