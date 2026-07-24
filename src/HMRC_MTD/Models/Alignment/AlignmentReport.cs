namespace TradeControl.Tax.UK.Models.Alignment;

public sealed class AlignmentReport
{
    public AlignmentStatus Status { get; init; } = AlignmentStatus.Unknown;

    public string Message { get; init; } = string.Empty;
}
