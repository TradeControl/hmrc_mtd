namespace TradeControl.Tax.UK.Models.Hmrc;

public sealed class Submission
{
    public string? FormBundleNumber { get; init; }

    public string? ChargeReference { get; init; }

    public DateTimeOffset? SubmittedAt { get; init; }
}
