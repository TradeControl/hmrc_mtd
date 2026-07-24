using TradeControl.Tax.UK.Models.Hmrc;

namespace TradeControl.Tax.UK.Services.Runner;

public sealed class HmrcSubmissionResult
{
    public required string Status { get; init; }

    public object? CanonicalPayload { get; init; }

    public object? HmrcResponse { get; init; }

    public string? SubmissionReference { get; init; }

    public DateTimeOffset? SubmittedAt { get; init; }

    public IReadOnlyList<string> Warnings { get; init; } = Array.Empty<string>();

    public IReadOnlyList<string> Errors { get; init; } = Array.Empty<string>();

    public IReadOnlyList<HmrcError> HmrcErrors { get; init; } = Array.Empty<HmrcError>();
}
