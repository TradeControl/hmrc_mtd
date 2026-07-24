namespace TradeControl.Tax.UK.Infrastructure.Logging;

public sealed class SubmissionLogger
{
    public Task LogAsync(string operationType, string status, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
