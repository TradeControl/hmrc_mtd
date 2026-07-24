namespace TradeControl.Tax.UK.Models.Tc;

public sealed class TcSubmissionHistory
{
    public string SubmissionReference { get; init; } = string.Empty;

    public DateTimeOffset SubmittedAt { get; init; }
}
