namespace TradeControl.Tax.UK.Services.Runner;

public sealed class HmrcSubmissionRequest
{
    public required string OperationType { get; init; }

    public required Dictionary<string, object?> Parameters { get; init; }
}
