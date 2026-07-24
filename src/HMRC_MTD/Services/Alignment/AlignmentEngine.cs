using TradeControl.Tax.UK.Models.Alignment;

namespace TradeControl.Tax.UK.Services.Alignment;

public sealed class AlignmentEngine
{
    public Task<AlignmentReport> RunAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(new AlignmentReport
        {
            Status = AlignmentStatus.Unknown,
            Message = "Alignment is outside Objective 2 scope."
        });
    }
}
